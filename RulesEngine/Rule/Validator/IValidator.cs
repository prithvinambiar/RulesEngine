using RulesEngine.Model;

namespace RulesEngine.Rule.Validator
{
    public interface IValidator
    {
        Notification Validate(DataUnit dataUnit, Notification notification);
    }
}