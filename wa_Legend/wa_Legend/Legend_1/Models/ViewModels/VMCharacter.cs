using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legend_1.Models.ViewModels
{
    public class VMCharacter
    {
        public Character Character { get; set; }
        public IEnumerable<SelectListItem> OrdersSelectList { get; set; }
    }
}
