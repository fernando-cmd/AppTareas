using System.Text.Json.Serialization;

namespace ToDoApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }           // Identificador de la tarea
        public string NombreCompleto { get; set; }    // Nombre o descripción de la tarea
        public DateTime FechaCreacion { get; set; }
        public string Sector { get; set; }
        public string Categoria { get; set; }

        [JsonIgnore]
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();  
    }
}
