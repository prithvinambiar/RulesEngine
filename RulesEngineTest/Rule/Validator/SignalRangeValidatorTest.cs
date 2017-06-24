using System.Linq;
using NUnit.Framework;
using RulesEngine.Model;
using RulesEngine.Rule.Validator;

namespace RulesEngineTest.Rule.Validator
{
    public class SignalRangeValidatorTest
    {
        private IValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new SignalRangeValidator("ALT1", "LOW", new StopChainValidator());
        }

        [Test]
        public void should_notify_error_when_signal_value_is_out_of_range()
        {
            var notification = _validator.Validate(new DataUnit("ALT1", "LOW", ValueType.String), new Notification());
            Assert.AreEqual(Error.SignalRangeViolation, notification.GetErrors().First());
        }
        
        [Test]
        public void should_not_notify_error_when_value_is_valid()
        {
            var notification = _validator.Validate(new DataUnit("DifferentSourceId", "HIGH", ValueType.String), new Notification());
            Assert.IsEmpty(notification.GetErrors());
        }
        
        [Test]
        public void should_not_notify_error_for_different_signal_when_value_is_invalid()
        {
            var notification = _validator.Validate(new DataUnit("DifferentSourceId", "LOW", ValueType.String), new Notification());
            Assert.IsEmpty(notification.GetErrors());
        }
    }
}