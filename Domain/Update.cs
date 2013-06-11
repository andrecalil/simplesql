using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SimpleSQL.Domain
{
    public class Update : ConditionedCommand
    {
        public List<Condition> Targets { get; set; }

        public static string CommandRegex = @"^\s*(?i:UPDATE)\s+(?<table>\S+\s+)(?i:SET)\s+(?<attributes>(\s*\S+\s*)\=(\s*\S+\s*)(,(\s*\S+\s*)\=(\s*\S+\s*))*)+(?<condition>((?i:where)|(?i:and)|(?i:or))(\s+.+\s*)(=|<|>|<=|>=|<>|(?i:in)|(?i:not in))((\s*\(?)(\s*.+\s*)(,\s*.+\s*)*(\s*\)?)))*$";

        public Update()
        {
            this.Conditions = new List<Condition>();
            this.Targets = new List<Condition>();
        }

        public override void FromRawCommand(Match pRegexMatch, string pRawCommand)
        {
            //Regex mRegex = new Regex(Update.CommandRegex, RegexOptions.Compiled);

            //Match pRegexMatch = mRegex.Match(pRawCommand);

            this.GetHeaderTransformation(pRawCommand, pRegexMatch);

            #region Targets

            List<Condition> mTargetsList = new List<Condition>();

            string mTargets = pRegexMatch.Groups["attributes"].Captures[0].Value;

            if (mTargets.Contains(","))
            {
                string[] mTargetsArray = mTargets.Split(',');

                string mAttribute = string.Empty;
                string mValue = string.Empty;

                Condition mTargetCondition;

                foreach (string mCurrentTarget in mTargetsArray)
                {
                    mAttribute = mCurrentTarget.Trim().Split('=')[0].Trim();
                    mValue = mCurrentTarget.Trim().Split('=')[1].Trim();

                    mTargetCondition = new Condition()
                    {
                        Operator = EnumOperator.Equal,
                        Attribute = mAttribute,
                        Values = new List<object> { this.PrepareValue(mValue) }
                    };

                    mTargetsList.Add(mTargetCondition);
                }
            }
            else
            {
                string mAttribute = mTargets.Trim().Split('=')[0].Trim();
                string mValue = mTargets.Trim().Split('=')[1].Trim();

                Condition mTargetCondition = new Condition()
                {
                    Operator = EnumOperator.Equal,
                    Attribute = mAttribute,
                    Values = new List<object> { this.PrepareValue(mValue) }
                };

                mTargetsList.Add(mTargetCondition);
            }

            this.Targets = mTargetsList;

            #endregion

            this.GetConditionsFromRegexMatch(pRegexMatch);
        }
    }
}