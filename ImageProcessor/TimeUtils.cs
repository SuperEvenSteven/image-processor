using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor {
    public static class TimeUtils {
        /// <summary>
        /// As name suggests.
        /// </summary>
        public static long GetCurrentTimeSecondsSinceEpoch() {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return (long)t.TotalSeconds;
        }

        /// <summary>
        /// As name suggests.
        /// </summary>
        public static long GetCurrentTimeMillisSinceEpoch() {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return (long)t.TotalMilliseconds;
        }

        /// <summary>
        /// As name suggests.
        /// </summary>
        internal static long GetTimeElapsedSecondsSince(long startTimeSecs) {
            return GetCurrentTimeSecondsSinceEpoch() - startTimeSecs;
        }


        /// <summary>
        /// As name suggests.
        /// </summary>
        internal static long GetTimeElapsedMillisSince(long startTimeMillis) {
            long now = GetCurrentTimeMillisSinceEpoch();
            long elapsedTimeMills = now - startTimeMillis;
            return elapsedTimeMills;
        }


        /// <summary>
        /// As name suggests.
        /// </summary>
        internal static string GetFormattedDurationFromSeconds(long secs) {
            TimeSpan t = TimeSpan.FromSeconds(secs);
            string s = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);
            return s;
        }

        /// <summary>
        /// As name suggests.
        /// </summary>
        internal static string GetFormattedDurationFromMillis(long millis) {
            TimeSpan t = TimeSpan.FromMilliseconds(millis);
            string s = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);
            return s;
        }

        /// <summary>
        /// Returns time now formatted to 'yyMMddHmmss'. i.e. 20171001170059
        /// </summary>
        internal static string GetDateTimeFilenameSuffix() {
            return DateTime.Now.ToString("yyyyMMddHmmss"); ;
        }
    }
}

