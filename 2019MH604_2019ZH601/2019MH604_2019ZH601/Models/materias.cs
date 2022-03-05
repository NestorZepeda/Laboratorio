using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2019MH604_2019ZH601.Models
{
    public class materias
    {
        [Key]
       public int id { get; set; }
       public int facultadId { get; set; }
       public string materia { get; set; }
       public int unidades_valorativas { get; set; }
       public string estado { get; set; }
    }
}
