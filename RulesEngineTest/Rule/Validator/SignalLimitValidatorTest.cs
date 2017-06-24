using System.Linq;
using NUnit.Framework;
using RulesEngine.Model;
using RulesEngine.Rule.Validator;

namespace RulesEngineTest.Rule.Validator
{
    public class SignalLimitValidatorTest
    {
        private IValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new SignalLimitValidator("ATL1", 240.00, new StopChainValidator());
        }

        [Test]
        public void should_notify_error_when_signal_value_exceeds_specified_limit()
        {
            var notification = _validator.Validate(new DataUnit("ATL1", "241", ValueType.Integer), new Notification());
            Assert.AreEqual(Error.SignalLimitExceeded, notification.GetErrors().First());
        }
        
        [Test]
        public void should_not_notify_error_when_signal_value_doesnot_exceed_specified_limit()
        {
            var notification = _validator.Validate(new DataUnit("ATL1", "240", ValueType.Integer), new Notification());
            Assert.AreEqual(0, notification.GetErrors().Count);
        }

        [Test]
        public void should_not_notify_error_for_different_signal()
        {
            var dataUnit = new DataUnit("DifferentSourceId", "234", ValueType.Integer);
            var notification = _validator.Validate(dataUnit, new Notification());
            var errors = notification.GetErrors();
            Assert.AreEqual(0, errors.Count);
        }
    }
}