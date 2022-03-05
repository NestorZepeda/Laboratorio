using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2019MH604_2019ZH601.Models
{
    public class Facultad
    {
        [Key]
        public int id { get; set; }
        public string facultad { get; set; }
    }
}
