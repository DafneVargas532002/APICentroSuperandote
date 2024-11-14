using System;
using System.Collections.Generic;

namespace APISuperandote.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            EstudianteRealizaActividads = new HashSet<EstudianteRealizaActividad>();
            ReportesProgresos = new HashSet<ReportesProgreso>();
        }

        public int Id { get; set; }
        public int? Ci { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; }
        public string? GradoAutismo { get; set; }

        public virtual ICollection<EstudianteRealizaActividad> EstudianteRealizaActividads { get; set; }
        public virtual ICollection<ReportesProgreso> ReportesProgresos { get; set; }
    }
}
