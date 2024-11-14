namespace APISuperandote.Models.Request.Actividad
{
    public class Actividad_add_request
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Niveles { get; set; }
    }
}
