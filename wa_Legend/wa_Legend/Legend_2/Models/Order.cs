using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Legend_2.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Order")]
        [MinLength(2)]
        public string NameOrder { get; set; }
    }
}
