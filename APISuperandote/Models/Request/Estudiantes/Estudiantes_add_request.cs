namespace APISuperandote.Models.Request.Estudiantes
{
    public class Estudiantes_add_request
    {
        public int? Ci { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string? GradoAutismo { get; set; } = null!;
    }
}
