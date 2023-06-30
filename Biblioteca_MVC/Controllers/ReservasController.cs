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
    public class ReservasController : Controller
    {
        private readonly DbbibliotecaV2Context _context;

        public ReservasController(DbbibliotecaV2Context context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index(string buscar)
        {
            //var dbbibliotecaV2Context = _context.Reservas.Include(r => r.Libro).Include(r => r.Usuario);
            //return View(await dbbibliotecaV2Context.ToListAsync());

            var reserva = from Reserva in _context.Reservas select Reserva;

            if (!string.IsNullOrEmpty(buscar))
            {
                //DateTime datetime = DateTime.Parse(buscar);
                DateTime datetime = DateTime.ParseExact(buscar, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string formattedDate = datetime.ToString("yyyy-MM-dd");
                reserva = reserva.Where(r => r.FechaReserva == DateTime.Parse(formattedDate));
            }

            reserva = reserva.Include(r => r.Libro).Include(r => r.Usuario);

            return View(await reserva.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Libro)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LibroId,UsuarioId,FechaReserva,FechaVencimiento,Estado")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", reserva.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", reserva.UsuarioId);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", reserva.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", reserva.UsuarioId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LibroId,UsuarioId,FechaReserva,FechaVencimiento,Estado")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", reserva.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", reserva.UsuarioId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Libro)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservas == null)
            {
                return Problem("Entity set 'DbbibliotecaV2Context.Reservas'  is null.");
            }
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
          return (_context.Reservas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
