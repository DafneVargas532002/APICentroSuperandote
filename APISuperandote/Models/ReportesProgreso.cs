using System;
using System.Collections.Generic;

namespace APISuperandote.Models
{
    public partial class ReportesProgreso
    {
        public int Id { get; set; }
        public int Cieducador { get; set; }
        public int Ciestudiante { get; set; }
        public int IdActividad { get; set; }
        public DateTime FechaReporte { get; set; }
        public string? Observaciones { get; set; }
        public bool Estado { get; set; }

        public virtual Educadore CieducadorNavigation { get; set; } = null!;
        public virtual Estudiante CiestudianteNavigation { get; set; } = null!;
        public virtual EstudianteRealizaActividad IdActividadNavigation { get; set; } = null!;
    }
}
