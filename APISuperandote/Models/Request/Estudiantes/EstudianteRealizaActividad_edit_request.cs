namespace APISuperandote.Models.Request.Estudiantes
{
    public class EstudianteRealizaActividad_edit_request
    {
        public int Nivel { get; set; } 
        public int Puntos { get; set; } 
        public string Tiempo { get; set; } = null!;
        public DateTime? FechaActividad { get; set; } = null;
    }
}
