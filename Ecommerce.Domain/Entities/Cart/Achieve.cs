using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.Cart
{
    public class Achieve
    {
        [Key]
        public Guid Id { get; set; }= Guid.NewGuid();
        public Guid UserId { get; set; }
        public DateTime DateCreate {  get; set; }= DateTime.Now;
        public Guid ProductId { get; set; }
        public int Quantity {  get; set; }
    }
}
