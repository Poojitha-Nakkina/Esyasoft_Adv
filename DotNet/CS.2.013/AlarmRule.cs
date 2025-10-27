using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS._2._013
{
    internal abstract class AlarmRule
    {
        public string Name { get; }
        protected AlarmRule(string name) => Name = name;
        public abstract bool IsTriggered(LoadProfileDay day);
        public virtual string Message(LoadProfileDay day)
            => $"{Name} triggered on {day.Date:yyyy-MM-dd}";
    }

    class PeakOveruseRule : AlarmRule
    {   // trigger if day.Total > threshold
        private readonly int _threshold;
        public PeakOveruseRule(int threshold) : base("PeakOveruse") => _threshold = threshold;
        public override bool IsTriggered(LoadProfileDay day) => day.Total > _threshold;
    }

    class SustainedOutageRule : AlarmRule
    {   // trigger if consecutive zero hours >= N
        private readonly int _minConsecutive;
        public SustainedOutageRule(int min) : base("SustainedOutage") => _minConsecutive = min;
        public override bool IsTriggered(LoadProfileDay day) {
            int count = 0;
            int max = 0;
            foreach(var kwh in day.HourlyKwh) {
                if(kwh == 0) {
                    count++;
                    max = Math.Max(max, count);
                    if(max >= _minConsecutive) return true;
                } else {
                    count = 0;
                }
               
            }
            return false;

        }

    }
}
