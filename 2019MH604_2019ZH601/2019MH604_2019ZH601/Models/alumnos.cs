using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2019MH604_2019ZH601.Models
{
    public class alumnos
    {
        [Key]
       public int id { get; set; }
        public string carnet { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public int dui { get; set; } 
        public int departamentoId { get; set; }
        public int estado { get; set; }
    }
}
