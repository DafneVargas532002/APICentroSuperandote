using APISuperandote.Models.Request.Actividad;
using APISuperandote.Models.Response;
using APISuperandote.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APISuperandote.Models.Request.Educadores;

namespace APISuperandote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesProgresoController : ControllerBase
    {
        private readonly db_CMSuperandoteContext _context;

        public ReportesProgresoController(db_CMSuperandoteContext context)
        {
            _context = context;
        }


        [HttpGet("getAllReportesProgreso")]
        public IActionResult getAllReportesProgreso()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.ReportesProgresos.Select(c => new
                {
                    c.Id,
                    c.Cieducador,
                    c.Ciestudiante,
                    c.IdActividad,
                    c.FechaReporte,
                    c.Observaciones,
                    Estado = c.Estado ? "Activo" : "Inactivo"
                }).ToList();
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
        [HttpGet("getActiveReportesProgreso")]
        public IActionResult getActiveReportesProgreso()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.ReportesProgresos.Where(i => i.Estado).Select(c => new
                {
                    c.Id,
                    c.Cieducador,
                    c.Ciestudiante,
                    c.IdActividad,
                    c.FechaReporte,
                    c.Observaciones,
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

        [HttpGet("getDisabledReportesProgreso")]
        public IActionResult getDisabledReportesProgreso()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.ReportesProgresos.Where(i => !i.Estado).Select(c => new
                {
                    c.Id,
                    c.Cieducador,
                    c.Ciestudiante,
                    c.IdActividad,
                    c.FechaReporte,
                    c.Observaciones,
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
        [HttpGet("getReportesProgresoById/{id}")]
        public IActionResult getReportesProgresoById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.ReportesProgresos.Where(i => i.Id == id).Select(c => new
                {
                    c.Cieducador,
                    c.Ciestudiante,
                    c.IdActividad,
                    c.FechaReporte,
                    c.Observaciones,
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


        [HttpGet("getReportesProgresoByCIEstudiante/{ci}")]
        public IActionResult getReportesProgresoByCIEstudiante(int ci)
        {
            Response oResponse = new Response();
            try
            {
                var datos = from rp in _context.ReportesProgresos
                            join era in _context.EstudianteRealizaActividads on rp.IdActividad equals era.Id
                            join actividad in _context.Actividads on era.IdActividad equals actividad.Id
                            where rp.Ciestudiante == ci
                            select new
                            {
                                rp.Id,
                                rp.Cieducador,
                                rp.Ciestudiante,
                                nombreactividad = actividad.Nombre,
                                FechaReporte = rp.FechaReporte.ToString("yyyy-MM-dd"),
                                rp.Observaciones,
                                Estado = rp.Estado ? "Activo" : "Inactivo",
                            };


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


        [HttpPost("addReportesProgreso")]
        public IActionResult addReportesProgreso(ReportesProgreso_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verf = _context.ReportesProgresos;
              
                ReportesProgreso reportesProgreso = new ReportesProgreso();

                reportesProgreso.Cieducador = oModel.Cieducador;
                reportesProgreso.Ciestudiante = _context.Estudiantes .Where(e => e.Id == oModel.Ciestudiante).Select(e => e.Ci).FirstOrDefault() ?? 0;
                reportesProgreso.IdActividad = oModel.IdActividad;
                reportesProgreso.FechaReporte = DateTime.Now;
                reportesProgreso.Observaciones = oModel.Observaciones;
                reportesProgreso.Estado = true;


                _context.ReportesProgresos.Add(reportesProgreso);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = reportesProgreso;
                oResponse.message = "Reporte registrado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateReportesProgreso/{id}")]
        public IActionResult updateReportesProgreso(ReportesProgreso_add_request oModel, int id)
        {
            Response oResponse = new Response();
            try
            {
                var reportesProgreso = _context.ReportesProgresos.Find(id);

                // Verificar si el registro no existe
                if (reportesProgreso == null)
                {
                    oResponse.message = "El registro no existe";
                    return NotFound(oResponse);
                }
                reportesProgreso.Cieducador = oModel.Cieducador;
                reportesProgreso.Ciestudiante = oModel.Ciestudiante;
                reportesProgreso.IdActividad = oModel.IdActividad;
                reportesProgreso.FechaReporte = oModel.FechaReporte;
                reportesProgreso.Observaciones = oModel.Observaciones;

                _context.SaveChanges();

                oResponse.success = 1;
                oResponse.data = reportesProgreso;
                oResponse.message = "Reporte actualizado con éxito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteReportesProgreso/{id}")]
        public IActionResult deleteReportesProgreso(int id)
        {
            Response oResponse = new Response();
            try
            {
                var reporteProgreso = _context.ReportesProgresos.Find(id);
                if (reporteProgreso == null)
                {
                    oResponse.message = "El reporte no existe";
                    return BadRequest(oResponse);
                }
                if (!reporteProgreso.Estado)
                {
                    oResponse.message = "El reporte no existe";
                    return BadRequest(oResponse);
                }
                reporteProgreso.Estado = false;
                _context.ReportesProgresos.Update(reporteProgreso);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = reporteProgreso;
                oResponse.message = "Reporte eliminado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreReportesProgreso/{id}")]
        public IActionResult restoreReportesProgreso(int id)
        {
            Response oResponse = new Response();
            try
            {
                var reportesProgreso = _context.ReportesProgresos.Find(id);
                if (reportesProgreso == null)
                {
                    oResponse.message = "El reporte no existe";
                    return BadRequest(oResponse);
                }
                if (reportesProgreso.Estado)
                {
                    oResponse.message = "El reporte no esta eliminado";
                    return BadRequest(oResponse);
                }
                reportesProgreso.Estado = true;
                _context.ReportesProgresos.Update(reportesProgreso);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = reportesProgreso;
                oResponse.message = "Reporte restaurado con exito";
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

