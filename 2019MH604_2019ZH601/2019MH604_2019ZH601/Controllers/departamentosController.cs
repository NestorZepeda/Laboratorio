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
    public class departamentoController : ControllerBase
    {

        //Configurar variable de conexion al contexto
        private readonly notasContext _contexto;
        public departamentoController(notasContext miContexto)
        {
            _contexto = miContexto;
        }

        [HttpGet]
        [Route("api/ConsultarDepartamento")]
        public IActionResult Get()
        {

            var departamentolist = (from e in _contexto.departamentos
                                select new
                                {
                                    e.id,
                                    e.departamento

                                });

            if (departamentolist.Count() > 0)
            {
                return Ok(departamentolist);
            }
            return NotFound();

        }

        [HttpGet]
        [Route("api/ConsultardepartamentoPorID/{iddepartamento }")]
        public IActionResult Get(int iddepartamento)
        {
            var departamentos = (from e in _contexto.departamentos
                            where e.id == iddepartamento
                            select new
                            {
                                e.id,
                                e.departamento

                            }).FirstOrDefault();
            if (departamentos != null)
            {
                return Ok(departamentos);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("api/InsertarDepartamento")]
        public IActionResult guardarDepartamento([FromBody] departamentos deptonuevo)
        {
            try
            {
                _contexto.departamentos.Add(deptonuevo);
                _contexto.SaveChanges();
                return Ok(deptonuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Route("api/Actualizardepto")]
        public IActionResult updatedepto([FromBody] departamentos deptoAModificar)
        {

            //Para actualizar un registro, se obtiene el registro origial de la base de datos
            departamentos deptoExiste = (from e in _contexto.departamentos
                                   where e.id == deptoAModificar.id
                                   select e).FirstOrDefault();
            if (deptoExiste is null)
            {
                // si no existe el registro de retorna un NO ENCONTRAD

                return NotFound();
            }

            // si se encuntra el registro, se alteran los campos a modificar.
            deptoExiste.id = deptoAModificar.id;
            deptoExiste.departamento = deptoAModificar.departamento;


            //Se envia el objeto a la base de datos.
            _contexto.Entry(deptoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(deptoExiste);
        }
    }
}