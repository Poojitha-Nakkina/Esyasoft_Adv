using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS._2._013
{
    internal class Device
    {
        public string Id;
        public DateOnly InstalledOn;

        protected Device(string id, DateOnly installedOn) {
            Id = id;
            InstalledOn = installedOn;
        }

        virtual public void Describe()
        {
            Console.WriteLine($"Device ID: {Id}, Installed On: {InstalledOn}");
        }
    }

    internal class Meter : Device
    {
        public int PhaseCount;

        public Meter(string id, DateOnly installedOn, int phaseCount) : base(id, installedOn) 
        {
            PhaseCount = phaseCount;
        }
        public override void Describe()
        {
            Console.WriteLine($"Meter ID: {Id} | Installed On: {InstalledOn} | Phase Count: {PhaseCount}");
        }

    }

    internal class Gateway : Device
    {
        public string IpAddress;

        public Gateway(string id, DateOnly installedOn, string ipAddress) : base(id, installedOn)
        {
            IpAddress = ipAddress;
        }
        public override void Describe()
        {
            Console.WriteLine($"Gateway ID: {Id} | Installed On: {InstalledOn} | IP Address: {IpAddress}");
        }
    }
}
