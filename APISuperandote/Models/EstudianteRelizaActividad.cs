using System;
using System.Collections.Generic;

namespace APISuperandote.Models
{
    public partial class EstudianteRelizaActividad
    {
        public EstudianteRelizaActividad()
        {
            ReportesProgresos = new HashSet<ReportesProgreso>();
        }

        public int Id { get; set; }
        public int IdActividad { get; set; }
        public long Ciestudiante { get; set; }
        public int Nivel { get; set; }
        public int Puntos { get; set; }
        public bool Estado { get; set; }

        public virtual Estudiante CiestudianteNavigation { get; set; } = null!;
        public virtual Actividad IdActividadNavigation { get; set; } = null!;
        public virtual ICollection<ReportesProgreso> ReportesProgresos { get; set; }
    }
}
