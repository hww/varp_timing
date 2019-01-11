
using System;
namespace Code.Timing
{
    public static class UnixTime
    {
        /// <summary>
        /// Contains EPOC time
        /// </summary>
        public static DateTime Epoc = new DateTime(1970, 1, 1, 0, 0, 0, 0); 

        /// <summary>
        /// Convert from UNIX time stamp to the DataTime
        /// </summary>
        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            return Epoc.AddMilliseconds(timestamp);
        }

        /// <summary>
        /// Convert DataTime to the UNIX format
        /// </summary>
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            var diff = date - Epoc;
            return Math.Floor(diff.TotalMilliseconds);
        }
    }
}