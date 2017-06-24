using System.Linq;
using NUnit.Framework;
using RulesEngine.Model;
using RulesEngine.RawData.Parser;

namespace RulesEngineTest.RawData.Parser
{
    internal class DataBuilderTest
    {
        [Test]
        public void should_parse_data_unit_from_raw_json_data()
        {
            const string rawJsonData = "[{\"signal\": \"ATL2\", \"value_type\": \"String\", \"value\": \"HIGH\"}]";
            var dataUnit = new DataBuilder(rawJsonData).Build().First();

            Assert.That(dataUnit.SignalSourceId, Is.EqualTo("ATL2"));
            Assert.That(dataUnit.ValueType, Is.EqualTo(ValueType.String));
            Assert.That(dataUnit.Value, Is.EqualTo("HIGH"));
        }
    }
}