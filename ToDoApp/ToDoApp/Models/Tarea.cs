using System.Text.Json.Serialization;

namespace ToDoApp.Models
{
    public class Tarea
    {
        public int Id { get; set; }           // Identificador de la tarea
        public string Nombre { get; set; }    // Nombre o descripción de la tarea
        public bool Completada { get; set; }  // Estado de la tarea

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioId { get; set; }  // Clave foránea (FK) de Usuario
        [JsonIgnore]
        public Usuario? Usuario { get; set; }  // Propiedad de navegación hacia el Usuario
    }
}
