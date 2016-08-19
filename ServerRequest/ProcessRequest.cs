using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Amazon;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using SimpleSQL.Domain;

namespace SimpleSQL.ServerRequest
{
    public class ProcessRequest
    {
        public static object ThreadLock = new object();

        #region Class attributes

        private Dictionary<string, List<string>> aDomainsDistribution;
        private AmazonSimpleDB aSimpleDBClient;
        private bool aSingleDomain;
        private string aSingleDomainName;

        private string aSimpleSQLTableName = "SimpleSQL_TableName";

        private string aAWSAccessKey;
        private string aAWSSecretAccessKey;

        #endregion

        #region Constructor methods

        public ProcessRequest(string pAWSAccessKey, string pAWSSecretAccessKey, Dictionary<string, List<string>> pDomainsDistribution)
        {
            if (pDomainsDistribution.Count > 1)
            {
                this.aSingleDomain = false;
                this.aDomainsDistribution = pDomainsDistribution;
            }
            else
            {
                this.SetSingleDomain(pDomainsDistribution.Keys.ElementAt<string>(0));
            }

            this.KeepSimpleDBAccessData(pAWSAccessKey, pAWSSecretAccessKey);

            this.SetSimpleDBClient();
        }

        public ProcessRequest(string pAWSAccessKey, string pAWSSecretAccessKey, string pSingleDomainName)
        {
            this.SetSingleDomain(pSingleDomainName);

            this.KeepSimpleDBAccessData(pAWSAccessKey, pAWSSecretAccessKey);

            this.SetSimpleDBClient();
        }

        private void SetSingleDomain(string pSingleDomainName)
        {
            this.aSingleDomain = true;
            this.aSingleDomainName = pSingleDomainName;
        }

        private void KeepSimpleDBAccessData(string pAWSAccessKey, string pAWSSecretAccessKey)
        {
            this.aAWSAccessKey = pAWSAccessKey;
            this.aAWSSecretAccessKey = pAWSSecretAccessKey;
        }

        private void SetSimpleDBClient()
        {
            if (this.aSimpleDBClient != null)
            {
                this.aSimpleDBClient.Dispose();
                this.aSimpleDBClient = null;
            }

            this.aSimpleDBClient = AWSClientFactory.CreateAmazonSimpleDBClient(this.aAWSAccessKey, this.aAWSSecretAccessKey);
        }

        #endregion

        /// <summary>
        /// Based on the table name from the command, this method returns the domain name, as set
        /// on the domain distribution dictionary, on the instantiation of this object
        /// </summary>
        /// <param name="pTable">The table name from the command object (from Command abstract class)</param>
        /// <returns>The SimpleDB domain name</returns>
        private string GetDomainName(string pTable)
        {
            if (this.aSingleDomain)
            {
                return this.aSingleDomainName;
            }
            else
            {
                foreach (KeyValuePair<string, List<string>> mCurrentDomain in this.aDomainsDistribution)
                {
                    if (mCurrentDomain.Value.Any<string>(x => x.Equals(pTable)))
                    {
                        return mCurrentDomain.Key;
                    }
                }
            }

            throw new Exception(string.Format("SimpleSQL: Table not found at domains distribution. Table: {0}", pTable));
        }

        /// <summary>
        /// Receives a list of conditions and turns it into a WHERE expression (WHERE ... AND ...)
        /// </summary>
        /// <param name="pConditions"></param>
        /// <returns></returns>
        private string CreateConditionsExpression(List<Condition> pConditions, string pTableName)
        {
            StringBuilder mConditionsExpression = new StringBuilder();

            mConditionsExpression.AppendFormat("WHERE {0}='{1}' ",
                    new object[]
                        {
                            this.aSimpleSQLTableName,
                            pTableName
                        }
                );

            if (pConditions != null && pConditions.Count > 0)
            {
                foreach (Domain.Condition mCondition in pConditions)
                {
                    mConditionsExpression.AppendFormat("{0} {1} {2} {3} ",
                        new object[]
                        {
                            "AND",
                            mCondition.SingleNameAttribute,
                            mCondition.Operator.ToCommand(),
                            (mCondition.Operator.IsListOperator() ?
                                "(" + string.Join(",", (from value in mCondition.Values select value.ToString()).ToArray<string>()) + ")"
                                : mCondition.Values[0].ToString())
                        }
                    );
                }    
            }

            return mConditionsExpression.ToString();
        }

