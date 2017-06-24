using System;

namespace RulesEngine.Model
{
    public enum ValueType
    {
        Integer,
        String,
        DateTime
    }

    static class ValueTypeExtension
    {
        public static ValueType Parse(string valueTypeString)
        {
            switch (valueTypeString.ToLowerInvariant())
            {
                case "integer":
                    return ValueType.Integer;
                case "string":
                    return ValueType.String;
                case "datetime":
                    return ValueType.DateTime;
                default:
                    throw new ArgumentException("Invalid value type string");
            }
        }
    }
}