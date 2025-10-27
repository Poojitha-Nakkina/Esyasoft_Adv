using System;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogApi.Models.Validators
{
    public class PriceCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var priceCheck = value as decimal?;
            if (priceCheck.HasValue && priceCheck.Value > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Price must be greater than 0.");
        }
    }


    public class StockQuantityCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var stockQuantityCheck = value as int?;
            if (stockQuantityCheck.HasValue && stockQuantityCheck.Value >= 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("stockQuantity must be greater than or equal to 0.");
        }
    }
}
     


    

