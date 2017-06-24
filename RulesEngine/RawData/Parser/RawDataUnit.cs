using System.Runtime.Serialization;

namespace RulesEngine.RawData.Parser
{
    [DataContract]
    internal class RawDataUnit
    {
        [DataMember(Name = "signal")] public string SignalSourceId { get; private set; }

        [DataMember(Name = "value_type")] public string ValueType { get; private set; }

        [DataMember(Name = "value")] public string Value { get; private set; }

        public RawDataUnit(string signalSourceId, string valueType, string value)
        {
            SignalSourceId = signalSourceId;
            ValueType = valueType;
            Value = value;
        }
    }
}