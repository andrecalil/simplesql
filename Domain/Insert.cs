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

        public static string CommandRegex = @"^(?:\s*(?i:INSERT INTO)\s+)(?<table>[^\(]+)(?:\(\s*)(?<attributes>(?:\w+\s*)(?:\,\s*\w+\s*)*)(?:\)\s+)(?:(?i:VALUES)\s+\(\s*)(?<values>(?:[^\,]+|[^\)])+\s*(?:\,\s*[^\)])*)(?:\))$";

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

            this.Attributes = mAttributes.Split(',').ToList<string>();
            this.Attributes.ForEach(x => x = x.Trim());

            #endregion

            #region Values

            //TODO: value may contain a comma (,), like in a string 'stuff,stuff'
            string mValues = pRegexMatch.Groups["values"].Captures[0].Value;
            string[] mValuesArray = mValues.Split(',');

            this.Values = mValues.Split(',').ToList<string>();
            this.Values.ForEach(x => x = x.Replace("'","").Trim());

            #endregion

            this.ItemID = Guid.NewGuid().ToString();
        }
    }
}