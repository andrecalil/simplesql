using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace SimpleSQL.Domain
{
    public abstract class ConditionedCommand : Command
    {
        public List<Condition> Conditions { get; set; }

        public void GetConditionsFromRegexMatch(Match pRegexMatch)
        {
            CaptureCollection mConditions = pRegexMatch.Groups["condition"].Captures;
            if (mConditions.Count > 0)
            {
                List<string> mCommandConditions = this.SplitConditions(mConditions[0].Value);

                Condition mNewCondition;
                Condition mLastCondition = null;
                string[] mSplittedCondition;
                List<string> mConditionTerms;

                foreach (string mCurrentCondition in mCommandConditions)
                {
                    mSplittedCondition = mCurrentCondition.Split(' ');
                    mSplittedCondition.ToList<string>().ForEach(x => x.Trim());
                    mConditionTerms = mSplittedCondition.Where<string>(x => !string.IsNullOrEmpty(x)).ToList<string>();

                    mNewCondition = new Condition();

                    switch (mConditionTerms[0].ToUpper())
                    {
                        case "WHERE":
                        case "AND":
                            {
                                mNewCondition = this.CreateConditionFromTerms(mConditionTerms);

                                this.Conditions.Add(mNewCondition);
                                mLastCondition = mNewCondition;

                                break;
                            }
                        case "OR":
                            {
                                mLastCondition.Or = this.CreateConditionFromTerms(mConditionTerms);

                                break;
                            }
                    }
                } 
            }
        }

        private List<string> SplitConditions(string pFullConditionsString)
        {
            List<string> mReturn = new List<string>();

            return this.SplitConditionsRecursive(pFullConditionsString, mReturn);
        }

        private List<string> SplitConditionsRecursive(string pConditions, List<string> pAlreadySplitedConditions)
        {
            if (pConditions.ToUpper().Substring(5).Contains(" OR ") || pConditions.ToUpper().Substring(5).Contains(" AND "))
            {
                string mSplittedCondition;
                string mRemainingConditions;

                if (pConditions.ToUpper().IndexOf(" OR ", 5) > pConditions.ToUpper().IndexOf(" AND ", 5))
                {
                    mSplittedCondition = pConditions.Substring(0, pConditions.ToUpper().IndexOf(" OR ", 5));
                    mRemainingConditions = pConditions.Substring(pConditions.ToUpper().IndexOf(" OR ", 5));
                }
                else
                {
                    mSplittedCondition = pConditions.Substring(0, pConditions.ToUpper().IndexOf(" AND ", 5));
                    mRemainingConditions = pConditions.Substring(pConditions.ToUpper().IndexOf(" AND ", 5));
                }

                pAlreadySplitedConditions.Add(mSplittedCondition);

                return this.SplitConditionsRecursive(mRemainingConditions, pAlreadySplitedConditions);
            }
            else
            {
                pAlreadySplitedConditions.Add(pConditions);
            }

            return pAlreadySplitedConditions;
        }

        private Condition CreateConditionFromTerms(List<string> pConditionTerms)
        {
            Condition mReturn = new Condition();
            mReturn.Attribute = pConditionTerms[1];
            mReturn.Operator = EnumOperatorUtils.FromCommand(pConditionTerms[2]);

            if (mReturn.Operator.IsListOperator())
            {
                string[] mValues = pConditionTerms[3].Replace("(", "").Replace(")", "").Split(',');
                mValues.ToList<string>().ForEach(x => x = this.PrepareValue(x));

                mReturn.Values = mValues.ToList<object>();
            }
            else
            {
                mReturn.Values = new List<object> { this.PrepareValue(pConditionTerms[3]) };
            }

            return mReturn;
        }

        protected string PrepareValue(string pValue)
        {
            string mValue = pValue.Trim();

            if (!mValue.StartsWith("'"))
                mValue = string.Format("{0}{1}", "'", mValue);

            if (!mValue.EndsWith("'"))
                mValue = string.Format("{0}{1}", mValue, "'");

            return mValue;
        }
    }
}