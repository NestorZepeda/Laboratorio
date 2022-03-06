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
    public class notasController : ControllerBase
    {
        //Configurar variable de conexion al contexto
        private readonly notasContext _contexto;
        public notasController(notasContext miContexto)
        {
            _contexto = miContexto;
        }

        [HttpGet]
        [Route("api/ConsultarNotas")]
        public IActionResult Get()
        {

            var notaslist = (from e in _contexto.notas
                                   //Inid hace referencia a inscripcion id
                               join Inid in _contexto.inscripciones on e.inscripcionId equals Inid.id
                               select new
                               {
                                   e.id,
                                   ID_inscripcion = Inid.id,
                                   Inid.inscripcion,
                                   e.evaluacion,
                                   e.nota,
                                   e.porcentaje,
                                   e.fecha

                               });

            if (notaslist.Count() > 0)
            {
                return Ok(notaslist);
            }
            return NotFound();

        }

        [HttpGet]
        [Route("api/ConsultarNotasPorID/{idnota }")]
        public IActionResult Get(int idnota)
        {
            var Nota = (from e in _contexto.notas
                            //Inid hace referencia a inscripcion id
                        join Inid in _contexto.inscripciones on e.inscripcionId equals Inid.id
                        where e.id == idnota
                           select new
                           {

                               e.id,
                               ID_inscripcion = Inid.id,
                               Inid.inscripcion,
                               e.evaluacion,
                               e.nota,
                               e.porcentaje,
                               e.fecha
                           }).FirstOrDefault();
            if (Nota != null)
            {
                return Ok(Nota);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("api/InsertarNotas")]
        public IActionResult guardarnota([FromBody] notas notasNuevo)
        {
            try
            {
                _contexto.notas.Add(notasNuevo);
                _contexto.SaveChanges();
                return Ok(notasNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Route("api/ActualizarNotas")]
        public IActionResult updatenotas([FromBody] notas NotaAModificar)
        {

            //Para actualizar un registro, se obtiene el registro origial de la base de datos
            notas NotaExiste = (from e in _contexto.notas
                                      where e.id == NotaAModificar.id
                                      select e).FirstOrDefault();
            if (NotaExiste is null)
            {
                // si no existe el registro de retorna un NO ENCONTRAD

                return NotFound();
            }

            // si se encuntra el registro, se alteran los campos a modi ficar.
            NotaExiste.inscripcionId = NotaAModificar.inscripcionId;
            NotaExiste.evaluacion = NotaAModificar.evaluacion;
            NotaExiste.nota = NotaAModificar.nota;
            NotaExiste.porcentaje = NotaAModificar.porcentaje;
            NotaExiste.fecha = NotaAModificar.fecha;


            //Se envia el objeto a la base de datos.
            _contexto.Entry(NotaExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(NotaExiste);
        }
    }
}
