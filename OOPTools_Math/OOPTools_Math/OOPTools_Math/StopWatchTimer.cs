using System;
using System.Collections.Generic;
using System.Text;

namespace OOPTools
{
    [Serializable]
    public class StopWatchTimer
    {
        private DateTime startTime;
        private DateTime endTime;
        public StopWatchTimer()
        {
            startTime = new DateTime();
            endTime = new DateTime();
        }
        /// <summary>
        /// Run to start timer
        /// </summary>
        public void StartTimer()
        {
            startTime = DateTime.Now;
        }
        /// <summary>
        /// Run to stop timer
        /// </summary>
        public void StopTimer()
        {
            endTime = DateTime.Now;
        }
        public double TimeElapsedInSeconds()
        {
            TimeSpan ts = new TimeSpan();
            ts = endTime - startTime;
            return ts.TotalSeconds;
        }
        public double TimeElapsedInMiliseconds()
        {
            TimeSpan ts = new TimeSpan();
            ts = endTime - startTime;
            return ts.TotalMilliseconds;
        }
        public double TimeElapsedInMinutes()
        {
            TimeSpan ts = new TimeSpan();
            ts = endTime - startTime;
            return ts.TotalMinutes;
        }
        public double TimeElapsedInHours()
        {
            TimeSpan ts = new TimeSpan();
            ts = endTime - startTime;
            return ts.TotalHours;
        }
    }
}
