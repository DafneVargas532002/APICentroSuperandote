using System;
using System.Collections.Generic;

namespace APISuperandote.Models
{
    public partial class EstudianteRealizaActividad
    {
        public EstudianteRealizaActividad()
        {
            ReportesProgresos = new HashSet<ReportesProgreso>();
        }

        public int Id { get; set; }
        public int IdActividad { get; set; }
        public int Ciestudiante { get; set; }
        public int Nivel { get; set; }
        public int Puntos { get; set; }
        public bool Estado { get; set; }
        public DateTime? FechaActividad { get; set; }
        public string? Tiempo { get; set; }

        public virtual Estudiante CiestudianteNavigation { get; set; } = null!;
        public virtual Actividad IdActividadNavigation { get; set; } = null!;
        public virtual ICollection<ReportesProgreso> ReportesProgresos { get; set; }
    }
}
