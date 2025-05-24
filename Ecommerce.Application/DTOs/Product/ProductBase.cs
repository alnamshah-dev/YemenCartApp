using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs.Product
{
    public class ProductBase
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Product name must be between 1 and 100 characters.")]
        public string? Name {  get; set; }
        [Required(ErrorMessage = "Product image URL is required.")]
        public string? Image {  get; set; }
        [Required(ErrorMessage = "Product quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Product quantity must be greater than or equal to zero.")]
        public int Quantity {  get; set; }
        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Product description must be between 1 and 500 characters.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than zero.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Category ID is required.")]
        public Guid CategoryId { get; set; }

    }
}
