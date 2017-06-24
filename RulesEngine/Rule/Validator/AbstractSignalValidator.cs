using RulesEngine.Model;

namespace RulesEngine.Rule.Validator
{
    public abstract class AbstractSignalValidator : IValidator
    {
        private readonly string _signalSourceId;
        private readonly IValidator _nextValidator;

        protected AbstractSignalValidator(string signalSourceId, IValidator nextValidator)
        {
            _signalSourceId = signalSourceId;
            _nextValidator = nextValidator;
        }

        public Notification Validate(DataUnit dataUnit, Notification notification)
        {
            notification = dataUnit.SignalSourceId.Equals(_signalSourceId)
                ? DoValidate(dataUnit, notification)
                : notification;
            return _nextValidator.Validate(dataUnit, notification);
        }

        protected abstract Notification DoValidate(DataUnit dataUnit, Notification notification);
    }
}