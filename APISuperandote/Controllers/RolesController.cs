using APISuperandote.Models;
using APISuperandote.Models.Request.Roles;
using APISuperandote.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISuperandote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly db_CMSuperandoteContext _context;

        public RolesController(db_CMSuperandoteContext context)
        {
            _context = context;
        }
        [HttpGet("getAllRoles")]
        public IActionResult getAllRoles()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Roles.Select(c => new
                {
                    c.Id,
                    c.NombreRol,
                    c.Descripcion,
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
        [HttpGet("getActiveRoles")]
        public IActionResult getActiveRoles()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Roles.Where(i => i.Estado).Select(c => new
                {
                    c.Id,
                    c.NombreRol,
                    c.Descripcion,
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

        [HttpGet("getDisabledRoles")]
        public IActionResult getDisabledRoles()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Roles.Where(i => !i.Estado).Select(c => new
                {
                    c.Id,
                    c.NombreRol,
                    c.Descripcion,
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
        [HttpGet("getRolById/{id}")]
        public IActionResult getRolById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Roles.Where(i => i.Id == id && i.Estado).Select(c => new
                {
                    c.Id,
                    c.NombreRol,
                    c.Descripcion,
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
        [HttpPost("addRol")]
        public IActionResult addRol(Rol_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verf = _context.Roles.Where(i => i.NombreRol.ToUpper() == oModel.NombreRol.ToUpper());
                if (verf.Count() != 0)
                {
                    oResponse.message = "El rol ya existe";
                    return BadRequest(oResponse);
                }
                Role cargo = new Role();
                cargo.NombreRol = oModel.NombreRol;
                cargo.Descripcion = oModel.Descripcion;
                cargo.Estado = true;
                _context.Roles.Add(cargo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = cargo;
                oResponse.message = "Rol registrado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateRol/{id}")]
        public IActionResult updateRol(Rol_add_request oModel, int id)
        {
            Response oResponse = new Response();
            try
            {
                var cargo = _context.Roles.Find(id);
                if (cargo == null)
                {
                    oResponse.message = "El Rol no existe";
                    return BadRequest(oResponse);
                }
                cargo.NombreRol = oModel.NombreRol;
                cargo.Descripcion = oModel.Descripcion;
                _context.Roles.Update(cargo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = cargo;
                oResponse.message = "Rol actualizado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteRol/{id}")]
        public IActionResult deleteRol(int id)
        {
            Response oResponse = new Response();
            try
            {
                var cargo = _context.Roles.Find(id);
                if (cargo == null)
                {
                    oResponse.message = "El rol no existe";
                    return BadRequest(oResponse);
                }
                if (!cargo.Estado)
                {
                    oResponse.message = "El rol no existe";
                    return BadRequest(oResponse);
                }
                cargo.Estado = false;
                _context.Roles.Update(cargo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = cargo;
                oResponse.message = "rol eliminado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreRol/{id}")]
        public IActionResult restoreRol(int id)
        {
            Response oResponse = new Response();
            try
            {
                var cargo = _context.Roles.Find(id);
                if (cargo == null)
                {
                    oResponse.message = "El rol no existe";
                    return BadRequest(oResponse);
                }
                if (cargo.Estado)
                {
                    oResponse.message = "El rol no esta eliminado";
                    return BadRequest(oResponse);
                }
                cargo.Estado = true;
                _context.Roles.Update(cargo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = cargo;
                oResponse.message = "Rol restaurado con exito";
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
