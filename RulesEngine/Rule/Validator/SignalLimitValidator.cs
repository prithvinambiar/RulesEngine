using RulesEngine.Model;

namespace RulesEngine.Rule.Validator
{
    public class SignalLimitValidator : AbstractSignalValidator
    {
        private readonly double _limit;

        public SignalLimitValidator(string signalSourceId, double limit, IValidator nextValidator)
            : base(signalSourceId, nextValidator)
        {
            _limit = limit;
        }

        protected override Notification DoValidate(DataUnit dataUnit, Notification notification)
        {
            if (dataUnit.IsExceedingValue(_limit))
            {
                notification.AddError(Error.SignalLimitExceeded, dataUnit.ToString());
            }

            return notification;
        }
    }
}