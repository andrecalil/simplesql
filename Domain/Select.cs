using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleSQL.Domain
{
    public class Select : ConditionedCommand
    {
        public List<string> Attributes { get; set; }
        public List<InnerJoin> Joins { get; set; }
        public List<string> SingleNameAttributes
        {
            get
            {
                List<String> mReturn = new List<string>();

                foreach (string mAttribute in this.Attributes)
                {
                    if (mAttribute.Contains("."))
                    {
                        mReturn.Add(mAttribute.Split('.')[1]);
                    }
                    else
                    {
                        mReturn.Add(mAttribute);
                    }
                }

                return mReturn;
            }
        }

        public new static string CommandRegex = @"^(?:\s*(?i:SELECT)\s+)(?<attributes>(?:\S+\s*)(?:,\s*\S+\s*)*)(?:\s+(?i:FROM)\s+)(?<table>\s*\S+\s*)(?<join>\s+(?i:inner join)\s+(?<toTable>\s*\S+\s*)(?:\s+(?i:on)\s+)(?<fromKey>\s*\S+\s*)(?:\s*=\s*)(?<toKey>\s*\S+\s*)\s+)*(?<condition>(?:(?i:where)|(?i:and)|(?i:or))(?:\s+.+\s*)(?:=|<|>|<=|>=|<>|(?i:in)|(?i:not in)|(?i:like))(?:(?:\s*\(?)(?:\s*.+\s*)(?:,\s*.+\s*)*(?:\s*\)?)))*$";

        public Select()
        {
            this.Attributes = new List<string>();
            this.Joins = new List<InnerJoin>();
            this.Conditions = new List<Condition>();
        }

        public override void FromRawCommand(Match pRegexMatch, string pRawCommand)
        {
            this.GetHeaderTransformation(pRawCommand, pRegexMatch);

            #region Attributes

            string mAttributes = pRegexMatch.Groups["attributes"].Captures[0].Value.Trim();
            //string[] mAttributesArray = mAttributes.Split(',');
            //List<string> mCommandAttributes = new List<string>();
            foreach (string mCurrentAttribute in mAttributes.Split(','))
            {
                this.Attributes.Add(mCurrentAttribute.Trim());
            }

            //this.Attributes = mAttributes.Split(',').ToList();
            //this.Attributes.ForEach(x => x = x.Trim());

            #endregion

            #region Joins

            CaptureCollection mJoins = pRegexMatch.Groups["join"].Captures;
            CaptureCollection mToTables = pRegexMatch.Groups["toTable"].Captures;
            CaptureCollection mFromKeys = pRegexMatch.Groups["fromKey"].Captures;
            CaptureCollection mToKeys = pRegexMatch.Groups["toKey"].Captures;

            for (int mIx = 0; mIx < mJoins.Count; mIx++)
            {
                this.Joins.Add(new InnerJoin()
                    {
                        FromAttribute = mFromKeys[mIx].Value.Trim(),
                        ToAttribute = mToKeys[mIx].Value.Trim(),
                        WithTable = mToTables[mIx].Value.Trim()
                    });
            }

            #endregion

            this.GetConditionsFromRegexMatch(pRegexMatch);
        }
    }
}