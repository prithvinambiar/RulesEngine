using System;

namespace RulesEngine.Model
{
    public enum Time
    {
        Past,
        Present,
        Future

    }

    static class TimeExtension
    {
        public static Time Parse(string timeString)
        {
            switch (timeString.ToLowerInvariant())
            {
                case "past" :
                    return Time.Past;
                case "present" :
                    return Time.Present;
                case "future" :
                    return Time.Future;
                default:
                    throw new ArgumentException("Invalid time string");
            }
        }
    }
}