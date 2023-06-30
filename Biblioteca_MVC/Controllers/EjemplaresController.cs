using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca_MVC.Models;

namespace Biblioteca_MVC.Controllers
{
    public class EjemplaresController : Controller
    {
        private readonly DbbibliotecaV2Context _context;

        public EjemplaresController(DbbibliotecaV2Context context)
        {
            _context = context;
        }

        // GET: Ejemplares
        public async Task<IActionResult> Index(string buscar)
        {
            //var dbbibliotecaV2Context = _context.Ejemplares.Include(e => e.Libro).Include(e => e.Ubicacion);
            //return View(await dbbibliotecaV2Context.ToListAsync());
            var ejemplares = from Ejemplares in _context.Ejemplares.Include(e => e.Libro).Include(e => e.Ubicacion) join Libro in _context.Libros on Ejemplares.LibroId equals Libro.Id select Ejemplares;

            if (!string.IsNullOrEmpty(buscar))
            {
                ejemplares = ejemplares.Where(ejemplares => ejemplares.Libro.Titulo.Contains(buscar));
            }

            ejemplares = ejemplares.Include(e => e.Libro).Include(e => e.Ubicacion);

            return View(await ejemplares.ToListAsync());

        }

        // GET: Ejemplares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ejemplares == null)
            {
                return NotFound();
            }

            var ejemplare = await _context.Ejemplares
                .Include(e => e.Libro)
                .Include(e => e.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ejemplare == null)
            {
                return NotFound();
            }

            return View(ejemplare);
        }

        // GET: Ejemplares/Create
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Estante");
            return View();
        }

        // POST: Ejemplares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LibroId,UbicacionId,CopiasDisponibles,Estado")] Ejemplare ejemplare)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ejemplare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", ejemplare.LibroId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Id", ejemplare.UbicacionId);
            return View(ejemplare);
        }

        // GET: Ejemplares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ejemplares == null)
            {
                return NotFound();
            }

            var ejemplare = await _context.Ejemplares.FindAsync(id);
            if (ejemplare == null)
            {
                return NotFound();
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", ejemplare.LibroId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Estante", ejemplare.UbicacionId);
            return View(ejemplare);
        }

        // POST: Ejemplares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LibroId,UbicacionId,CopiasDisponibles,Estado")] Ejemplare ejemplare)
        {
            if (id != ejemplare.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ejemplare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EjemplareExists(ejemplare.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", ejemplare.LibroId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "Id", "Id", ejemplare.UbicacionId);
            return View(ejemplare);
        }

        // GET: Ejemplares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ejemplares == null)
            {
                return NotFound();
            }

            var ejemplare = await _context.Ejemplares
                .Include(e => e.Libro)
                .Include(e => e.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ejemplare == null)
            {
                return NotFound();
            }

            return View(ejemplare);
        }

        // POST: Ejemplares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ejemplares == null)
            {
                return Problem("Entity set 'DbbibliotecaV2Context.Ejemplares'  is null.");
            }
            var ejemplare = await _context.Ejemplares.FindAsync(id);
            if (ejemplare != null)
            {
                _context.Ejemplares.Remove(ejemplare);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EjemplareExists(int id)
        {
          return (_context.Ejemplares?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
