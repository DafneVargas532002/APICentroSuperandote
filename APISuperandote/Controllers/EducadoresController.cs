using APISuperandote.Models;
using APISuperandote.Models.Request.Educadores;
using APISuperandote.Models.Request.Roles;
using APISuperandote.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISuperandote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducadoresController : ControllerBase
    {
        private readonly db_CMSuperandoteContext _context;

        public EducadoresController(db_CMSuperandoteContext context)
        {
            _context = context;
        }
        [HttpGet("getAllEducadores")]
        public IActionResult getAllEducadores()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Educadores.Select(c => new
                {
                    c.Id,
                    c.Ci,
                    c.Nombres,
                    c.Apellidos,
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
        [HttpGet("getActiveEducadores")]
        public IActionResult getActiveEducadores()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Educadores.Where(i => i.Estado).Select(c => new
                {
                    c.Id,
                    c.Ci,
                    c.Nombres,
                    c.Apellidos,
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

        [HttpGet("getDisabledEducadores")]
        public IActionResult getDisabledEducadores()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Educadores.Where(i => !i.Estado).Select(c => new
                {
                    c.Id,
                    c.Ci,
                    c.Nombres,
                    c.Apellidos,
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
        [HttpGet("getEducadorByCI/{ci}")]
        public IActionResult getEducadorByCI(int ci)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Educadores.Where(i => i.Ci == ci && i.Estado).Select(c => new
                {
                    c.Id,
                    c.Ci,
                    c.Nombres,
                    c.Apellidos,
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
        [HttpPost("addEducador")]
        public IActionResult addEducador(Educadores_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verf = _context.Educadores.Where(i => i.Ci == oModel.Ci);
                var verfEstudiantes = _context.Estudiantes.Where(i => i.Ci == oModel.Ci);
                if (verf.Count() != 0 || verfEstudiantes.Count() != 0 )
                {
                    oResponse.message = "El CI ya se encuentra registrado";
                    return BadRequest(oResponse);
                }
                Educadore educador = new Educadore();
                educador.Ci = oModel.Ci;
                educador.Nombres = oModel.Nombres;
                educador.Apellidos = oModel.Apellidos;
                educador.Estado = true;
                _context.Educadores.Add(educador);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = educador;
                oResponse.message = "Educador registrado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateEducador/{ci}")]
        public IActionResult updateEducador(Educadores_add_request oModel, int ci)
        {
            Response oResponse = new Response();
            try
            {
                var educador = _context.Educadores.FirstOrDefault(e => e.Ci== ci);
                if (educador == null)
                {
                    oResponse.message = "El educador no existe";
                    return BadRequest(oResponse);
                }
                educador.Ci = ci;
                educador.Nombres = oModel.Nombres;
                educador.Apellidos = oModel.Apellidos;
                _context.Educadores.Update(educador);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = educador;
                oResponse.message = "Educador actualizado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteEducador/{ci}")]
        public IActionResult deleteEducador(int ci)
        {
            Response oResponse = new Response();
            try
            {
                var educador = _context.Educadores.FirstOrDefault(e => e.Ci == ci);
                var educadorusuario = _context.Usuarios.FirstOrDefault(e => e.Ci == ci);

                if (educador == null)
                {
                    oResponse.message = "El educador no existe";
                    return BadRequest(oResponse);
                }
                if (!educador.Estado)
                {
                    oResponse.message = "El educador no existe";
                    return BadRequest(oResponse);
                }
                educador.Estado = false;
                if (educadorusuario != null)
                {
                    educadorusuario.Estado = false;
                    _context.Usuarios.Update(educadorusuario);
                }
                
                _context.Educadores.Update(educador);
                
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = educador;
                oResponse.message = "Educador eliminado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreEducador/{ci}")]
        public IActionResult restoreEducador(int ci)
        {
            Response oResponse = new Response();
            try
            {
                var educador = _context.Educadores.FirstOrDefault(e => e.Ci == ci);
                var educadorusuario = _context.Usuarios.FirstOrDefault(e => e.Ci == ci);
                if (educador == null)
                {
                    oResponse.message = "El educador no existe";
                    return BadRequest(oResponse);
                }
                if (educador.Estado)
                {
                    oResponse.message = "El educador no esta eliminado";
                    return BadRequest(oResponse);
                }
                educador.Estado = true;
                if (educadorusuario != null)
                {
                    educadorusuario.Estado = true;
                    _context.Usuarios.Update(educadorusuario);
                }
               
                _context.Educadores.Update(educador);
                
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = educador;
                oResponse.message = "Educador restaurado con exito";
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
