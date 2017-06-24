using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using RulesEngine.Model;

namespace RulesEngine.RawData.Parser
{
    public class DataBuilder
    {
        private readonly string _rawData;

        public DataBuilder(string rawData)
        {
            _rawData = rawData;
        }

        public IEnumerable<DataUnit> Build()
        {
            var djs = new DataContractJsonSerializer(typeof(List<RawDataUnit>));
            var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(_rawData));
            var rawDataUnits = (List<RawDataUnit>)djs.ReadObject(memoryStream);
            return rawDataUnits.Select(ToDataUnit);
        }

        private static DataUnit ToDataUnit(RawDataUnit rawDataUnit)
        {
            var valueType = ValueTypeExtension.Parse(rawDataUnit.ValueType);
            return new DataUnit(rawDataUnit.SignalSourceId, rawDataUnit.Value, valueType);
        }
    }
}
