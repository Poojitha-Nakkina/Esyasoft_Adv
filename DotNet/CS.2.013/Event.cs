using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS._2._013
{
    internal abstract class Event
    {
        public DateTime When { get; }
        public string MeterSerial { get; }
        protected Event(DateTime when, string meterSerial) { When = when; MeterSerial = meterSerial; }
        public abstract string Category { get; }
        public abstract int Severity { get; } // 1..5
        public virtual string Describe() => $"{When:yyyy-MM-dd HH:mm} [{Category}] {MeterSerial}";
    }

    class OutageEvent : Event
    {
        public override string Category => "OUTAGE";
        public override int Severity => 3;
        public int DurationMinutes { get; }

        public OutageEvent(DateTime when, string meterSerial, int durationMinutes) : base(when, meterSerial)
        {
            DurationMinutes = durationMinutes;
        }

        public override string Describe()
        {
            return base.Describe() + $" Duration: {DurationMinutes}  Severity: {Severity}";
        }
    }

    class TamperEvent : Event
    {
        public override string Category => "TAMPER";
        public override int Severity => 5;
        public string Code { get; }

        public TamperEvent(DateTime when, string meterSerial, string code) : base(when, meterSerial)
        {
            Code = code;
        }

        public override string Describe()
        {
            return base.Describe() + $" Code: {Code}  Severity:{Severity}";
        }
    }

    class VoltageEvent : Event
    {
        public override string Category => "VOLTAGE";
        public override int Severity => 2;
        public double Voltage { get; }

        public VoltageEvent(DateTime when, string meterSerial, double voltage) : base(when, meterSerial)
        {
            Voltage = voltage;
        }

        public override string Describe()
        {
            return base.Describe() + $" Voltage: {Voltage} Severity:{Severity}";
        }

    }
    class EventProcessor
    {
        public static void PrintTopSevere(IEnumerable<Event> events, int topN)
        {
            // sort by Severity desc, then When desc; print Describe() + extra fields polymorphically
            var topEvents = events.OrderByDescending(e => e.Severity).ThenByDescending(e => e.When).Take(topN);
            foreach (var ev in topEvents)
            {
                Console.WriteLine(ev.Describe());
            }

        }
    }

}
