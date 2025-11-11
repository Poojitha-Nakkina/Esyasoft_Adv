//using AMI_ProjectAPI.Data.Repository;
//using AMI_ProjectAPI.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace AMI_ProjectAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BillReadingController : ControllerBase
//    {

//        private readonly AmiContext _context;
//        private readonly IGenericRepository<BillReading> _billReadingRepo;

//        public BillReadingController(AmiContext context, IGenericRepository<BillReading> billReadingRepo)
//        {
//            _context = context;
//            _billReadingRepo = billReadingRepo;
//        }

//        // 🔹 GET all bill readings
//        [HttpGet("AllBillReadings")]
//        public async Task<IActionResult> GetAllBillReadings()
//        {
//            var bills = await _billReadingRepo.GetAllAsync();
//            return Ok(bills);
//        }

//        // 🔹 GET bill by ID
//        [HttpGet("BillReading/{id}")]
//        public async Task<IActionResult> GetBillById(int id)
//        {
//            var bill = await _billReadingRepo.GetByIdAsync(id);
//            if (bill == null)
//                return NotFound();

//            return Ok(bill);
//        }

//        // 🔹 GET bills for a specific consumer & month (frontend use)
//        [HttpGet("ConsumerBills/{consumerId}/{month}")]
//        public async Task<IActionResult> GetBillsByConsumerAndMonth(int consumerId, string month)
//        {
//            var bills = await _context.BillReadings
//                .Where(b => b.ConsumerId == consumerId && b.BillingMonth == month)
//                .ToListAsync();

//            if (!bills.Any())
//                return NotFound("No bills found for this consumer/month");

//            return Ok(bills);
//        }

//        // 🔹 POST — Generate bills for a given month (core billing logic)
//        [HttpPost("GenerateBills/{month}")]
//        public async Task<IActionResult> GenerateBills(string month)
//        {
//            // ✅ Step 0️⃣ : Extract Year and Month from the "YYYY-MM" format
//            if (!DateTime.TryParse($"{month}-01", out DateTime parsedMonth))
//                return BadRequest("Invalid month format. Please use 'YYYY-MM' (e.g., 2025-11).");

//            int selectedYear = parsedMonth.Year;
//            int selectedMonth = parsedMonth.Month;

//            // ✅ Step 1️⃣ : Compute monthly consumption per meter
//            var consumptions = await (
//                from mr in _context.MeterReadings
//                join m in _context.Meters on mr.MeterId equals m.MeterId
//                join c in _context.Consumers on m.ConsumerId equals c.ConsumerId
//                join t in _context.Tariffs on c.TariffId equals t.TariffId
//                join s in _context.TariffSlabs on t.TariffId equals s.TariffId
//                where mr.ReadingDate.Month == selectedMonth && mr.ReadingDate.Year == selectedYear
//                group mr by new
//                {
//                    c.ConsumerId,
//                    mr.MeterId,
//                    t.TariffId,
//                    t.Name,
//                    t.BaseRate,
//                    t.TaxRate,
//                    s.FromKwh,
//                    s.ToKwh,
//                    s.RatePerKwh
//                }
//                into g
//                select new
//                {
//                    g.Key.ConsumerId,
//                    g.Key.MeterId,
//                    g.Key.TariffId,
//                    g.Key.Name,
//                    g.Key.BaseRate,
//                    g.Key.TaxRate,
//                    g.Key.FromKwh,
//                    g.Key.ToKwh,
//                    g.Key.RatePerKwh,
//                    BillingMonth = $"{selectedYear}-{selectedMonth:D2}",
//                    TotalConsumption = g.Sum(r => r.ReadingValue)
//                }).ToListAsync();

//            if (!consumptions.Any())
//                return BadRequest("No meter readings found for this month.");

