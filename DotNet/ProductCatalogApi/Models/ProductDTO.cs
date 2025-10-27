namespace ProductCatalogApi.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using ProductCatalogApi.Models.Validators;
    using System.ComponentModel.DataAnnotations;


    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        [PriceCheck]
        public decimal Price { get; set; }
        [StockQuantityCheck]
        public int StockQuantity { get; set; }
    }
}
