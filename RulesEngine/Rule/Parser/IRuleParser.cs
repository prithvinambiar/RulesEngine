using RulesEngine.Rule.Validator;

namespace RulesEngine.Rule.Parser
{
    public interface IRuleParser
    {
        IValidator Parse(string data, IValidator nextValidator);
    }
}
