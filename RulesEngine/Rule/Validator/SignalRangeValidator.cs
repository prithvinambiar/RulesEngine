using RulesEngine.Model;

namespace RulesEngine.Rule.Validator
{
    public class SignalRangeValidator : AbstractSignalValidator
    {
        private readonly string _invalidValue;

        public SignalRangeValidator(string signalSourceId, string invalidValue, IValidator nextValidator)
            : base(signalSourceId, nextValidator)
        {
            _invalidValue = invalidValue;
        }

        protected override Notification DoValidate(DataUnit dataUnit, Notification notification)
        {
            if (dataUnit.Value.Equals(_invalidValue))
            {
                notification.AddError(Error.SignalRangeViolation, dataUnit.ToString());
            }
            return notification;
        }
    }
}