//            // ✅ Step 2️⃣ : Compute energy charges, tax, and total bill
//            var billList = consumptions
//                .GroupBy(x => new { x.ConsumerId, x.MeterId, x.BillingMonth, x.Name, x.BaseRate, x.TaxRate })
//                .Select(g =>
//                {
//                    decimal totalConsumption = g.First().TotalConsumption;
//                    decimal energyCharge = g.Sum(s =>
//                    {
//                        if (totalConsumption >= s.FromKwh && totalConsumption <= s.ToKwh)
//                            return (totalConsumption - s.FromKwh) * s.RatePerKwh;
//                        else if (totalConsumption > s.ToKwh)
//                            return (s.ToKwh - s.FromKwh) * s.RatePerKwh;
//                        else
//                            return 0;
//                    }) + g.Key.BaseRate;

//                    decimal taxAmount = energyCharge * (g.Key.TaxRate);
//                    decimal totalBill = energyCharge + taxAmount;

//                    return new BillReading
//                    {
//                        ConsumerId = g.Key.ConsumerId,
//                        MeterId = g.Key.MeterId,
//                        TariffId = g.First().TariffId,
//                        BillingMonth = g.Key.BillingMonth,
//                        TariffType = g.Key.Name,
//                        TotalConsumption = totalConsumption,
//                        EnergyCharge = Math.Round(energyCharge, 2),
//                        TaxAmount = Math.Round(taxAmount, 2),
//                        TotalBill = Math.Round(totalBill, 2),
//                        Status = "Unpaid",
//                        GeneratedDate = DateTime.Now
//                    };
//                })
//                .ToList();

//            // ✅ Step 3️⃣ : Save generated bills
//            await _context.AddRangeAsync(billList);
//            await _context.SaveChangesAsync();


//            return Ok(new
//            {
//                Message = $"✅ {billList.Count} bills generated for {month}",
//                Bills = billList
//            });
//        }


//        // 🔹 PUT — Update payment status
//        [HttpPut("PayBill/{consumerId}/{month}")]
//        public async Task<IActionResult> PayBill(int consumerId, string month)
//        {
//            var bills = await _context.BillReadings
//                .Where(b => b.ConsumerId == consumerId && b.BillingMonth == month && b.Status == "Unpaid")
//                .ToListAsync();

//            if (!bills.Any())
//                return NotFound("No unpaid bills found for this consumer/month.");

//            foreach (var bill in bills)
//                bill.Status = "Paid";

//            await _context.SaveChangesAsync();

//            return Ok($"✅ Bills for consumer {consumerId} ({month}) marked as Paid.");
//        }

//        // 🔹 DELETE a bill
//        [HttpDelete("DeleteBill/{id}")]
//        public async Task<IActionResult> DeleteBill(int id)
//        {
//            await _billReadingRepo.DeleteAsync(id);
//            return NoContent();
//        }

//    }
//}


