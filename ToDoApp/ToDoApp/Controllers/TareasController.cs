// Controllers/TareasController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;


namespace ToDoApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    //ENDPOINT
    public class TareasController : ControllerBase  // Cambié a ControllerBase, que es adecuado para API
    {
        private readonly ToDoContext _context;

        public TareasController(ToDoContext context)
        {
            _context = context;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas()
        {
            return await _context.Tareas.ToListAsync();
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return tarea;
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            if (tarea == null)
            {
                return BadRequest("La tarea no puede ser nula.");
            }

            var usuario = await _context.Usuario.FindAsync(tarea.UsuarioId);
            if (usuario == null)
            {
                return NotFound($"No se encontró el usuario con Id {tarea.UsuarioId}.");
            }

            tarea.Usuario= usuario;

            if (tarea.UsuarioId <= 0)  // Asegúrate de que el UsuarioId sea válido
            {
                return BadRequest("El campo UsuarioId es obligatorio.");
            }

            tarea.FechaCreacion = DateTime.Now;

            _context.Tareas.Add(tarea);  // Añade la tarea al contexto
            await _context.SaveChangesAsync();  // Guarda la tarea en la base de datos

            return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tarea);
        }


        // PUT: api/Tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, [FromBody] Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return BadRequest();
            }

            // Busca la tarea existente en la base de datos
            var tareaExistente = await _context.Tareas.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            if (tareaExistente == null)
            {
                return NotFound();
            }

            // Asigno la fecha que tenia en principio
            tarea.FechaCreacion = tareaExistente.FechaCreacion;

            // Adjunta la tarea al contexto y marca el estado como modificado
            _context.Entry(tarea).State = EntityState.Modified;

            try
            {
                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tareas.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // DELETE: api/Tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
   
    }
 