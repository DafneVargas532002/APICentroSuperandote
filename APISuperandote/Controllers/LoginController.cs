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
    public class LoginController : ControllerBase
    {
        private readonly db_CMSuperandoteContext _context;

        public LoginController(db_CMSuperandoteContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult login(usuario_login_request auth)
        {
            Response oResponse = new Response();
            try
            {
                var userSession = _context.Usuarios.Where(i => i.Ci == auth.Ci && i.Contraseña == Encrypt.GetSHA256(auth.Contraseña) && i.Estado).Select(i => new
                {
                    i.Id,
                    i.Ci,
                    i.Estado,
                    i.IdRol
                });
                if (userSession.Count() == 0)
                {
                    oResponse.message = "Error al ingresar, verifique sus datos.";
                    return NotFound(oResponse);
                }
                oResponse.success = 1;
                oResponse.data = userSession;
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

