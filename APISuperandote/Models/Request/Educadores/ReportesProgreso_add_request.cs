namespace APISuperandote.Models.Request.Educadores
{
    public class ReportesProgreso_add_request
    {
        public int Cieducador { get; set; }
        public int Ciestudiante { get; set; }
        public int IdActividad { get; set; }
        public DateTime FechaReporte { get; set; }
        public string Observaciones { get; set; } = null!;
    }
}
