using System;

namespace RulesEngine.Rule.Parser
{
    public class RuleParseException : Exception
    {
        public RuleParseException(string s) : base(s)
        {
        }
    }
}