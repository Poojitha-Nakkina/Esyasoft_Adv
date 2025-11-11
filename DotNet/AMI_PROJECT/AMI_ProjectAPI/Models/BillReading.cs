using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Models;

public partial class BillReading
{
    public int BillId { get; set; }
    [ForeignKey("ConsumerId")]
    public int ConsumerId { get; set; }
    [ForeignKey("MeterId")]

    public int MeterId { get; set; }
    [ForeignKey("TariffId")]

    public int TariffId { get; set; }

    public string BillingMonth { get; set; } = null!;

    public decimal TotalConsumption { get; set; }

    public string TariffType { get; set; } = null!;

    public decimal EnergyCharge { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal TotalBill { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? GeneratedDate { get; set; }

    public decimal? AmountPaid { get; set; }  // nullable if unpaid
    public string? PaymentMode { get; set; }  // Cash, UPI, Card, etc.
    public DateTime? PaymentDate { get; set; }
    public decimal? RemainingBalance { get; set; }

    public virtual Consumer Consumer { get; set; } = null!;

    public virtual Meter Meter { get; set; } = null!;

    public virtual Tariff Tariff { get; set; } = null!;
}
