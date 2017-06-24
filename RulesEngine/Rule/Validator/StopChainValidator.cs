using RulesEngine.Model;

namespace RulesEngine.Rule.Validator
{
    public class StopChainValidator : IValidator
    {
        public Notification Validate(DataUnit dataUnit, Notification notification)
        {
            return notification;
        }
    }
}