using System;
using System.Collections.Generic;

namespace APISuperandote.Models
{
    public partial class Modificacione
    {
        public int Id { get; set; }
        public int CiUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Modificacion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual Usuario CiUsuarioNavigation { get; set; } = null!;
    }
}
