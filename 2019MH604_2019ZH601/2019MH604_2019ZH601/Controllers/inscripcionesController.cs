using _2019MH604_2019ZH601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2019MH604_2019ZH601.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class inscripcionesController : ControllerBase
    {
        //Configurar variable de conexion al contexto
        private readonly notasContext _contexto;
        public inscripcionesController(notasContext miContexto)
        {
            _contexto = miContexto;
        }

        [HttpGet]
        [Route("api/ConsultarInscripciones")]
        public IActionResult Get()
        {
            //IEnumerable<inscripciones> inscripcionlist = (from e in _contexto.inscripciones select e);
            var inscripcionlist = (from e in _contexto.inscripciones
                                       //aid hace referencia a alumno id
                                   join aid in _contexto.alumnos on e.alumnoId equals aid.id
                                   join mid in _contexto.materias on e.materiaId equals mid.id
                                   select new 
                                   {
                                       e.id, 
                                       Alumno_ID= aid.id,
                                       aid.nombre,
                                       Materia_ID= mid.id,
                                       mid.materia,
                                       e.inscripcion,
                                       e.fecha,
                                       e.estado

                                   });
            
            if (inscripcionlist.Count() > 0)
            {
                return Ok(inscripcionlist);
            }
            return NotFound();

        }

        [HttpGet]
        [Route("api/ConsultarInscripcionesPorID/{idInscripcion }")]
        public IActionResult Get(int idInscripcion)
        {
           var inscripcion = (from e in _contexto.inscripciones
                                  //aid hace referencia a alumno id
                              join aid in _contexto.alumnos on e.alumnoId equals aid.id
                              join mid in _contexto.materias on e.materiaId equals mid.id
                              where e.id==idInscripcion
                              select new
                              {
                                  e.id,
                                  Alumno_ID = aid.id,
                                  aid.nombre,
                                  Materia_ID = mid.id,
                                  mid.materia,
                                  e.inscripcion,
                                  e.fecha,
                                  e.estado

                              }).FirstOrDefault();
            if (inscripcion != null)
            {
                return Ok(inscripcion);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("api/InsertarInscripciones")]
        public IActionResult guardarInscripcion([FromBody] inscripciones inscripcionNuevo)
        {
            try
            {
                _contexto.inscripciones.Add(inscripcionNuevo);
                _contexto.SaveChanges();
                return Ok(inscripcionNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Route("api/ActualizarInscripciones")]
        public IActionResult updateinscripciones([FromBody] inscripciones inscripcionAModificar)
        {

            //Para actualizar un registro, se obtiene el registro origial de la base de datos
            inscripciones inscripcionExiste = (from e in _contexto.inscripciones
                                    where e.id == inscripcionAModificar.id
                                    select e).FirstOrDefault();
            if (inscripcionExiste is null)
            {
                // si no existe el registro de retorna un NO ENCONTRAD

                return NotFound();
            }

            // si se encuntra el registro, se alteran los campos a modi ficar.
            inscripcionExiste.alumnoId = inscripcionAModificar.alumnoId;
            inscripcionExiste.materiaId = inscripcionAModificar.materiaId;
            inscripcionExiste.inscripcion = inscripcionAModificar.inscripcion;
            inscripcionExiste.fecha = inscripcionAModificar.fecha;
            inscripcionExiste.estado = inscripcionAModificar.estado;


            //Se envia el objeto a la base de datos.
            _contexto.Entry(inscripcionExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(inscripcionExiste);
        }
    }

   
}
