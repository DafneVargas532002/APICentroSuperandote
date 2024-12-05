using APISuperandote.Models.Response;
using APISuperandote.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APISuperandote.Models.Request.Roles;
using APISuperandote.Models.Request.Estudiantes;

namespace APISuperandote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteRealizaActividadController : ControllerBase
    {
        private readonly db_CMSuperandoteContext _context;

        public EstudianteRealizaActividadController(db_CMSuperandoteContext context)
        {
            _context = context;
        }
        [HttpGet("getAllEstudianteRealizaActividad")]
        public IActionResult getAllEstudianteRealizaActividad()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.EstudianteRealizaActividads.Select(c => new
                {
                    c.Id,
                    c.IdActividadNavigation.Nombre,
                    c.Puntos,
                    c.Nivel,
                    Estado = c.Estado ? "Activo" : "Inactivo",
                    c.Ciestudiante,
                    c.Tiempo,
                    c.FechaActividad
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.success = 1;
                oResponse.data = datos;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getActiveEstudianteRealizaActividad")]
        public IActionResult getActiveEstudianteRealizaActividad()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.EstudianteRealizaActividads.Where(i => i.Estado).Select(c => new
                {
                    c.Id,
                    c.IdActividadNavigation.Nombre,
                    c.Puntos,
                    c.Nivel,
                    c.Estado,
                    c.Ciestudiante,
                    c.Tiempo
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.success = 1;
                oResponse.data = datos;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpGet("getDisabledEstudianteRealizaActividad")]
        public IActionResult getDisabledEstudianteRealizaActividad()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.EstudianteRealizaActividads.Where(i => !i.Estado).Select(c => new
                {
                    c.Id,
                    c.IdActividadNavigation.Nombre,
                    c.Puntos,
                    c.Nivel,
                    c.Estado,
                    c.Ciestudiante,
                    c.Tiempo
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.success = 1;
                oResponse.data = datos;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getEstudianteRealizaActividadById/{id}")]
        public IActionResult getEstudianteRealizaActividadById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.EstudianteRealizaActividads.Where(i => i.Id == id && i.Estado).Select(c => new
                {
                    c.Id,
                    c.IdActividadNavigation.Nombre,
                    c.Ciestudiante,
                    c.Puntos,
                    c.Nivel,
                    c.Tiempo,
                    c.FechaActividad
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.success = 1;
                oResponse.data = datos;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpGet("getEstudianteRealizaActividadByIdEstudiante/{id}")]
        public IActionResult getEstudianteRealizaActividadByIdEstudiante(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.EstudianteRealizaActividads.Where(i => i.Ciestudiante == id).Select(c => new
                {
                    c.Id,
                    c.IdActividadNavigation.Nombre,
                    c.Puntos,
                    c.Nivel,
                    c.Tiempo,
                    FechaActividad = c.FechaActividad.HasValue ? c.FechaActividad.Value.ToString("yyyy-MM-dd") : null,
                    Estado = c.Estado ? "Activo" : "Inactivo",
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.success = 1;
                oResponse.data = datos;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpPost("addEstudianteRealizaActividad")]
        public IActionResult addEstudianteRealizaActividad(EstudianteRealizaActividad_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verf = _context.EstudianteRealizaActividads;

                EstudianteRealizaActividad estudianteRealizaActividad = new EstudianteRealizaActividad();
                estudianteRealizaActividad.IdActividad = oModel.IdActividad;
                estudianteRealizaActividad.Ciestudiante = oModel.Ciestudiante;
                estudianteRealizaActividad.Nivel = oModel.Nivel;
                estudianteRealizaActividad.Puntos = oModel.Puntos;
                estudianteRealizaActividad.Tiempo = oModel.Tiempo;
                estudianteRealizaActividad.FechaActividad = DateTime.Now;
                estudianteRealizaActividad.Estado = true;

                _context.EstudianteRealizaActividads.Add(estudianteRealizaActividad);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = estudianteRealizaActividad;



                oResponse.message = "Registrado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPost("addEstudianteRealizaActividadVR")]
        public IActionResult addEstudianteRealizaActividadVR(EstudianteRealizaActividad_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verf = _context.EstudianteRealizaActividads;

                EstudianteRealizaActividad estudianteRealizaActividad = new EstudianteRealizaActividad();
                estudianteRealizaActividad.IdActividad = oModel.IdActividad;
                estudianteRealizaActividad.Ciestudiante = _context.Estudiantes
     .Where(e => e.Ci == oModel.Ciestudiante)
     .Select(e => e.Id)
     .FirstOrDefault(); 
                estudianteRealizaActividad.Nivel = oModel.Nivel;
                estudianteRealizaActividad.Puntos = oModel.Puntos;
                estudianteRealizaActividad.Tiempo = oModel.Tiempo;
                estudianteRealizaActividad.FechaActividad = DateTime.Now;
                estudianteRealizaActividad.Estado = true;

                _context.EstudianteRealizaActividads.Add(estudianteRealizaActividad);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = estudianteRealizaActividad;



                oResponse.message = "Registrado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateEstudianteRealizaActividad/{id}")]
        public IActionResult updateEstudianteRealizaActividad(EstudianteRealizaActividad_edit_request oModel, int id)
        {
            Response oResponse = new Response();
            try
            {
                // Buscar el registro existente por su ID
                var estudianterealizaactividad = _context.EstudianteRealizaActividads.Find(id);

                // Verificar si el registro no existe
                if (estudianterealizaactividad == null)
                {
                    oResponse.message = "El registro no existe";
                    return NotFound(oResponse);
                }

                // Actualizar las propiedades del objeto existente en lugar de crear uno nuevo
                estudianterealizaactividad.Nivel = oModel.Nivel;
                estudianterealizaactividad.Puntos = oModel.Puntos;
                estudianterealizaactividad.Tiempo = oModel.Tiempo;
                estudianterealizaactividad.FechaActividad = oModel.FechaActividad; // Actualizar la fecha

                // Guardar los cambios en la base de datos
                _context.SaveChanges();

                // Preparar la respuesta
                oResponse.success = 1;
                oResponse.data = estudianterealizaactividad;
                oResponse.message = "Actualizado con éxito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpPut("deleteEstudianteRealizaActividad/{id}")]
        public IActionResult deleteEstudianteRealizaActividad(int id)
        {
            Response oResponse = new Response();
            try
            {
                var estudianterealizaactividad = _context.EstudianteRealizaActividads.Find(id);
                if (estudianterealizaactividad == null)
                {
                    oResponse.message = "El rol no existe";
                    return BadRequest(oResponse);
                }
                if (!estudianterealizaactividad.Estado)
                {
                    oResponse.message = "El rol no existe";
                    return BadRequest(oResponse);
                }
                estudianterealizaactividad.Estado = false;
                _context.EstudianteRealizaActividads.Update(estudianterealizaactividad);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = estudianterealizaactividad;
                oResponse.message = "Registro eliminado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreEstudianteRealizaActividad/{id}")]
        public IActionResult restoreEstudianteRealizaActividad(int id)
        {
            Response oResponse = new Response();
            try
            {
                var estudianterealizaactividad = _context.EstudianteRealizaActividads.Find(id);
                if (estudianterealizaactividad== null)
                {
                    oResponse.message = "El registro no existe";
                    return BadRequest(oResponse);
                }
                if (estudianterealizaactividad.Estado)
                {
                    oResponse.message = "El registro no esta eliminado";
                    return BadRequest(oResponse);
                }
                estudianterealizaactividad.Estado = true;
                _context.EstudianteRealizaActividads.Update(estudianterealizaactividad);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = estudianterealizaactividad;
                oResponse.message = "Registro restaurado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
    }
}
