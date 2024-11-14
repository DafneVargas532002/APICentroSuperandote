namespace APISuperandote.Models.Request.Usuario
{
    public class usuario_login_request
    {
        public int Ci { get; set; }
        public string Contraseña { get; set; } = null!;
    }
}
