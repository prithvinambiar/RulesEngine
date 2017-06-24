using System;
using System.Linq;
using NUnit.Framework;
using RulesEngine.Model;
using RulesEngine.Rule.Validator;
using ValueType = RulesEngine.Model.ValueType;

namespace RulesEngineTest.Rule.Validator
{
    public class SignalTimeValidatorTest
    {
        private const string DateTimeFormat = "dd/MM/yyyy";
        private IValidator _futureSignalValidator;
        private IValidator _pastValidator;
        private IValidator _presentValidator;
        private DataUnit _futureDataUnit;
        private DataUnit _pastDataUnit;
        private DataUnit _presentDataUnit;

        [SetUp]
        public void SetUp()
        {
            _pastValidator = new SignalTimeValidator("ALT1", Time.Past, new StopChainValidator());
            _presentValidator = new SignalTimeValidator("ALT1", Time.Present, new StopChainValidator());
            _futureSignalValidator = new SignalTimeValidator("ALT1", Time.Future, new StopChainValidator());
            var futureDate = DateTime.Now.AddDays(10).ToString(DateTimeFormat);
            _futureDataUnit = new DataUnit("ALT1", futureDate, ValueType.DateTime);
            var presentDate = DateTime.Now.ToString(DateTimeFormat);
            _presentDataUnit = new DataUnit("ALT1", presentDate, ValueType.DateTime);
            var pastDate = DateTime.Now.AddDays(-10).ToString(DateTimeFormat);
            _pastDataUnit = new DataUnit("ALT1", pastDate, ValueType.DateTime);
        }

        [Test]
        public void should_notify_error_for_future_signals_when_the_validator_is_configured_for_that()
        {
            var notification = _futureSignalValidator.Validate(_futureDataUnit, new Notification());
            Assert.AreEqual(Error.SignalTimeViolation, notification.GetErrors().First());

            notification = _futureSignalValidator.Validate(_pastDataUnit, new Notification());
            Assert.IsEmpty(notification.GetErrors());
        }

        [Test]
        public void should_notify_error_for_present_signals_when_the_validator_is_configured_for_that()
        {
            var notification = _presentValidator.Validate(_presentDataUnit, new Notification());
            Assert.AreEqual(Error.SignalTimeViolation, notification.GetErrors().First());

            notification = _presentValidator.Validate(_pastDataUnit, new Notification());
            Assert.IsEmpty(notification.GetErrors());
        }

        [Test]
        public void should_notify_error_for_past_signals_when_the_validator_is_configured_for_that()
        {
            var notification = _pastValidator.Validate(_pastDataUnit, new Notification());
            Assert.AreEqual(Error.SignalTimeViolation, notification.GetErrors().First());

            notification = _pastValidator.Validate(_futureDataUnit, new Notification());
            Assert.IsEmpty(notification.GetErrors());
        }

        [Test]
        public void should_not_notify_error_for_different_signal()
        {
            var futureDate = DateTime.Now.AddDays(10).ToString(DateTimeFormat);
            var dataUnit = new DataUnit("DifferentSourceId", futureDate, ValueType.DateTime);
            var notification = _pastValidator.Validate(dataUnit, new Notification());
            Assert.IsEmpty(notification.GetErrors());
        }
    }
}