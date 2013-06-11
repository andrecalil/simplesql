using System;
using System.Collections.Generic;

namespace SimpleSQL.Domain
{
    public class Condition
    {
        public String Attribute { get; set; }
        public List<Object> Values { get; set; }
        public EnumOperator Operator { get; set; }
        public Condition Or { get; set; }

        public string SingleNameAttribute
        {
            get
            {
                return this.Attribute.Contains(".") ? this.Attribute.Split('.')[1] : this.Attribute;
            }
        }
    }
}