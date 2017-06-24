using System.Collections.Generic;
using System.Linq;

namespace RulesEngine.Model
{
    public class Notification
    {
        private readonly IList<ErrorMessage> _errors;

        public Notification()
        {
            _errors = new List<ErrorMessage>();
        }

        public IList<Error> GetErrors()
        {
            return _errors.Select(message => message.Error).ToList();
        }

        public IList<string> GetErrorMessages()
        {
            return _errors.Select(message => message.Message).ToList();
        }

        public void AddError(Error error, string message)
        {
            _errors.Add(new ErrorMessage(error, message));
        }
    }
}