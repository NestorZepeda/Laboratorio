using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2019MH604_2019ZH601.Models
{
    public class notas
    {
        [Key]
        public int id { get; set; }
        public int inscripcionId { get; set; }
        public string evaluacion { get; set; }
        public decimal nota { get; set; }
        public decimal porcentaje { get; set; }
        public DateTime fecha { get; set; }
    }
}
