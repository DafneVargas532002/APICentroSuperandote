using System;
using System.Collections.Generic;

namespace APISuperandote.Models
{
    public partial class Educadore
    {
        public Educadore()
        {
            ReportesProgresos = new HashSet<ReportesProgreso>();
        }

        public int Id { get; set; }
        public int? Ci { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<ReportesProgreso> ReportesProgresos { get; set; }
    }
}
