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
    [ApiController]
    public class AlumnoController : ControllerBase
    {

        //Configurar variable de conexion al contexto
        private readonly notasContext _contexto;
        public AlumnoController(notasContext miContexto)
        {
            _contexto = miContexto;
        }

        [HttpGet]
        [Route("api/ConsultarAlumno")]
        public IActionResult Get()
        {

            var inscripcionlist = (from e in _contexto.alumnos
                                       //did hace referencia a departamento id
                                   join did in _contexto.departamentos on e.departamentoId equals did.id
                                   select new
                                   {
                                       e.id,
                                       e.carnet,
                                       e.nombre,
                                       e.apellidos,
                                       e.dui,
                                       departamento_ID = did.id,
                                       e.estado

                                   });

            if (inscripcionlist.Count() > 0)
            {
                return Ok(inscripcionlist);
            }
            return NotFound();

        }

        [HttpGet]
        [Route("api/ConsultarAlumnoPorID/{idalumno }")]
        public IActionResult Get(int idalumno)
        {
            var alumno = (from e in _contexto.alumnos
                          join did in _contexto.departamentos on e.departamentoId equals did.id
                          where e.id == idalumno
                           select new
                           {
                               e.id,
                               e.carnet,
                               e.nombre,
                               e.apellidos,
                               e.dui,
                               departamento_ID = did.id,
                               e.estado

                           }).FirstOrDefault();
            if (alumno != null)
            {
                return Ok(alumno);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("api/Insertaralumno")]
        public IActionResult guardaralumno([FromBody] alumnos alumnoNuevo)
        {
            try
            {
                _contexto.alumnos.Add(alumnoNuevo);
                _contexto.SaveChanges();
                return Ok(alumnoNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Route("api/Actualizaralumno")]
        public IActionResult updatealumno([FromBody] alumnos alumnoAModificar)
        {

            //Para actualizar un registro, se obtiene el registro origial de la base de datos
            alumnos alumnoExiste = (from e in _contexto.alumnos
                                      where e.id == alumnoAModificar.id
                                      select e).FirstOrDefault();
            if (alumnoExiste is null)
            {
                // si no existe el registro de retorna un NO ENCONTRADO

                return NotFound();
            }

            // si se encuntra el registro, se alteran los campos a modificar.
            alumnoExiste.id = alumnoAModificar.id;
            alumnoExiste.carnet = alumnoAModificar.carnet;
            alumnoExiste.nombre = alumnoAModificar.nombre;
            alumnoExiste.apellidos = alumnoAModificar.apellidos;
            alumnoExiste.dui = alumnoAModificar.dui;
            alumnoExiste.departamentoId = alumnoAModificar.departamentoId;
            alumnoExiste.estado = alumnoAModificar.estado;


            //Se envia el objeto a la base de datos.
            _contexto.Entry(alumnoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(alumnoExiste);
        }
    }
}