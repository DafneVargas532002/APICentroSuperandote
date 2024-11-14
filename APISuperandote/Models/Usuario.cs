using System;
using System.Collections.Generic;

namespace APISuperandote.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Modificaciones = new HashSet<Modificacione>();
        }

        public int Id { get; set; }
        public int Ci { get; set; }
        public string Contraseña { get; set; } = null!;
        public int IdRol { get; set; }
        public bool Estado { get; set; }

        public virtual Role IdRolNavigation { get; set; } = null!;
        public virtual ICollection<Modificacione> Modificaciones { get; set; }
    }
}