        /// <summary>
        /// Executes a simple select, that is: SELECT * FROM [domain] [conditions] 
        /// </summary>
        /// <param name="pDomainName">The target domain name</param>
        /// <param name="pConditions">The conditions list, if any (null is supported)</param>
        /// <returns></returns>
        private SelectResponse ProcessSimpleSelect(string pDomainName, List<string> pAttributes, List<Condition> pConditions, string pTableName)
        {
            DateTime mStart = DateTime.Now;

            StringBuilder mSelectExpression = new StringBuilder();
            mSelectExpression.AppendFormat(
                "SELECT {0} FROM {1} ",
                ((pAttributes != null && pAttributes.Count > 0)? string.Join(",", pAttributes.ToArray()) : "*"),
                pDomainName);
            mSelectExpression.Append(this.CreateConditionsExpression(pConditions, pTableName));

            Debug.WriteLine(mSelectExpression.ToString());

            SelectRequest mSelectRequest = new SelectRequest().WithSelectExpression(mSelectExpression.ToString());
            SelectResponse mSelectResponse = this.aSimpleDBClient.Select(mSelectRequest);
            mSelectResponse = this.ProcessSimpleSelectRecursive(mSelectRequest, mSelectResponse);

            Debug.WriteLine(string.Format("ACCESS: {0}", DateTime.Now.Subtract(mStart).ToString()));

            return mSelectResponse;
        }

        private SelectResponse ProcessSimpleSelectRecursive(SelectRequest pRequest, SelectResponse pResponse)
        {
            if (pResponse.SelectResult.IsSetNextToken())
            {
                pRequest.NextToken = pResponse.SelectResult.NextToken;

                SelectResponse mNewResponse;
                try
                {
                    mNewResponse = this.aSimpleDBClient.Select(pRequest);
                }
                catch (System.OutOfMemoryException)
                {
                    this.SetSimpleDBClient();
                    
                    mNewResponse = this.aSimpleDBClient.Select(pRequest);
                }
                
                mNewResponse.SelectResult.Item.AddRange(pResponse.SelectResult.Item);

                return this.ProcessSimpleSelectRecursive(pRequest, mNewResponse);
            }
            else
            {
                return pResponse;
            }
        }

        private DataTable TransformSelectResult(string pTableName, List<string> pAttributes, SelectResponse pSelectResponse)
        {
            DateTime mStart = DateTime.Now;

            DataTable mReturn = new DataTable();

            if (pSelectResponse.IsSetSelectResult())
            {
                mReturn.TableName = pTableName;

                bool mAllAttributes = pAttributes.Count == 1 ? pAttributes[0].Equals("*") : false;

                if (!mAllAttributes)
                {
                    pAttributes.ForEach(attr => mReturn.Columns.Add(attr.Trim(), typeof(string)));
                }
                else
                {
                    pSelectResponse.SelectResult.Item[0].Attribute.ForEach(x => mReturn.Columns.Add(x.Name.Trim(), typeof(string)));
                    mReturn.Columns.Remove("SimpleSQL_TableName");
                }

                DataRow mNewRow;

                foreach (Item mCurrentItem in pSelectResponse.SelectResult.Item)
                {
                    if (mCurrentItem.IsSetAttribute())
                    {
                        mNewRow = mReturn.NewRow();

                        foreach (Amazon.SimpleDB.Model.Attribute mCurrentAttribute in mCurrentItem.Attribute)
                        {
                            if (mCurrentAttribute.IsSetName() && mCurrentAttribute.IsSetValue())
                            {
                                if (mReturn.Columns.Contains(mCurrentAttribute.Name.Trim()))
                                {
                                    mNewRow[mCurrentAttribute.Name.Trim()] = mCurrentAttribute.Value;
                                }
                            }
                        }

                        mReturn.Rows.Add(mNewRow);
                    }
                }
            }
            else
            {
                return null;
            }

            Debug.WriteLine(string.Format("TRANSFORM: {0}", DateTime.Now.Subtract(mStart).ToString()));

            return mReturn;
        }
        
