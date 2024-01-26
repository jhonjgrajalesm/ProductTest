using PT.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.Domain.Entities
{
    public class Product
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        public string Name { get; set; }
        [Required]
        public StateType StatusName { get; set; }
        [Required]
        public decimal Stock { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Name must be less than 500 characters.")]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
