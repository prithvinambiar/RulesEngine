using System;
using System.Globalization;

namespace RulesEngine.Model
{
    public class DataUnit
    {
        private const string DateTimeFormat = "dd/MM/yyyy";

        public string SignalSourceId { get; private set; }
        public string Value { get; private set; }
        public ValueType ValueType { get; private set; }

        public DataUnit(string signalSourceId, string value, ValueType valueType)
        {
            SignalSourceId = signalSourceId;
            Value = value;
            ValueType = valueType;
        }

        public bool IsExceedingValue(double limit)
        {
            if (ValueType != ValueType.Integer) return false;

            double intValue;
            var isValidInteger = double.TryParse(Value, out intValue);
            if (isValidInteger)
            {
                return intValue > limit;
            }
            return false;
        }

        public bool IsPast()
        {
            if (ValueType != ValueType.DateTime)
            {
                return false;
            }
            return GetDateTime(Value) < GetCurrentDateTime();
        }

        public bool IsPresent()
        {
            if (ValueType != ValueType.DateTime)
            {
                return false;
            }
            return GetDateTime(Value) == GetCurrentDateTime();
        }

        public bool IsFuture()
        {
            if (ValueType != ValueType.DateTime)
            {
                return false;
            }
            return GetDateTime(Value) > GetCurrentDateTime();
        }

        private static DateTime? GetCurrentDateTime()
        {
            return GetDateTime(DateTime.Now.ToString(DateTimeFormat));
        }

        private static DateTime? GetDateTime(string value)
        {
            DateTime dateTime;
            DateTime.TryParseExact(value, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dateTime);
            return dateTime;
        }

        public override string ToString()
        {
            return string.Format("{{\"signal\": \"{0}\", \"value_type\": \"{1}\", \"value\": \"{2}\"}}", SignalSourceId, ValueType, Value);
        }
    }
}