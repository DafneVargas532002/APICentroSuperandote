using apiPlanetFitness.Models.Tools;
using APISuperandote.Models;
using APISuperandote.Models.Request.Usuario;
using APISuperandote.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISuperandote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly db_CMSuperandoteContext _context;

        public UsuariosController(db_CMSuperandoteContext context)
        {
            _context = context;
        }

        [HttpGet("getAllUsers")]
        public IActionResult getAllUsers()
        {
            int ci = 0;
            Response oResponse = new Response();
            try
            {
                var datos = _context.Usuarios.Select(i => new
                {
                    Id = i.Id,
                    CI = i.Ci,
                    nombres = _context.Estudiantes.Where(e => e.Ci == i.Ci).Select(e => e.Nombres)
                    .FirstOrDefault() ??
                    _context.Educadores.Where(ed => ed.Ci == i.Ci).Select(ed => ed.Nombres)
                    .FirstOrDefault(),
                    apellidos = _context.Estudiantes.Where(e => e.Ci == i.Ci).Select(e => e.Apellidos)
                    .FirstOrDefault() ??
                    _context.Educadores.Where(ed => ed.Ci == i.Ci).Select(ed => ed.Apellidos)
                    .FirstOrDefault(),
                    Rol = i.IdRolNavigation.NombreRol,
                    i.Estado
                });


                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getActiveUsers")]
        public IActionResult getActiveUsers()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Usuarios.Where(i => i.Estado).Select(i => new
                {
                    Id = i.Id,
                    CI = i.Ci,
                    Rol = i.IdRolNavigation.NombreRol,
                    i.Estado
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getDisabledUsers")]
        public IActionResult getDisabledUsers()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Usuarios.Where(i => !i.Estado).Select(i => new
                {
                    Id = i.Id,
                    CI = i.Ci,
                    Rol = i.IdRolNavigation.NombreRol,
                    i.Estado
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpGet("getUserByCI/{ci}")]
        public IActionResult getUserByCI(int ci)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Where(i => i.Ci == ci && i.Estado).Select(i => new
                {
                    Id = i.Id,
                    CI = i.Ci,
                    Rol = i.IdRolNavigation.NombreRol,
                    i.Estado
                });
                if (user.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = user;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getUserByRolId/{id}")]
        public IActionResult getUserByRolId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Where(i => i.IdRol == id).Select(i => new
                {
                    Id = i.Id,
                    CI = i.Ci,
                    Rol = i.IdRolNavigation.NombreRol,
                    i.Estado
                });
                if (user.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = user;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getActiveUsersByRolId/{id}")]
        public IActionResult getActiveUserByRolId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Where(i => i.IdRol == id && i.Estado).Select(i => new
                {
                    Id = i.Id,
                    CI = i.Ci,
                    Rol = i.IdRolNavigation.NombreRol,
                    i.Estado
                });
                if (user.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = user;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getDisabledUsersByRolId/{id}")]
        public IActionResult getDisabledUserByRolId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Where(i => i.IdRol == id && !i.Estado).Select(i => new
                {
                    Id = i.Id,
                    CI = i.Ci,
                    Rol = i.IdRolNavigation.NombreRol,
                    i.Estado
                });
                if (user.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = user;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPost("addUser")]
        public IActionResult addUser(usuario_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verf = _context.Usuarios.FirstOrDefault(e => e.Ci == oModel.Ci);
                int Rol = 0; 
                var verficarsiesestudiante = _context.Estudiantes.FirstOrDefault(e => e.Ci == oModel.Ci);
                var verficarsieseducador = _context.Educadores.FirstOrDefault(e => e.Ci == oModel.Ci);


                if (verf != null)
                {
                    oResponse.message = "El usuario ya se encuentra registrado.";
                    return BadRequest(oResponse);
                }
                if(verficarsieseducador==null && verficarsiesestudiante == null)
                {
                    oResponse.message = "El usuario no se encuentra habilitado para registrarse";
                    return BadRequest(oResponse);
                }
                if (verficarsieseducador != null )
                {
                    Rol = 2;
                }
                if ( verficarsiesestudiante != null)
                {
                    Rol = 3;
                }
                Usuario user = new Usuario();
                user.Ci = oModel.Ci;
                user.Contraseña = Encrypt.GetSHA256(oModel.Contraseña);
                user.IdRol = Rol;
                user.Estado = true;
                _context.Usuarios.Add(user);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Usuario registrado con exito";
                oResponse.data = user;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        
        [HttpPut("updateUser/{ci}")]
        public IActionResult updateUser(usuario_update_request oModel, int ci)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios
                   .Where(i => i.Ci == ci && i.Estado)
                   .FirstOrDefault();

                if (user == null)
                {
                    oResponse.message = "El usuario no existe.";
                    return BadRequest(oResponse);
                }


                user.Contraseña = Encrypt.GetSHA256(oModel.Contraseña);
                //user.IdRol = oModel.IdRol;
                _context.Usuarios.Update(user);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Usuario actualizado con exito";
                oResponse.data = user;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteUser/{ci}")]
        public IActionResult deleteUser(int ci)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios
                   .Where(i => i.Ci == ci && i.Estado)
                   .FirstOrDefault();

                if (user == null)
                {
                    oResponse.message = "El usuario no existe.";
                    return BadRequest(oResponse);
                }
                if (!user.Estado)
                {
                    oResponse.message = "El usuario no existe.";
                    return BadRequest(oResponse);
                }
                user.Estado = false;
                _context.Usuarios.Update(user);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Usuario eliminado con exito";
                oResponse.data = user;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreUser/{ci}")]
        public IActionResult restoreUser(int ci)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios
                   .Where(i => i.Ci == ci)
                   .FirstOrDefault();
                if (user == null)
                {
                    oResponse.message = "El usuario no existe.";
                    return BadRequest(oResponse);
                }
                if (user.Estado)
                {
                    oResponse.message = "El usuario no esta eliminado.";
                    return BadRequest(oResponse);
                }
                user.Estado = true;
                _context.Usuarios.Update(user);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Usuario restaurado con exito";
                oResponse.data = user;
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