        public string Request(Domain.Update pUpdate)
        {
            #region Update conditions validation

            if (pUpdate.Conditions.Count == 0)
            {
                throw new Exception("SimpleSQL: unparameterized update is not supported.");
            }

            #endregion

            string mDomainName = this.GetDomainName(pUpdate.Table);

            SelectResponse mSelectResponse = this.ProcessSimpleSelect(mDomainName, null, pUpdate.Conditions, pUpdate.Table);

            int mTotalUpdatedItems = 0;

            if (mSelectResponse.IsSetSelectResult())
            {
                foreach (Item mCurrentItem in mSelectResponse.SelectResult.Item)
                {
                    if (mCurrentItem.IsSetName())
                    {
                        List<ReplaceableAttribute> mUpdateTargets = new List<ReplaceableAttribute>();

                        foreach (Condition mTarget in pUpdate.Targets)
                        {
                            mUpdateTargets.Add(new ReplaceableAttribute()
                                {
                                    Name = mTarget.Attribute,
                                    Value = mTarget.Values[0].ToString(),
                                    Replace = true
                                }
                            );
                        }
                        
                        PutAttributesRequest mReplaceAction = new PutAttributesRequest().WithDomainName(mDomainName).WithItemName(mCurrentItem.Name).WithAttribute(mUpdateTargets.ToArray());
                        
                        this.aSimpleDBClient.PutAttributes(mReplaceAction);

                        mTotalUpdatedItems++;
                    }
                }
            }

            return mTotalUpdatedItems.ToString();
        }

        public string Request(Domain.Insert pInsert)
        {
            #region Attributes and values validation

            if (pInsert.Attributes.Count != pInsert.Values.Count)
            {
                throw new Exception("SimpleSQL: insert command has different attributes and values.");
            }

            #endregion

            PutAttributesRequest mPutAction = new PutAttributesRequest().WithDomainName(this.GetDomainName(pInsert.Table)).WithItemName(pInsert.ItemID.ToString());

            List<ReplaceableAttribute> mPutAttributes = mPutAction.Attribute;
            ReplaceableAttribute mAttribute;

            #region Add attributes to the SimpleDB object

            int mTotalAttributes = pInsert.Attributes.Count;
            for (int mIndex = 0; mIndex < mTotalAttributes; mIndex++)
            {
                mAttribute = new ReplaceableAttribute()
                {
                    Name = pInsert.Attributes[mIndex],
                    Value = pInsert.Values[mIndex].ToString()
                };

                mPutAttributes.Add(mAttribute);
            }

            #endregion

            mPutAttributes.Add(new ReplaceableAttribute().WithName(this.aSimpleSQLTableName).WithValue(pInsert.Table));
            
            PutAttributesResponse mResponse = this.aSimpleDBClient.PutAttributes(mPutAction);

            return mResponse.ToString();
        }

        public string Request(Domain.Delete pDelete)
        {
            #region Delete conditions validation

            if (pDelete.Conditions.Count == 0)
            {
                throw new Exception("SimpleSQL: unparameterized delete is not supported.");
            }

            #endregion

            string mDomainName = this.GetDomainName(pDelete.Table);

            SelectResponse mSelectResponse = this.ProcessSimpleSelect(mDomainName, null, pDelete.Conditions, pDelete.Table);

            int mTotalDeleteItems = 0;

            if (mSelectResponse.IsSetSelectResult())
            {
                DeleteAttributesRequest mDeleteAction;

                foreach (Item mCurrentItem in mSelectResponse.SelectResult.Item)
                {
                    if (mCurrentItem.IsSetName())
                    {
                        mDeleteAction = new DeleteAttributesRequest().WithDomainName(mDomainName).WithItemName(mCurrentItem.Name);
                        this.aSimpleDBClient.DeleteAttributes(mDeleteAction);

                        mTotalDeleteItems++;
                    }
                }
            }

            return mTotalDeleteItems.ToString();
        }

