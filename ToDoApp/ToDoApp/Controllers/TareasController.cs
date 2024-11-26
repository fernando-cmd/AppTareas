// Controllers/TareasController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;


namespace ToDoApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
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

        // POST: api/Tareas
        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            tarea.FechaCreacion = DateTime.Now;
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tarea);
        }

        // PUT: api/Tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, Tarea tarea)
        {            
            if (id != tarea.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarea).State = EntityState.Modified;

            try
            {
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


    /*[Route("api/[controller]")]
    [ApiController]
    public class TareasController : Controller
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

        // POST: api/Tareas
        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tarea);
        }

        // PUT: api/Tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarea).State = EntityState.Modified;

            try
            {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _context.Tareas.Add(tarea);  // Agregar la tarea a la base de datos
                _context.SaveChanges();      // Guardar los cambios
                return RedirectToAction(nameof(Index));  // Redirigir a la vista de la lista de tareas
            }
            return View(tarea);  // Si el modelo no es válido, vuelve a mostrar el formulario
        }

        public IActionResult Index()
        {
            var tareas = _context.Tareas.ToList();  // Obtener todas las tareas desde la base de datos
            return View(tareas);  // Pasar las tareas a la vista
        }

        // Mostrar formulario de edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = _context.Tareas.Find(id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // Actualizar tarea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);  // Actualizar la tarea en la base de datos
                    _context.SaveChanges();  // Guardar los cambios
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tareas.Any(e => e.Id == tarea.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));  // Redirigir al listado de tareas
            }
            return View(tarea);  // Si el modelo no es válido, vuelve a mostrar el formulario
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = _context.Tareas
                .FirstOrDefault(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tarea = _context.Tareas.Find(id);
            _context.Tareas.Remove(tarea);  // Eliminar la tarea de la base de datos
            _context.SaveChanges();        // Guardar los cambios
            return RedirectToAction(nameof(Index));  // Redirigir a la vista de la lista de tareas
        }

    }*/
}
