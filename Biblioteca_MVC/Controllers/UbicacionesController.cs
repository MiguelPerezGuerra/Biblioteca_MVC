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
    public class UbicacionesController : Controller
    {
        private readonly DbbibliotecaV2Context _context;

        public UbicacionesController(DbbibliotecaV2Context context)
        {
            _context = context;
        }

        // GET: Ubicaciones
        public async Task<IActionResult> Index()
        {
              return _context.Ubicaciones != null ? 
                          View(await _context.Ubicaciones.ToListAsync()) :
                          Problem("Entity set 'DbbibliotecaV2Context.Ubicaciones'  is null.");
        }

        // GET: Ubicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ubicaciones == null)
            {
                return NotFound();
            }

            var ubicacione = await _context.Ubicaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ubicacione == null)
            {
                return NotFound();
            }

            return View(ubicacione);
        }

        // GET: Ubicaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ubicaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Seccion,Estante")] Ubicacione ubicacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ubicacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ubicacione);
        }

        // GET: Ubicaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ubicaciones == null)
            {
                return NotFound();
            }

            var ubicacione = await _context.Ubicaciones.FindAsync(id);
            if (ubicacione == null)
            {
                return NotFound();
            }
            return View(ubicacione);
        }

        // POST: Ubicaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Seccion,Estante")] Ubicacione ubicacione)
        {
            if (id != ubicacione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ubicacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UbicacioneExists(ubicacione.Id))
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
            return View(ubicacione);
        }

        // GET: Ubicaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ubicaciones == null)
            {
                return NotFound();
            }

            var ubicacione = await _context.Ubicaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ubicacione == null)
            {
                return NotFound();
            }

            return View(ubicacione);
        }

        // POST: Ubicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ubicaciones == null)
            {
                return Problem("Entity set 'DbbibliotecaV2Context.Ubicaciones'  is null.");
            }
            var ubicacione = await _context.Ubicaciones.FindAsync(id);
            if (ubicacione != null)
            {
                _context.Ubicaciones.Remove(ubicacione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UbicacioneExists(int id)
        {
          return (_context.Ubicaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