        public DataTable Request(Domain.Select pSelect)
        {
            string mMainDomainName = this.GetDomainName(pSelect.Table);

            DataTable mReturn = null;

            #region No joins (simple select)
            if (pSelect.Joins == null || pSelect.Joins.Count == 0)
            {
                mReturn = this.TransformSelectResult(pSelect.Table, pSelect.Attributes, this.ProcessSimpleSelect(mMainDomainName, pSelect.SingleNameAttributes, pSelect.Conditions, pSelect.Table));
                mReturn.TableName = mMainDomainName;
            }
            #endregion
            #region Joins
            else
            {
                List<string> mTables = new List<string>();
                mTables.Add(pSelect.Table);
                mTables.AddRange(pSelect.Joins.Select<InnerJoin, string>(x => x.WithTable).ToList<string>());

                List<Select> mSplittedSelects = new List<Select>();

                #region Split the main select into a set of simple selects

                List<Thread> mSelectSplitThreads = new List<Thread>();

                foreach (string mCurrentTable in mTables)
                {
                    Thread mThread = new Thread(SplitToSimpleSelect);
                    mThread.Start(new SelectSplitThread(pSelect, mCurrentTable, mSplittedSelects));

                    mSelectSplitThreads.Add(mThread);
                }

                int mTimer = 0;
                while(mSelectSplitThreads.Any<Thread>(x => x.IsAlive))
                {
                    mTimer++;
                }

                #endregion

                Debug.WriteLine(string.Format("SPLIT: {0}", DateTime.Now.ToString()));

                List<DataTable> mMultipleTables = new List<DataTable>();

                #region Get the resulting DataTable for each joinned table

                //string mCurrentDomain = string.Empty;

                //foreach (KeyValuePair<string, List<string>> mTableAndColumns in mTablesAndColumns)
                //{
                //    mCurrentDomain = this.GetDomainName(mTableAndColumns.Key);

                //    mMultipleTables.Add(this.TransformSelectResult(mCurrentDomain, mTableAndColumns.Value, this.ProcessSimpleSelect(mCurrentDomain,  mTableAndColumns.Value, mTablesAndConditions[mTableAndColumns.Key], mTableAndColumns.Key)));
                //}

                List<Thread> mRetrieveDataTables = new List<Thread>();

                //int mStartI = 0;
                //int mFinishI = mSplittedSelects.Count - 1;
                //for(mStartI = 0; mStartI <= mFinishI; mStartI++)
                foreach (Select mCurrentSelect in mSplittedSelects)
                {
                    //ThreadStart mThreadStart = new ThreadStart(() => RetrieveResultingDataTable());
                    Thread mThread = new Thread(RetrieveResultingDataTable);
                    mThread.Start(new ProcessSelectThread(mCurrentSelect, mMultipleTables, this));

                    mRetrieveDataTables.Add(mThread);
                }

                //mRetrieveDataTables.ForEach(x => x.Start());

                while (mRetrieveDataTables.Any<Thread>(x => x.IsAlive))
                {
                    mTimer++;
                }

                #endregion

                DataTable mJoinnedTable = new DataTable();

                #region Join the results into a single happy DataTable

                DataTable mLeftTable;
                DataTable mRightTable;

                bool mFirstJoin = true;

                foreach(InnerJoin mCurrentJoin in pSelect.Joins)
                {
                    if (mFirstJoin)
                    {
                        mLeftTable = mMultipleTables.Where<DataTable>(x => x.TableName == pSelect.Table).Single<DataTable>();

                        foreach (DataColumn mCurrentColumn in mLeftTable.Columns)
                        {
                            mJoinnedTable.Columns.Add(string.Format("{0}.{1}", mLeftTable.TableName, mCurrentColumn.ColumnName), typeof(string));
                        }
                    }
                    else
                    {
                        mLeftTable = mJoinnedTable.Copy();
                    }
                    
                    mRightTable = mMultipleTables.Where<DataTable>(x => x.TableName == mCurrentJoin.WithTable).Single<DataTable>();

                    foreach (DataColumn mCurrentColumn in mRightTable.Columns)
                    {
                        mJoinnedTable.Columns.Add(string.Format("{0}.{1}", mRightTable.TableName, mCurrentColumn.ColumnName), typeof(string));
                    }

                    foreach (DataRow mLeftTableRow in mLeftTable.Rows)
                    {
                        if (mRightTable.Columns.Count > 0)
                        {
                            foreach (DataRow mRightTableRow in mRightTable.Rows)
                            {
                                if (mLeftTableRow[(mFirstJoin ? mCurrentJoin.SingleNameFrom : mCurrentJoin.FromAttribute)].ToString() == mRightTableRow[mCurrentJoin.SingleNameTo].ToString())
                                {
                                    DataRow mNewJoinnedRow = mJoinnedTable.NewRow();

                                    foreach (DataColumn mLeftColumn in mLeftTable.Columns)
                                    {
                                        mNewJoinnedRow[string.Format("{0}.{1}", mLeftTable.TableName, mLeftColumn.ColumnName)] = mLeftTableRow[mLeftColumn];
                                    }

                                    foreach (DataColumn mRightColumn in mRightTable.Columns)
                                    {
                                        mNewJoinnedRow[string.Format("{0}.{1}", mRightTable.TableName, mRightColumn.ColumnName)] = mRightTableRow[mRightColumn];
                                    }

                                    mJoinnedTable.Rows.Add(mNewJoinnedRow);
                                }
                            }
                        }
                    }

                    mFirstJoin = false;
                }

                #endregion

                mReturn = mJoinnedTable;

                #region Remove the unexpected resulting columns

                DataTable mRemoveColumns = mReturn.Clone();

                foreach (DataColumn mColumn in mRemoveColumns.Columns)
                {
                    if (!pSelect.Attributes.Contains(mColumn.ColumnName))
                    {
                        mReturn.Columns.Remove(mReturn.Columns[mColumn.ColumnName]);
                    }
                }

                #endregion               

                Debug.WriteLine(string.Format("JOIN: {0}", DateTime.Now.ToString()));
            }
            #endregion

            return mReturn;
        }

