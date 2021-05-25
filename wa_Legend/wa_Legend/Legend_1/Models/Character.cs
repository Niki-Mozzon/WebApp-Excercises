using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Legend_1.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [DisplayName("Character")]
        public string NameCharacter { get; set; }

        [ForeignKey("OrderId")]
        [DisplayName("Order")]
        public virtual Order OrderCharacter { get; set; }
        [Required]
        public int OrderId { get; set; }

    }
}
