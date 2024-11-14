namespace APISuperandote.Models.Request.Estudiantes
{
    public class EstudianteRealizaActividad_add_request
    {
        public int IdActividad { get; set; }
        public int Ciestudiante { get; set; } 
        public int Nivel { get; set; } 
        public int Puntos { get; set; } 
        public string Tiempo { get; set; } = null!;
        public DateTime? FechaActividad { get; set; } = null;
    }
}
