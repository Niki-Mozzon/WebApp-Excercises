using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legend_2.Models.ViewModels
{
    public class CharacterVM
    {
        public Character Character { get; set; }
        public IEnumerable<SelectListItem> Orders{ get; set; }
    }
}
