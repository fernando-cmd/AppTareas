namespace ToDoApp.Models
{
    public class Tarea
    {
        public int Id { get; set; }           // Identificador de la tarea
        public string Nombre { get; set; }    // Nombre o descripción de la tarea
        public bool Completada { get; set; }  // Estado de la tarea

        public DateTime FechaCreacion { get; set; }
    }
}
