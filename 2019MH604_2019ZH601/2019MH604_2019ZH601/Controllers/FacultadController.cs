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
    public class facultadController : ControllerBase
    {

        //Configurar variable de conexion al contexto
        private readonly notasContext _contexto;
        public facultadController(notasContext miContexto)
        {
            _contexto = miContexto;
        }

        [HttpGet]
        [Route("api/Consultarfacultad")]
        public IActionResult Get()
        {

            var facultadlist = (from e in _contexto.facultad
                                select new
                                {
                                    e.id,
                                    e.facultad

                                });

            if (facultadlist.Count() > 0)
            {
                return Ok(facultadlist);
            }
            return NotFound();

        }

        [HttpGet]
        [Route("api/ConsultarfacultadPorID/{idfacultad }")]
        public IActionResult Get(int idfacultad)
        {
            var Facultad = (from e in _contexto.facultad
                            where e.id == idfacultad
                            select new
                            {
                                e.id,
                                e.facultad

                            }).FirstOrDefault();
            if (Facultad != null)
            {
                return Ok(Facultad);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("api/Insertarfacultad")]
        public IActionResult guardarfacultad([FromBody] Facultad facultadNueva)
        {
            try
            {
                _contexto.facultad.Add(facultadNueva);
                _contexto.SaveChanges();
                return Ok(facultadNueva);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Route("api/Actualizarfacultad")]
        public IActionResult updatefacultad([FromBody] Facultad facuAModificar)
        {

            //Para actualizar un registro, se obtiene el registro origial de la base de datos
            Facultad facuExiste = (from e in _contexto.facultad
                                   where e.id == facuAModificar.id
                                   select e).FirstOrDefault();
            if (facuExiste is null)
            {
                // si no existe el registro de retorna un NO ENCONTRAD

                return NotFound();
            }

            // si se encuntra el registro, se alteran los campos a modificar.
            facuExiste.id = facuAModificar.id;
            facuExiste.facultad = facuAModificar.facultad;


            //Se envia el objeto a la base de datos.
            _contexto.Entry(facuExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(facuExiste);
        }
    }
}
