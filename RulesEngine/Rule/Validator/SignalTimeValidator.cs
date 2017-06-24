using System;
using RulesEngine.Model;

namespace RulesEngine.Rule.Validator
{
    public class SignalTimeValidator : AbstractSignalValidator
    {
        private readonly Time _invalidTime;

        public SignalTimeValidator(string signalSourceId, Time invalidTime, IValidator nextValidator) : base(signalSourceId, nextValidator)
        {
            _invalidTime = invalidTime;
        }

        protected override Notification DoValidate(DataUnit dataUnit, Notification notification)
        {
            switch (_invalidTime)
            {
                case Time.Past:
                    if (dataUnit.IsPast())
                    {
                        notification.AddError(Error.SignalTimeViolation, dataUnit.ToString());
                    }
                    break;

                case Time.Present:
                    if (dataUnit.IsPresent())
                    {
                        notification.AddError(Error.SignalTimeViolation, dataUnit.ToString());
                    }
                    break;
                    
                case Time.Future:
                    if (dataUnit.IsFuture())
                    {
                        notification.AddError(Error.SignalTimeViolation, dataUnit.ToString());
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return notification;
        }
    }
}