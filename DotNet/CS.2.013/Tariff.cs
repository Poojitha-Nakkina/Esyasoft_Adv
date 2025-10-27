using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS._2._013
{
    internal class Tariff
    {

        public string Name;
        public double RatePerKwh;
        public double FixedCharge;

        public Tariff(string name)
        {
            Name = name;
            RatePerKwh = 6.0;
            FixedCharge = 50;
        }

        public Tariff(string name, double rate)
        {
            Name = name;
            RatePerKwh = rate;
            FixedCharge = 50;
            Validate();
        }

        public Tariff(string name, double rate, double fixedCharge)
        {
            Name = name;
            RatePerKwh = rate;
            FixedCharge = fixedCharge;
            Validate();
        }

        public void ComputeBill(int units)
        {
            double total = (int)(units * RatePerKwh + FixedCharge);
            Console.WriteLine($"{Name}: {total}");
        }

        public void Validate()
        {
            if(RatePerKwh <= 0)
            {
                throw new ArgumentException("Rate per Kwh cannot be negative");
            }
            if(FixedCharge < 0)
            {
                throw new ArgumentException("Fixed charge cannot be negative");
            }
        }

    }
}
