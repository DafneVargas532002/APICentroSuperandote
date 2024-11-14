using APISuperandote.Models;
using APISuperandote.Models.Request.Actividad;
using APISuperandote.Models.Request.Roles;
using APISuperandote.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISuperandote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        private readonly db_CMSuperandoteContext _context;

        public ActividadController(db_CMSuperandoteContext context)
        {
            _context = context;
        }
        [HttpGet("getAllActividades")]
        public IActionResult getAllActividades()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Actividads.Select(c => new
                {
                    c.Id,
                    c.Nombre,
                    c.Descripcion,
                    c.Niveles,
                    c.Estado
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron actividades";
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
        [HttpGet("getActiveActividades")]
        public IActionResult getActiveActividades()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Actividads.Where(i => i.Estado).Select(c => new
                {
                    c.Id,
                    c.Nombre,
                    c.Descripcion,
                    c.Niveles,
                    c.Estado
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

        [HttpGet("getDisabledActividades")]
        public IActionResult getDisabledActividades()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Actividads.Where(i => !i.Estado).Select(c => new
                {
                    c.Id,
                    c.Nombre,
                    c.Descripcion,
                    c.Niveles,
                    c.Estado
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
        [HttpGet("getActividadById/{id}")]
        public IActionResult getActividadById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Actividads.Where(i => i.Id == id).Select(c => new
                {
                    c.Id,
                    c.Nombre,
                    c.Descripcion,
                    c.Niveles,
                    c.Estado
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
        [HttpPost("addActividad")]
        public IActionResult addActividad(Actividad_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verf = _context.Actividads.Where(i => i.Nombre.ToUpper() == oModel.Nombre.ToUpper());
                if (verf.Count() != 0)
                {
                    oResponse.message = "La actividad ya existe";
                    return BadRequest(oResponse);
                }
                Actividad actividad = new Actividad();
                actividad.Nombre = oModel.Nombre;
                actividad.Descripcion = oModel.Descripcion;
                actividad.Estado = true;
                _context.Actividads.Add(actividad);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = actividad;
                oResponse.message = "Actividad registrada con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateActividad/{id}")]
        public IActionResult updateActividad(Actividad_add_request oModel, int id)
        {
            Response oResponse = new Response();
            try
            {
                var actividad = _context.Actividads.Find(id);

                // Verificar si el registro no existe
                if (actividad == null)
                {
                    oResponse.message = "La actividad no existe";
                    return NotFound(oResponse);
                }
                actividad.Nombre = oModel.Nombre;
                actividad.Descripcion = oModel.Descripcion;
                actividad.Niveles = oModel.Niveles;

                _context.SaveChanges();

                oResponse.success = 1;
                oResponse.data = actividad;
                oResponse.message = "Actividad actualizada con éxito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteActividad/{id}")]
        public IActionResult deleteActividad(int id)
        {
            Response oResponse = new Response();
            try
            {
                var actividad = _context.Actividads.Find(id);
                if (actividad == null)
                {
                    oResponse.message = "La actividad no existe";
                    return BadRequest(oResponse);
                }
                if (!actividad.Estado)
                {
                    oResponse.message = "La actividad  no existe";
                    return BadRequest(oResponse);
                }
                actividad.Estado = false;
                _context.Actividads.Update(actividad);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data =actividad;
                oResponse.message = "Actividad eliminada con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreActividad/{id}")]
        public IActionResult restoreActividad(int id)
        {
            Response oResponse = new Response();
            try
            {
                var actividad = _context.Actividads.Find(id);
                if (actividad == null)
                {
                    oResponse.message = "La actividad no existe";
                    return BadRequest(oResponse);
                }
                if (actividad.Estado)
                {
                    oResponse.message = "La actividad no esta eliminada";
                    return BadRequest(oResponse);
                }
                actividad.Estado = true;
                _context.Actividads.Update(actividad);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = actividad;
                oResponse.message = "Actividad restaurada con exito";
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
