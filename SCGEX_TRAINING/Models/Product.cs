using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SCGEX_TRAINING.Models
{
    public class Product
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        [Required]
        public virtual Machine Machine { get; set; }
        [Required]
        public virtual ProductInfo ProductInfo { get; set; }
    }
}
