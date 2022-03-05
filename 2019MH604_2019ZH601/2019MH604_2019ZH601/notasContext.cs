using _2019MH604_2019ZH601.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2019MH604_2019ZH601
{
    public class notasContext : DbContext
    {
        public notasContext(DbContextOptions<notasContext> options) : base(options) 
        {
            
        }

        public DbSet<alumnos> alumnos { get; set; }
        public DbSet<departamentos> departamentos { get; set; }
        public DbSet<Facultad> facultad { get; set; }
        public DbSet<inscripciones> inscripciones { get; set; }
        public DbSet<materias> materias { get; set; }
        public DbSet<notas> notas { get; set; }
    }
}
