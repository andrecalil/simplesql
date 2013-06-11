using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SimpleSQL.Domain
{
    public class Delete : ConditionedCommand
    {
        public static string CommandRegex = @"^(?:\s*(?i:DELETE FROM)\s+)(?<table>\S+\s+)(?<condition>(?:(?i:where)|(?i:and)|(?i:or))(?:\s+.+\s*)(?:=|<|>|<=|>=|<>|(?i:in)|(?i:not in))(?:(?:\s*\(?)(?:\s*.+\s*)(?:,\s*.+\s*)*(?:\s*\)?)))*$";

        public Delete()
        {
            this.Conditions = new List<Condition>();
        }

        public override void FromRawCommand(Match pRegexMatch, string pRawCommand)
        {
            //Regex mRegex = new Regex(Delete.CommandRegex, RegexOptions.Compiled);

            //Match pRegexMatch = mRegex.Match(pRawCommand);

            this.GetHeaderTransformation(pRawCommand, pRegexMatch);

            this.GetConditionsFromRegexMatch(pRegexMatch);
        }
    }
}