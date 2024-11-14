using System;
using System.Collections.Generic;

namespace APISuperandote.Models
{
    public partial class Actividad
    {
        public Actividad()
        {
            EstudianteRealizaActividads = new HashSet<EstudianteRealizaActividad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Niveles { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<EstudianteRealizaActividad> EstudianteRealizaActividads { get; set; }
    }
}
