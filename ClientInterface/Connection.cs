using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleSQL.Domain;
using SimpleSQL.ServerRequest;
using System.Data;
using System.Text.RegularExpressions;

namespace SimpleSQL.ClientInterface
{
    public class Connection
    {
        #region Class attributes

        private ProcessRequest aProcessRequest;

        #endregion

        #region Contructor methods

        public Connection (string pAWSAccessKey, string pAWSSecretAccessKey, Dictionary<string, List<string>> pDomainsDistribution)
        {
            this.aProcessRequest = new ProcessRequest(pAWSAccessKey, pAWSSecretAccessKey, pDomainsDistribution);
        }

        public Connection(string pAWSAccessKey, string pAWSSecretAccessKey, string pSingleDomainName)
        {
            this.aProcessRequest = new ProcessRequest(pAWSAccessKey, pAWSSecretAccessKey, pSingleDomainName);
        }

        #endregion

        #region Public methods

        public string ExecuteNonQuery(string pCommand)
        {
            Command mCommand = this.TransformDomainFromRawCommand(pCommand);

            if (mCommand is Update)
            {
                return this.aProcessRequest.Request((Update)mCommand);
            }
            else if (mCommand is Insert)
            {
                return this.aProcessRequest.Request((Insert)mCommand);
            }
            else if (mCommand is Delete)
            {
                return this.aProcessRequest.Request((Delete)mCommand);
            }
            else
            {
                throw new Exception("SimpleSQL: unidentified type of command");
            }
        }

        public DataTable ExecuteQuery(string pCommand)
        {
            return this.aProcessRequest.Request((Select)this.TransformDomainFromRawCommand(pCommand));
        }

        #endregion

        #region Debugging methods

        public T GetCommand<T>(string pRawCommand) where T : Command
        {
            return (T)this.TransformDomainFromRawCommand(pRawCommand);
        }

        #endregion

        private Command TransformDomainFromRawCommand(string pRawCommand)
        {
            string pFormattedCommandString = pRawCommand.Trim();
            //pFormattedCommandString = pFormattedCommandString.ToUpper();
        
            if (pFormattedCommandString.ToUpper().StartsWith("SELECT"))
            {
                Match mRegexMatch = Regex.Match(pFormattedCommandString, Select.CommandRegex);
                if (mRegexMatch.Success)
                {
                    Select mCommand = new Select();
                    mCommand.FromRawCommand(mRegexMatch, pFormattedCommandString);

                    return mCommand;
                }
                else
                {
                    throw new Exception("SimpleSQL: SELECT command not well formed.");
                }
            }
            else if (pFormattedCommandString.ToUpper().StartsWith("UPDATE"))
            {
                Match mRegexMatch = Regex.Match(pFormattedCommandString, Update.CommandRegex);
                if (mRegexMatch.Success)
                {
                    Update mCommand = new Update();
                    mCommand.FromRawCommand(mRegexMatch, pFormattedCommandString);

                    return mCommand;
                }
                else
                {
                    throw new Exception("SimpleSQL: UPDATE command not well formed.");
                }
            }
            else if (pFormattedCommandString.ToUpper().StartsWith("INSERT"))
            {
                Match mRegexMatch = Regex.Match(pFormattedCommandString, Insert.CommandRegex);
                if (mRegexMatch.Success)
                {
                    Insert mCommand = new Insert();
                    mCommand.FromRawCommand(mRegexMatch, pFormattedCommandString);

                    return mCommand;
                }
                else
                {
                    throw new Exception("SimpleSQL: INSERT command not well formed.");
                }
            }
            else if (pFormattedCommandString.ToUpper().StartsWith("DELETE"))
            {
                Match mRegexMatch = Regex.Match(pFormattedCommandString, Delete.CommandRegex);
                if (mRegexMatch.Success)
                {
                    Delete mCommand = new Delete();
                    mCommand.FromRawCommand(mRegexMatch, pFormattedCommandString);

                    return mCommand;
                }
                else
                {
                    throw new Exception("SimpleSQL: DELETE command not well formed.");
                }
            }

            throw new Exception("SimpleSQL: unidentified command");
        }
    }
}