using System;
using RulesEngine.Rule.Validator;

namespace RulesEngine.Rule.Parser
{
    class RuleBuilder
    {
        private readonly string _rules;
        private readonly IRuleParser _parser;

        public RuleBuilder(string rules, IRuleParser parser)
        {
            _rules = rules;
            _parser = parser;
        }

        public IValidator Build()
        {
            var newLine = Environment.NewLine;
            var lines = _rules.Split(new[] { newLine }, StringSplitOptions.None);
            IValidator nextValidator = new StopChainValidator();
            foreach (var line in lines)
            {
                nextValidator = Parse(line, nextValidator);
            }
            return nextValidator;
        }

        private IValidator Parse(string line, IValidator nextValidator)
        {
            return _parser.Parse(line, nextValidator);
        }
    }
}