        public void SplitToSimpleSelect(object pParameters)
        {
            SelectSplitThread mParameters = (SelectSplitThread)pParameters;
            Select mMainSelect = mParameters.SelectToSplit;

            Select mNewSelect = new Select();
            mNewSelect.Table = mParameters.TargetTable;

            Debug.WriteLine(string.Format("[{0}] select split thread start", mNewSelect.Table));

            #region Retrieve the attributes

            foreach (string mCurrentAttribute in mMainSelect.Attributes)
            {
                if (mCurrentAttribute.Trim().Split('.')[0].Equals(mNewSelect.Table))
                {
                    mNewSelect.Attributes.Add(mCurrentAttribute);
                }
            }

            #endregion

            #region Retrive attributes used at joins

            foreach (InnerJoin mCurrentJoin in mMainSelect.Joins)
            {
                if (mCurrentJoin.FromAttribute.StartsWith(string.Format("{0}.",mNewSelect.Table)))
                {
                    if (!mNewSelect.Attributes.Contains(mCurrentJoin.FromAttribute))
                    {
                        mNewSelect.Attributes.Add(mCurrentJoin.FromAttribute);
                    }
                }
                else if (mCurrentJoin.ToAttribute.StartsWith(string.Format("{0}.", mNewSelect.Table)))
                {
                    if (!mNewSelect.Attributes.Contains(mCurrentJoin.ToAttribute))
                    {
                        mNewSelect.Attributes.Add(mCurrentJoin.ToAttribute);
                    }
                }
            }

            #endregion

            #region Retrieve the conditions

            foreach (Condition mCurrentCondition in mMainSelect.Conditions)
            {
                if (mCurrentCondition.Attribute.Trim().Split('.')[0].Equals(mNewSelect.Table))
                {
                    mNewSelect.Conditions.Add(mCurrentCondition);
                }
            }

            #endregion

            lock (ThreadLock)
            {
                mParameters.SelectStore.Add(mNewSelect);
            }

            Debug.WriteLine(string.Format("[{0}] select split thread finish", mNewSelect.Table));
        }

        public static void RetrieveResultingDataTable(object pParameters)
        {
            ProcessSelectThread mParameters = (ProcessSelectThread)pParameters;
            Select mSelectToRun = mParameters.SelectToProcess;
            Debug.WriteLine(string.Format("[{0}] select process thread start", mSelectToRun.Table));
            String mCurrentDomain = mParameters.ProcessRequest.GetDomainName(mSelectToRun.Table);

            DataTable mResult = mParameters.ProcessRequest.TransformSelectResult(mSelectToRun.Table, mSelectToRun.SingleNameAttributes, mParameters.ProcessRequest.ProcessSimpleSelect(mCurrentDomain, mSelectToRun.SingleNameAttributes, mSelectToRun.Conditions, mSelectToRun.Table));

            lock (ThreadLock)
            {
                mParameters.ResultStore.Add(mResult);
            }

            Debug.WriteLine(string.Format("[{0}] select process thread finish", mSelectToRun.Table));
        }
    }
}