using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2019MH604_2019ZH601.Models
{
    public class inscripciones
    {
        [Key]
        public int id { get; set; }
        public int alumnoId { get; set; }
        public int materiaId { get; set; }
        public int inscripcion { get; set; }

        public DateTime fecha { get; set; }
        public string estado { get; set; }
    }
}
