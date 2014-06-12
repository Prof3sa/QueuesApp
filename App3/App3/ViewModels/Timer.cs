using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace App3.ViewModels
{
    class Timer
    {

        private Stopwatch stopWatch;
        private long timeLeft;

        public Timer(double time)
        {
            stopWatch = new Stopwatch();
            timeLeft = ConvertToMilliSeconds(time);
            stopWatch.Start();
        }

        private long ConvertToMilliSeconds(double minutes)
        {
            double t = minutes * 60 * 1000;
            return (long)Math.Round(t);
        }

        private int ConvertToMinutes(long milliseconds)
        {
            long m = milliseconds / 1000;
            m = m / 60;
            return (int)m;
        }

        public int GetMinutesLeft()
        {
            long elapsed = stopWatch.ElapsedMilliseconds;
            long remaining = timeLeft - elapsed;
            if (remaining <= 0)
                stopWatch.Stop();
            return ConvertToMinutes(remaining);
        }



    }
}
