using System.ComponentModel.DataAnnotations.Schema;

namespace AMI_ProjectAPI.Data
{
    public class BillPaymentDTO
    {
       
            public decimal AmountPaid { get; set; }
            public string PaymentMode { get; set; } = string.Empty;
            public DateTime PaymentDate { get; set; }
        
    }
}
