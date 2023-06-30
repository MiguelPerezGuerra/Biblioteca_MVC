using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca_MVC.Models;
using System.Globalization;

namespace Biblioteca_MVC.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly DbbibliotecaV2Context _context;

        public PrestamosController(DbbibliotecaV2Context context)
        {
            _context = context;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index(string buscar)
        {
            //var dbbibliotecaV2Context = _context.Prestamos.Include(p => p.Libro).Include(p => p.Usuario);
            //return View(await dbbibliotecaV2Context.ToListAsync());
            var prestamo = from Prestamos in _context.Prestamos.Include(p => p.Libro).Include(p => p.Usuario) join Usuarios in _context.Usuarios on Prestamos.UsuarioId equals Usuarios.Id select Prestamos;

            if (!string.IsNullOrEmpty(buscar))
            {
                prestamo = prestamo.Where(prestamo => prestamo.Usuario.Nombre.Contains(buscar));
            }

            prestamo = prestamo.Include(p => p.Libro).Include(p => p.Usuario);

            return View(await prestamo.ToListAsync());
        }

        // GET: Prestamos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Prestamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LibroId,UsuarioId,FechaPrestamo,FechaLimiteDevolucion,Estado")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", prestamo.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", prestamo.UsuarioId);
            return View(prestamo);
        }

        // GET: Prestamos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", prestamo.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", prestamo.UsuarioId);
            return View(prestamo);
        }

        // POST: Prestamos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LibroId,UsuarioId,FechaPrestamo,FechaLimiteDevolucion,Estado")] Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Id))
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
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", prestamo.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", prestamo.UsuarioId);
            return View(prestamo);
        }

        // GET: Prestamos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prestamos == null)
            {
                return Problem("Entity set 'DbbibliotecaV2Context.Prestamos'  is null.");
            }
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
          return (_context.Prestamos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
