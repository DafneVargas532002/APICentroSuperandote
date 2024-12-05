using APISuperandote.Models;
using APISuperandote.Models.Request.Educadores;
using APISuperandote.Models.Request.Estudiantes;
using APISuperandote.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISuperandote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly db_CMSuperandoteContext _context;

        public EstudiantesController(db_CMSuperandoteContext context)
        {
            _context = context;
        }
        [HttpGet("getAllEstudiantes")]
        public IActionResult getAllEstudiantes()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Estudiantes.Select(c => new
                {
                    c.Id,
                    c.Ci,
                    c.Nombres,
                    c.Apellidos,
                    FechaNacimiento = c.FechaNacimiento.ToString("yyyy-MM-dd"),
                    c.GradoAutismo,
                    Estado = c.Estado ? "Activo" : "Inactivo"
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
        [HttpGet("getActiveEstudiantes")]
        public IActionResult getActiveEstudiantes()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Estudiantes.Where(i => i.Estado).Select(c => new
                {
                    c.Id,
                    c.Ci,
                    c.Nombres,
                    c.Apellidos,
                    c.FechaNacimiento,
                    c.GradoAutismo,
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

        [HttpGet("getDisabledEstudiantes")]
        public IActionResult getDisabledEstudiantes()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Estudiantes.Where(i => !i.Estado).Select(c => new
                {
                    c.Id,
                    c.Ci,
                    c.Nombres,
                    c.Apellidos,
                    c.FechaNacimiento,
                    c.GradoAutismo,
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
        [HttpGet("getEstudianteByCI/{ci}")]
        public IActionResult getEstudianteByCI(int ci)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Estudiantes.Where(i => i.Ci == ci && i.Estado).Select(c => new
                {
                    c.Id,
                    c.Ci,
                    c.Nombres,
                    c.Apellidos,
                    c.FechaNacimiento,
                    c.GradoAutismo,
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
        [HttpPost("addEstudiante")]
        public IActionResult addEstudiante(Estudiantes_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verf = _context.Estudiantes.Where(i => i.Ci == oModel.Ci);
                var verfEducadores = _context.Educadores.Where(i => i.Ci == oModel.Ci);
                if (verf.Count() != 0 || verfEducadores.Count() != 0)
                {
                    oResponse.message = "El CI ya esta registrado";
                    return BadRequest(oResponse);
                }
                Estudiante estudiante = new Estudiante();
                estudiante.Ci = oModel.Ci;
                estudiante.Nombres = oModel.Nombres;
                estudiante.Apellidos = oModel.Apellidos;
                estudiante.FechaNacimiento = oModel.FechaNacimiento;
                estudiante.GradoAutismo= oModel.GradoAutismo;
                estudiante.Estado = true;
                _context.Estudiantes.Add(estudiante);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = estudiante;
                oResponse.message = "Estudiante registrado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateEstudiante/{ci}")]
        public IActionResult updateEstudiante(Estudiantes_add_request oModel, int ci)
        {
            Response oResponse = new Response();
            try
            {
                var estudiante = _context.Estudiantes.FirstOrDefault(e => e.Ci == ci);
                if (estudiante == null)
                {
                    oResponse.message = "El estudiante no existe";
                    return BadRequest(oResponse);
                }
                estudiante.Ci = ci;
                estudiante.Nombres = oModel.Nombres;
                estudiante.Apellidos = oModel.Apellidos;
                estudiante.FechaNacimiento = oModel.FechaNacimiento;
                estudiante.GradoAutismo= oModel.GradoAutismo;
                _context.Estudiantes.Update(estudiante);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = estudiante;
                oResponse.message = "Estudiante actualizado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteEstudiante/{ci}")]
        public IActionResult deleteEstudiante(int ci)
        {
            Response oResponse = new Response();
            try
            {
                var estudiante = _context.Estudiantes.FirstOrDefault(e => e.Ci == ci);
                var estudianteusuario = _context.Usuarios.FirstOrDefault(e => e.Ci == ci);
                if (estudiante == null)
                {
                    oResponse.message = "El estudiante no existe";
                    return BadRequest(oResponse);
                }
                if (!estudiante.Estado)
                {
                    oResponse.message = "El estudiante no existe";
                    return BadRequest(oResponse);
                }
                estudiante.Estado = false;
                if (estudianteusuario != null)
                {
                    estudianteusuario.Estado = false;
                    _context.Usuarios.Update(estudianteusuario);
                }
                _context.Estudiantes.Update(estudiante);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = estudiante;
                oResponse.message = "Estudiante eliminado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreEstudiante/{ci}")]
        public IActionResult restoreEstudiante(int ci)
        {
            Response oResponse = new Response();
            try
            {
                var estudiante = _context.Estudiantes.FirstOrDefault(e => e.Ci == ci);
                var estudianteusuario = _context.Usuarios.FirstOrDefault(e => e.Ci == ci);
                if (estudiante == null)
                {
                    oResponse.message = "El estudiante no existe";
                    return BadRequest(oResponse);
                }
                if (estudiante.Estado)
                {
                    oResponse.message = "El estudiante no esta eliminado";
                    return BadRequest(oResponse);
                }
                estudiante.Estado = true;
                if (estudianteusuario != null)
                {
                    estudianteusuario.Estado = false;
                    _context.Usuarios.Update(estudianteusuario);
                }
                _context.Estudiantes.Update(estudiante);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = estudiante;
                oResponse.message = "Estudiante restaurado con exito";
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