using AMI_ProjectAPI.Data;
using AMI_ProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMI_ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillReadingController : ControllerBase
    {
        private readonly AmiContext _context;

        public BillReadingController(AmiContext context)
        {
            _context = context;
        }

        // GET all bills
        [HttpGet("AllBills")]
        public async Task<IActionResult> GetAllBills()
        {
            var bills = await _context.BillReadings
                .OrderByDescending(b => b.BillingMonth)
                .ToListAsync();
            return Ok(bills);
        }

        // GET bills by Customer
        [HttpGet("ByCustomer/{consumerId}")]
        public async Task<IActionResult> GetBillsByCustomer(int consumerId)
        {
            var bills = await _context.BillReadings
                .Where(b => b.ConsumerId == consumerId)
                .OrderByDescending(b => b.BillingMonth)
                .ToListAsync();

            if (!bills.Any())
                return NotFound($"No bills found for consumer {consumerId}");

            return Ok(bills);
        }

        // Generate or return bills for a month (YYYY-MM)
        [HttpGet("Month/{month}")]
        public async Task<IActionResult> GetOrGenerateBillsForMonth(string month)
        {
            // validate month
            if (!DateTime.TryParse($"{month}-01", out DateTime parsedMonth))
                return BadRequest("Invalid month format. Use 'YYYY-MM' (e.g., 2025-11).");

            int selectedYear = parsedMonth.Year;
            int selectedMonth = parsedMonth.Month;
            string billingMonthKey = $"{selectedYear}-{selectedMonth:D2}";

            // Step 1: Compute monthly consumption per meter as Max(reading) - Min(reading)
            var meterConsumptions = await (
                from mr in _context.MeterReadings
                join m in _context.Meters on mr.MeterId equals m.MeterId
                join c in _context.Consumers on m.ConsumerId equals c.ConsumerId
                join t in _context.Tariffs on c.TariffId equals t.TariffId
                where mr.ReadingDate.Month == selectedMonth && mr.ReadingDate.Year == selectedYear
                group mr by new
                {
                    c.ConsumerId,
                    mr.MeterId,
                    t.TariffId,
                    t.Name,
                    t.BaseRate,
                    t.TaxRate
                } into g
                select new
                {
                    g.Key.ConsumerId,
                    g.Key.MeterId,
                    g.Key.TariffId,
                    g.Key.Name,
                    g.Key.BaseRate,
                    g.Key.TaxRate,
                    BillingMonth = billingMonthKey,
                    TotalConsumption = g.Max(r => r.ReadingValue) - g.Min(r => r.ReadingValue)
                }).ToListAsync();

            if (!meterConsumptions.Any())
                return NotFound("No meter readings available for this month.");

            // Step 2: Fetch existing bills for that month into dictionary (consumer,meter) -> bill
            var existingBills = await _context.BillReadings
                .Where(b => b.BillingMonth == billingMonthKey)
                .ToListAsync();

            var existingBillMap = existingBills.ToDictionary(b => (b.ConsumerId, b.MeterId), b => b);

            // Step 3: Pre-fetch slabs for involved tariffs to avoid repeated DB calls
            var tariffIds = meterConsumptions.Select(x => x.TariffId).Distinct().ToList();
            var slabs = await _context.TariffSlabs
                .Where(s => tariffIds.Contains(s.TariffId))
                .OrderBy(s => s.FromKwh)
                .ToListAsync();

            // Create map: tariffId -> ordered slab list
            var slabsByTariff = slabs
                .GroupBy(s => s.TariffId)
                .ToDictionary(g => g.Key, g => g.OrderBy(s => s.FromKwh).ToList());

            List<BillReading> toUpsert = new();

            foreach (var r in meterConsumptions)
            {
                decimal totalConsumption = Math.Max(0, r.TotalConsumption); // avoid negatives
                decimal energyCharge = 0m;

                // Get slabs for tariff; if none exist, we treat energyCharge = baseRate (or you may choose fallback rate)
                if (slabsByTariff.TryGetValue(r.TariffId, out var tariffSlabs) && tariffSlabs.Any())
                {
                    // robust slab calculation (units in each slab = max(0, min(total, To) - From) )
                    foreach (var slab in tariffSlabs)
                    {
                        // ensure slab.ToKwh > slab.FromKwh
                        decimal from = slab.FromKwh;
                        decimal to = slab.ToKwh;

                        if (to <= from) continue; // skip invalid slab

                        var units = Math.Max(0m, Math.Min(totalConsumption, to) - from);
                        if (units <= 0) continue;

                        energyCharge += units * slab.RatePerKwh;
                    }
                }
                // add base rate for tariff (flat fixed component)
                energyCharge += r.BaseRate;

                // TaxRate: handle if stored as percent (e.g., 18) or fraction (0.18)
                decimal taxRate = r.TaxRate;
                if (taxRate > 1) taxRate = taxRate / 100m;

                decimal taxAmount = Math.Round(energyCharge * taxRate, 2);
                decimal totalBill = Math.Round(energyCharge + taxAmount, 2);

                // Upsert logic
                if (existingBillMap.TryGetValue((r.ConsumerId, r.MeterId), out var existing))
                {
                    // update only if values differ (so we avoid unnecessary writes)
                    if (existing.TotalConsumption != totalConsumption
                        || existing.EnergyCharge != Math.Round(energyCharge, 2)
                        || existing.TotalBill != totalBill)
                    {
                        existing.TotalConsumption = totalConsumption;
                        existing.EnergyCharge = Math.Round(energyCharge, 2);
                        existing.TaxAmount = taxAmount;
                        existing.TotalBill = totalBill;
                        existing.GeneratedDate = DateTime.UtcNow;
                        toUpsert.Add(existing);
                    }
                }
                else
                {
                    var newBill = new BillReading
                    {
                        ConsumerId = r.ConsumerId,
                        MeterId = r.MeterId,
                        TariffId = r.TariffId,
                        BillingMonth = r.BillingMonth,
                        TariffType = r.Name,
                        TotalConsumption = totalConsumption,
                        EnergyCharge = Math.Round(energyCharge, 2),
                        TaxAmount = taxAmount,
                        TotalBill = totalBill,
                        Status = "Unpaid",
                        GeneratedDate = DateTime.UtcNow
                    };
                    toUpsert.Add(newBill);
                }
            }

            // Step 4: Save changes (insert new and update existing)
            if (toUpsert.Any())
            {
                foreach (var b in toUpsert)
                {
                    if (b.BillId == 0)
                        _context.BillReadings.Add(b);
                    else
                        _context.BillReadings.Update(b);
                }
                await _context.SaveChangesAsync();
            }

            // Step 5: Return all bills for that month
            var finalBills = await _context.BillReadings
                .Where(b => b.BillingMonth == billingMonthKey)
                .ToListAsync();

            return Ok(new
            {
                Message = toUpsert.Any()
                    ? $"✅ {toUpsert.Count} bills generated/updated for {billingMonthKey}"
                    : $"⚙️ All bills for {billingMonthKey} are already up-to-date",
                Bills = finalBills
            });
        }

        // GET bills for specific customer and month
        [HttpGet("ByCustomer/{consumerId}/{month}")]
        public async Task<IActionResult> GetBillByCustomerAndMonth(int consumerId, string month)
        {
            if (!DateTime.TryParse($"{month}-01", out DateTime parsedMonth))
                return BadRequest("Invalid month format. Use 'YYYY-MM'.");

            var billingMonthKey = $"{parsedMonth.Year}-{parsedMonth.Month:D2}";

            var bill = await _context.BillReadings
                .Where(b => b.ConsumerId == consumerId && b.BillingMonth == billingMonthKey)
                .ToListAsync();

            if (!bill.Any())
                return NotFound($"No bill found for customer {consumerId} in {billingMonthKey}");

            return Ok(bill);
        }


        [HttpPut("PayBill/{billId}")]
        public async Task<IActionResult> PayBill(int billId, [FromBody] BillPaymentDTO payment)
        {
            var bill = await _context.BillReadings.FindAsync(billId);
            if (bill == null)
                return NotFound("Bill not found");

            if (bill.Status == "Paid")
                return BadRequest("Bill already paid");

            if (payment.AmountPaid <= 0)
                return BadRequest("Invalid payment amount");

            // ✅ Handle payment
            bill.AmountPaid = payment.AmountPaid;
            bill.PaymentMode = payment.PaymentMode;
            bill.PaymentDate = payment.PaymentDate;
            if (bill.RemainingBalance == null)
                bill.RemainingBalance = bill.TotalBill;
            bill.RemainingBalance -= payment.AmountPaid;

            bill.Status = bill.RemainingBalance <= 0 ? "Paid" : "Partially Paid";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Payment processed successfully.",
                remainingBalance = bill.RemainingBalance
            });
        }





    }
}