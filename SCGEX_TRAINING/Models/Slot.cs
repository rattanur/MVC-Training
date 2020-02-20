using System.ComponentModel.DataAnnotations;

namespace SCGEX_TRAINING.Models
{
    public class Slot
    {

        public int Id { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [Required]
        public virtual Machine Machine { get; set; }

    }
}