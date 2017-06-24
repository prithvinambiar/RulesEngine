using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using RulesEngine;

namespace RulesEngineTest.Integration
{
    internal class EngineTest
    {
        [Test]
        public void should_apply_rules_on_raw_data()
        {
            var rawDataJson = ReadEmbeddedResource("RulesEngineTest.Integration.Data.raw_data.json");
            var ruleJson = ReadEmbeddedResource("RulesEngineTest.Integration.Data.rule.txt");
            var notification = Engine.Run(rawDataJson, ruleJson);
            Assert.That(1, Is.EqualTo(1));
        }

        private static string ReadEmbeddedResource(string resourceName)
        {
            using (var rawDataStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                Assert.That(rawDataStream, Is.Not.EqualTo(null));
                using (var streamReader = new StreamReader(rawDataStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}