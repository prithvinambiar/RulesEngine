using System;
using System.Linq;
using NUnit.Framework;
using RulesEngine.Model;
using RulesEngine.Rule.Parser;
using RulesEngine.Rule.Validator;
using ValueType = RulesEngine.Model.ValueType;

namespace RulesEngineTest.Rule.Parser
{
    internal class RuleParserTest
    {
        private IValidator _nextValidator;
        private const string DateTimeFormat = "dd/MM/yyyy";

        [SetUp]
        public void SetUp()
        {
            _nextValidator = new StopChainValidator();
        }

        [Test]
        public void should_build_signal_limit_validator()
        {
            IRuleParser ruleParser = new RuleParser();
            var validator = ruleParser.Parse("ATL1 value should not rise above 240.00", _nextValidator);
            Assert.IsTrue(validator is SignalLimitValidator);

            var notification = validator.Validate(new DataUnit("ATL1", "290.0", ValueType.Integer), new Notification());
            Assert.That(notification.GetErrors().First(), Is.EqualTo(Error.SignalLimitExceeded));
            Assert.That(notification.GetErrorMessages().First(),
                Is.EqualTo(@"{""signal"": ""ATL1"", ""value_type"": ""Integer"", ""value"": ""290.0""}"));
        }

        [Test]
        public void should_build_signal_range_validator()
        {
            IRuleParser ruleParser = new RuleParser();
            var validator = ruleParser.Parse("ATL2 value should never be LOW", _nextValidator);
            Assert.IsTrue(validator is SignalRangeValidator);

            var notification = validator.Validate(new DataUnit("ATL2", "LOW", ValueType.String), new Notification());
            Assert.That(notification.GetErrors().First(), Is.EqualTo(Error.SignalRangeViolation));
        }

        [Test]
        public void should_build_signal_time_validator()
        {
            IRuleParser ruleParser = new RuleParser();
            var validator = ruleParser.Parse("ATL3 should not be in future", _nextValidator);
            Assert.IsTrue(validator is SignalTimeValidator);

            var futureDate = DateTime.Now.AddDays(1).ToString(DateTimeFormat);
            var notification = validator.Validate(new DataUnit("ATL3", futureDate, ValueType.DateTime), new Notification());
            Assert.That(notification.GetErrors().First(), Is.EqualTo(Error.SignalTimeViolation));
        }

        [Test]
        [ExpectedException(typeof (RuleParseException))]
        public void should_throw_rule_parse_exception_for_invalid_rules()
        {
            IRuleParser ruleParser = new RuleParser();
            ruleParser.Parse("gibberish rule", _nextValidator);
        }
    }
}