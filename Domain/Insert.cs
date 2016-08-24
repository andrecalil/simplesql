using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SimpleSQL.Domain
{
    public class Insert : Command
    {
        public string ItemID { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Values { get; set; }

        public new static string CommandRegex = @"^(?:\s*(?i:INSERT INTO)\s+)(?<table>[^\(]+)(?:\(\s*)(?<attributes>(?:\w+\s*)(?:\,\s*\w+\s*)*)(?:\)\s+)(?:(?i:VALUES)\s+\(\s*)(?<values>(?:[^\,]+|[^\)])+\s*(?:\,\s*[^\)])*)(?:\))$";

        public Insert()
        {
            this.Attributes = new List<string>();
            this.Values = new List<string>();
        }

        public override void FromRawCommand(Match pRegexMatch, string pRawCommand)
        {
            this.GetHeaderTransformation(pRawCommand, pRegexMatch);

            #region Attributes

            string mAttributes = pRegexMatch.Groups["attributes"].Captures[0].Value;

            var mAttributesRaw = mAttributes.Split(',').ToList<string>();

            this.Attributes = new List<string>();

            foreach (var mAttribute in mAttributesRaw)
                this.Attributes.Add(mAttribute.Trim());

            #endregion

            #region Values

            string mValues = pRegexMatch.Groups["values"].Captures[0].Value;

            var mValuesRaw = mValues.Split(',');

            this.Values = new List<string>();

            foreach (var mValue in mValuesRaw)
                this.Values.Add(mValue.Trim().Replace("'", ""));

            #endregion

            this.ItemID = Guid.NewGuid().ToString();
        }
    }
}