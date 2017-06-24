using System.Collections.Generic;
using System.Text.RegularExpressions;
using RulesEngine.Model;
using RulesEngine.Rule.Validator;

namespace RulesEngine.Rule.Parser
{
    public class RuleParser : IRuleParser
    {
        private const string Signalsourceid = "signalSourceId";
        private const string Limit = "limit";
        private const string InvalidValue = "limit";
        private const string InvalidTime = "time";
        private readonly IDictionary<Error, Regex> _ruleDictionary;

        public RuleParser()
        {
            _ruleDictionary = new Dictionary<Error, Regex>
            {
                {
                    Error.SignalLimitExceeded,
                    new Regex(string.Format(@"(?<{0}>\w+) value should not rise above (?<{1}>\d+)", Signalsourceid,
                        Limit))
                },
                {
                    Error.SignalRangeViolation,
                    new Regex(string.Format(@"(?<{0}>\w+) value should never be (?<{1}>\w+)", Signalsourceid,
                        InvalidValue))
                },
                {
                    Error.SignalTimeViolation,
                    new Regex(string.Format(@"(?<{0}>\w+) should not be in (?<{1}>\w+)", Signalsourceid,
                        InvalidTime))
                }
            };
        }

        public IValidator Parse(string data, IValidator nextValidator)
        {
            foreach (var rule in _ruleDictionary)
            {
                var validationCode = rule.Key;
                var regex = rule.Value;
                Match match;
                string signalSourceId;
                switch (validationCode)
                {
                    case Error.SignalLimitExceeded:
                        match = regex.Match(data);
                        if (!match.Success) continue;
                        signalSourceId = match.Groups[Signalsourceid].Value;
                        var limit = double.Parse(match.Groups[Limit].Value);
                        return new SignalLimitValidator(signalSourceId, limit, nextValidator);

                    case Error.SignalRangeViolation:
                        match = regex.Match(data);
                        if (!match.Success) continue;
                        signalSourceId = match.Groups[Signalsourceid].Value;
                        var invalidValue = match.Groups[Limit].Value;
                        return new SignalRangeValidator(signalSourceId, invalidValue, nextValidator);

                    case Error.SignalTimeViolation:
                        match = regex.Match(data);
                        if (!match.Success) continue;
                        signalSourceId = match.Groups[Signalsourceid].Value;
                        var timeString = match.Groups[InvalidTime].Value;
                        return new SignalTimeValidator(signalSourceId, TimeExtension.Parse(timeString), nextValidator);

                }
            }
            throw new RuleParseException("Unable to parse the rule - " + data);
        }
    }
}