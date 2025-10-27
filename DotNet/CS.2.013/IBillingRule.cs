using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CS._2._013
{
    internal interface IBillingRule
    {
        double Compute(int units);
    }

    public class DomesticRule : IBillingRule
    {


        public double Compute(int units)
        {
            return units * 6.0 + 50;
        }

    }

    public class CommercialRule : IBillingRule
    {
        public double Compute(int units)
        {
            return units * 8.5 + 150;
        }
    }
    public class AgricultureRule : IBillingRule
    {
        public double Compute(int units)
        {
            return units * 3.0;
        }
    }

    class BillingEngine
    {
        private IBillingRule _billingRule;
        public BillingEngine(IBillingRule billingRule)
        {
            _billingRule = billingRule;
        }
        public double GenerateBill(int units)
        {
            return _billingRule.Compute(units);
        }
    }

    public  interface IRebate
    {
        string Code { get; }
        double Apply(double currentTotal, int outageDays);
    }

    public class NoOutageRebate: IRebate
    {
        public string Code => "NO_OUTAGE";
        public double Apply(double currentTotal, int outageDays)
        {
            if(outageDays ==0 ) return -0.02 * currentTotal;
            return 0;
        }
        
    }
    public class HighUsageRebate: IRebate
    {
        public string Code => "High_Usage";
        public double Apply(double currentTotal, int outageDays)
        {
            if(currentTotal > 500)
                return -0.03* currentTotal;
            return 0;
        }

    }

    class BillingContext
    {
        public IBillingRule Rule { get; }
        public List<IRebate> Rebates { get; } = new();
        public BillingContext(IBillingRule rule) => Rule = rule;
        public double Finalize(int units, int outageDays)
        {
            double total = Rule.Compute(units);
            foreach (var r in Rebates) total += r.Apply(total, outageDays);
            return total;
        }
    }

}
