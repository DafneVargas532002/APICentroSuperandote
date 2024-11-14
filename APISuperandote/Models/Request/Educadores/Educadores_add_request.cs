namespace APISuperandote.Models.Request.Educadores
{
    public class Educadores_add_request
    {
        public int Ci { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
    }
}
