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
    public class DevolucionesController : Controller
    {
        private readonly DbbibliotecaV2Context _context;

        public DevolucionesController(DbbibliotecaV2Context context)
        {
            _context = context;
        }

        // GET: Devoluciones
        public async Task<IActionResult> Index(string buscar)
        {
            //var dbbibliotecaV2Context = _context.Devoluciones.Include(d => d.Libro).Include(d => d.Usuario);
            //return View(await dbbibliotecaV2Context.ToListAsync());

            var devolucion = from Devolucion in _context.Devoluciones.Include(d=> d.Libro).Include(e=>e.Usuario) join Libro in _context.Libros on Devolucion.LibroId equals Libro.Id select Devolucion;

            if (!string.IsNullOrEmpty(buscar))
            {
                devolucion = devolucion.Where(devolucion => devolucion.Libro.Titulo.Contains(buscar));
            }

            devolucion = devolucion.Include(d => d.Libro).Include(d => d.Usuario);
            return View(await devolucion.ToListAsync());
        }

        // GET: Devoluciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Devoluciones == null)
            {
                return NotFound();
            }

            var devolucione = await _context.Devoluciones
                .Include(d => d.Libro)
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (devolucione == null)
            {
                return NotFound();
            }

            return View(devolucione);
        }

        // GET: Devoluciones/Create
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Devoluciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LibroId,UsuarioId,FechaPrestamo,FechaDevolucion,Estado")] Devolucione devolucione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(devolucione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", devolucione.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", devolucione.UsuarioId);
            return View(devolucione);
        }

        // GET: Devoluciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Devoluciones == null)
            {
                return NotFound();
            }

            var devolucione = await _context.Devoluciones.FindAsync(id);
            if (devolucione == null)
            {
                return NotFound();
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", devolucione.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", devolucione.UsuarioId);
            return View(devolucione);
        }

        // POST: Devoluciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LibroId,UsuarioId,FechaPrestamo,FechaDevolucion,Estado")] Devolucione devolucione)
        {
            if (id != devolucione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(devolucione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DevolucioneExists(devolucione.Id))
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
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", devolucione.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", devolucione.UsuarioId);
            return View(devolucione);
        }

        // GET: Devoluciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Devoluciones == null)
            {
                return NotFound();
            }

            var devolucione = await _context.Devoluciones
                .Include(d => d.Libro)
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (devolucione == null)
            {
                return NotFound();
            }

            return View(devolucione);
        }

        // POST: Devoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Devoluciones == null)
            {
                return Problem("Entity set 'DbbibliotecaV2Context.Devoluciones'  is null.");
            }
            var devolucione = await _context.Devoluciones.FindAsync(id);
            if (devolucione != null)
            {
                _context.Devoluciones.Remove(devolucione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DevolucioneExists(int id)
        {
          return (_context.Devoluciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
