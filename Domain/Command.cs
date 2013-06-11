using System;
using System.Data;
using System.Text.RegularExpressions;

namespace SimpleSQL.Domain
{
    public abstract class Command
    {
        public string Table { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime RespondedOn { get; set; }
        public string RawCommand { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public DataTable Result { get; set; }
        public static string CommandRegex { get; set; }

        public abstract void FromRawCommand(Match pRegexMatch, string pRawCommand);

        protected void GetHeaderTransformation(string pRawCommand, Match pRegexMatch)
        {
            this.RawCommand = pRawCommand;
            
            this.RequestedOn = DateTime.Now;

            this.Table = pRegexMatch.Groups["table"].Captures[0].Value.Trim();
        }
    }
}