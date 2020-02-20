using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCGEX_TRAINING.ViewModels
{
    public class MachinesAddProductsVM
    {
        public int MachineId { get; set; }

        [Required]
        [StringLength(12)]
        public string ProductInfoCode { get; set; }

        [Required(ErrorMessage ="กรุณากรอกข้อมูลเป็นตัวเลขเท่านั้น")]
        [Range(1, 999, ErrorMessage = "กรุณากรอกข้อมูลเป็นตัวเลขตั้งแต่ 1-999 เท่านั้น")]
        public int Quantity { get; set; }

        public DateTime ExpiredDate { get; set; }

    }
}