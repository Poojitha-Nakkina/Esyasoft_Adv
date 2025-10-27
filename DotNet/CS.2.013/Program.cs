namespace CS._2._013
  
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TASK-1
            meter m1 = new meter("AP-0001", "Feeder-12", new DateTime(2024, 3, 25), 7600);
            meter m2 = new meter("AP-0002", "DTR-9", new DateTime(2024, 4, 10), 4500);
            m1.AddReading(100);
            m2.AddReading(-20);
            m1.Summary();
            m2.Summary();

            TASK-2 
            Tariff t1 = new Tariff("Domestic");
            Tariff t2 = new Tariff("Commercial", 8.0);
            Tariff t3 = new Tariff("Industrial", 10.0, 100.0);
            t1.ComputeBill(120);
            t2.ComputeBill(120);
            t3.ComputeBill(120);
            TASK -3
               Device[] device = new Device[2];
               device[0] = new Meter() { Id = "AP-0001", InstalledOn = new DateOnly(2024,7,1), PhaseCount = 3 };
               device[1] = new Gateway() { Id = "GW-11", InstalledOn = new DateOnly(2025,1,10), IpAddress = "10.0.5.21" };
               foreach (var dev in device)
               {
                   dev.Describe();

               }

            Task-4
            List<IReadable> read = new List<IReadable>();
            for (int i = 0; i < 5; i++)
            {
               read.Add(new DlmsMeter());
               read.Add(new ModemGateway());
            }

            foreach (var r in read)
            {
               Console.WriteLine($" {r.SourceId} -> {r.ReadKwh()}");
            }


            TASK-5
               BillingEngine b = new BillingEngine(new DomesticRule());
               BillingEngine b1 = new BillingEngine(new CommercialRule());
               BillingEngine b2 = new BillingEngine(new AgricultureRule());

               Console.WriteLine($"Domestic Bill -> {b.GenerateBill(120)}");
               Console.WriteLine($"Commercial Bill -> {b1.GenerateBill(120)}");
               Console.WriteLine($"Agriculture Bill -> {b2.GenerateBill(120)}");

            TASK-6    
            int[] Hourly = new int[24]  ;
            Random rand = new Random();
            for(int i = 0; i < 24; i++)
            {
               Hourly[i]= rand.Next(1, 50);
            }
            LoadProfileDay d1 = new LoadProfileDay(new DateTime(2025, 10, 1), Hourly);
            Console.WriteLine($"{d1.Date.ToShortDateString()} | Total: {d1.Total} | PeakHour: {d1.PeakHour}");

            TASK-7
                      int[] HourlyTrigger = new int[24] {1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 1
            };


                      LoadProfileDay day = new LoadProfileDay(new DateTime(2025, 10, 1), HourlyTrigger);

                      AlarmRule r1 = new PeakOveruseRule(6);
                      AlarmRule r2 = new SustainedOutageRule(3);

                      if(r1.IsTriggered(day))
                          Console.WriteLine(r1.Message(day));
                      if (r2.IsTriggered(day))
                      {
                          Console.WriteLine(r2.Message(day));
                      }


            TASK-9
            IBillingRule rule = new CommercialRule();
            BillingContext bill = new BillingContext(rule);

            bill.Rebates.Add(new NoOutageRebate());
            bill.Rebates.Add(new HighUsageRebate());
            double subtotal = rule.Compute(620);
            double total =bill.Finalize(620, 0);

            Console.WriteLine($"Subtotal: {subtotal}");

            Console.WriteLine($"Final: {total}");

            TASK-10

            IEnumerable<Event> events = new List<Event>()
            {
               new OutageEvent(new DateTime(2024,7,1,14,30,0), "AP-0001", 120),
               new TamperEvent(new DateTime(2024,7,1,16,0,0), "AP-0002", "MISMATCH"),
               new VoltageEvent(new DateTime(2024,7,1,15,0,0), "AP-0003", 240.5),
               new OutageEvent(new DateTime(2024,7,2,10,0,0), "AP-0004", 60),
               new TamperEvent(new DateTime(2024,7,2,11,30,0), "AP-0005", "MATCH"),
               new VoltageEvent(new DateTime(2024,7,2,12,15,0), "AP-0006", 230.0),
            };

            EventProcessor.PrintTopSevere(events, 3);

        }
    }
}