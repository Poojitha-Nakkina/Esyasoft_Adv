using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS._2._013
{
    internal class meter
    {
        private string MeterSerial;
        private string Location;
        private DateTime InstalledOn;
        private int LastReadingKwh;

       public string meterSerial
        {
            get => MeterSerial; 
            set => MeterSerial = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Meter serial cannot be empty");
        }

        public string location
        {
            get => Location;
            set => Location = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Location cannot be empty");
        }

        public DateTime installedOn
        {
            get => InstalledOn;
            set => InstalledOn = value ;
        }

        public int lastReadingKwh
        {
            get => LastReadingKwh;
             set
            {
                if(value >= 0)
                {
                    LastReadingKwh = value;
                }
                else
                {
                    throw new ArgumentException("reading cannot be negative");
                }
            }
        }
        public void AddReading(int deltaKwh)
        {
            if (deltaKwh > 0)
            {
                LastReadingKwh += deltaKwh;
            }

        }

        public string Summary()
        {
            return $"{MeterSerial} Location: {Location} | Reading : {LastReadingKwh} ";
        }

        public override string ToString()
        {
            return Summary();
        }
    }
}
