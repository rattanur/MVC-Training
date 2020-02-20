using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCGEX_TRAINING.Models
{
    public class ProductInfo
    {
        [Key]
        [StringLength(12)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, 999999)]
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public virtual Category Category { get; set; }

        public virtual Company Supplier { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
