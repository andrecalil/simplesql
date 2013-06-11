using System;

namespace SimpleSQL.Domain
{
    public enum EnumOperator
    {
        Equal = 1, Different, LessThan, GreaterThan, LessEqualThan, GreaterEqualThan, In, NotIn, Like
    }

    public static class EnumOperatorUtils
    {
        public static string ToCommand(this EnumOperator pOperator)
        {
            switch(pOperator)
            {
                case EnumOperator.Equal: return "=";
                case EnumOperator.Different: return "<>";
                case EnumOperator.LessThan: return "<";
                case EnumOperator.GreaterThan: return ">";
                case EnumOperator.LessEqualThan: return "<=";
                case EnumOperator.GreaterEqualThan: return ">=";
                case EnumOperator.In: return "IN";
                case EnumOperator.NotIn: return "NOT IN";
                case EnumOperator.Like: return "LIKE";
                default: return string.Empty;
            }
        }

        public static bool IsListOperator(this EnumOperator pOperator)
        {
            switch(pOperator)
            {
                case EnumOperator.In: 
                case EnumOperator.NotIn: return true;
                default: return false;
            }
        }

        public static EnumOperator FromCommand(string pCommandOperator)
        {
            foreach (EnumOperator mSupportedOperator in System.Enum.GetValues(typeof(EnumOperator)))
            {
                if(mSupportedOperator.ToCommand().Equals(pCommandOperator.ToUpper().Trim()))
                {
                    return mSupportedOperator;
                }
            }

            throw new Exception(string.Format("SimpleSQL: unsupported operator. Operator: {0}",pCommandOperator));
        }
    }
}