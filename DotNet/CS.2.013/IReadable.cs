using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS._2._013
{
    internal interface IReadable
    {
        int ReadKwh();             
        string SourceId { get; }
    }

    public class DlmsMeter : IReadable
    {
        public int ReadKwh()
        {
            Random rand = new Random();
            return rand.Next(0, 10);
        }
        public string SourceId => "AP-0001";




    }   

    public class ModemGateway: IReadable
    {
        public int ReadKwh()
        {
            Random rand = new Random();
            return rand.Next(0, 2);
        }

        public string SourceId => "GW-21";
    }
}
