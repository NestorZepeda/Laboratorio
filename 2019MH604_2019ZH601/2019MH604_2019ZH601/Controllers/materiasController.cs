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
    public class materiasController : ControllerBase
    {

        //Configurar variable de conexion al contexto
        private readonly notasContext _contexto;
        public materiasController(notasContext miContexto)
        {
            _contexto = miContexto;
        }

        [HttpGet]
        [Route("api/Consultarmaterias")]
        public IActionResult Get()
        {
            
            var materialist = (from e in _contexto.materias
                                       //fid hace referencia a facultad id
                                   join Fid in _contexto.facultad on e.facultadId equals Fid.id
                                   select new
                                   {
                                       e.id,
                                       Facultad_ID = Fid.id,
                                       Fid.facultad,
                                       e.materia,
                                       e.unidades_valorativas,
                                       e.estado

                                   });

            if (materialist.Count() > 0)
            {
                return Ok(materialist);
            }
            return NotFound();

        }

        [HttpGet]
        [Route("api/ConsultarmateriasPorID/{idmateria }")]
        public IActionResult Get(int idmateria)
        {
            var Materia = (from e in _contexto.materias
                               //fid hace referencia a facultad id
                           join Fid in _contexto.facultad on e.facultadId equals Fid.id
                           where e.id ==idmateria
                           select new
                           {
                               e.id,
                               Facultad_ID = Fid.id,
                               Fid.facultad,
                               e.materia,
                               e.unidades_valorativas,
                               e.estado

                           }).FirstOrDefault();
            if (Materia != null)
            {
                return Ok(Materia);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("api/Insertarmaterias")]
        public IActionResult guardarMateria([FromBody] materias MateriaNuevo)
        {
            try
            {
                _contexto.materias.Add(MateriaNuevo);
                _contexto.SaveChanges();
                return Ok(MateriaNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Route("api/Actualizarmaterias")]
        public IActionResult updatematerias([FromBody] materias MateriaAModificar)
        {

            //Para actualizar un registro, se obtiene el registro origial de la base de datos
           materias materiaExiste = (from e in _contexto.materias
                                               where e.id == MateriaAModificar.id
                                               select e).FirstOrDefault();
            if (materiaExiste is null)
            {
                // si no existe el registro de retorna un NO ENCONTRAD

                return NotFound();
            }

            // si se encuntra el registro, se alteran los campos a modi ficar.
            materiaExiste.facultadId = MateriaAModificar.facultadId;
            materiaExiste.materia = MateriaAModificar.materia;
            materiaExiste.unidades_valorativas = MateriaAModificar.unidades_valorativas;
            materiaExiste.estado = MateriaAModificar.estado;


            //Se envia el objeto a la base de datos.
            _contexto.Entry(materiaExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(materiaExiste);
        }
    }
}
