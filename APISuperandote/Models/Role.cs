using System;
using System.Collections.Generic;

namespace APISuperandote.Models
{
    public partial class Role
    {
        public Role()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string NombreRol { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
