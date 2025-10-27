using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS._2._013
{
    internal class LoadProfileDay
    {
        public DateTime Date { get; }
        public int[] HourlyKwh { get; } // length 24
        public LoadProfileDay(DateTime date, int[] hourly)
        {
            // clone array; validate length == 24; values >= 0
           HourlyKwh = hourly;
              Date = date;
        }
        public int Total => HourlyKwh.Sum(); /* sum */
        public int PeakHour => Array.IndexOf(HourlyKwh, HourlyKwh.Max()); /* 0..23 of max */
    }
}
