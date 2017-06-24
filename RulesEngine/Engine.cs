using RulesEngine.Model;
using RulesEngine.RawData.Parser;
using RulesEngine.Rule.Parser;

namespace RulesEngine
{
    public class Engine
    {
        public static Notification Run(string rawData, string rules)
        {
            var dataBuilder = new DataBuilder(rawData);
            var dataUnits = dataBuilder.Build();

            var ruleBuilder = new RuleBuilder(rules, new RuleParser());
            var validator = ruleBuilder.Build();

            var notification = new Notification();
            foreach (var dataUnit in dataUnits)
            {
                notification = validator.Validate(dataUnit, notification);
            }
            return notification;
        }
    }
}