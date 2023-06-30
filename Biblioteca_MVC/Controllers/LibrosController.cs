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
    public class LibrosController : Controller
    {
        private readonly DbbibliotecaV2Context _context;

        public LibrosController(DbbibliotecaV2Context context)
        {
            _context = context;
        }

        // GET: Libros
        public async Task<IActionResult> Index(string buscar)
        {

            //var dbbibliotecaV2Context = _context.Libros.Include(l => l.AutorNavigation).Include(l => l.CategoriaNavigation).Include(l => l.EditorialNavigation).Include(l => l.GeneroNavigation);
            //return View(await dbbibliotecaV2Context.ToListAsync());

            var libros = from Libro in _context.Libros select Libro; 

            if (!string.IsNullOrEmpty(buscar))
            {
                libros = libros.Where(s => s.Titulo!.Contains(buscar));
            }

            libros = libros.Include(l => l.AutorNavigation).Include(l => l.CategoriaNavigation).Include(l => l.EditorialNavigation).Include(l => l.GeneroNavigation);

            return View(await libros.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.AutorNavigation)
                .Include(l => l.CategoriaNavigation)
                .Include(l => l.EditorialNavigation)
                .Include(l => l.GeneroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["Autor"] = new SelectList(_context.Autores, "Id", "Nombre");
            ViewData["Categoria"] = new SelectList(_context.Categorias, "Id", "Nombre");
            ViewData["Editorial"] = new SelectList(_context.Editoriales, "Id", "Nombre");
            ViewData["Genero"] = new SelectList(_context.Generos, "Id", "Nombre");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,AnioPublicacion,NumeroCopias,Autor,Editorial,Genero,Categoria")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Autor"] = new SelectList(_context.Autores, "Id", "Id", libro.Autor);
            ViewData["Categoria"] = new SelectList(_context.Categorias, "Id", "Id", libro.Categoria);
            ViewData["Editorial"] = new SelectList(_context.Editoriales, "Id", "Id", libro.Editorial);
            ViewData["Genero"] = new SelectList(_context.Generos, "Id", "Id", libro.Genero);
            return View(libro);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["Autor"] = new SelectList(_context.Autores, "Id", "Nombre", libro.Autor);
            ViewData["Categoria"] = new SelectList(_context.Categorias, "Id", "Nombre", libro.Categoria);
            ViewData["Editorial"] = new SelectList(_context.Editoriales, "Id", "Nombre", libro.Editorial);
            ViewData["Genero"] = new SelectList(_context.Generos, "Id", "Nombre", libro.Genero);
            return View(libro);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,AnioPublicacion,NumeroCopias,Autor,Editorial,Genero,Categoria")] Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id))
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
            ViewData["Autor"] = new SelectList(_context.Autores, "Id", "Id", libro.Autor);
            ViewData["Categoria"] = new SelectList(_context.Categorias, "Id", "Id", libro.Categoria);
            ViewData["Editorial"] = new SelectList(_context.Editoriales, "Id", "Id", libro.Editorial);
            ViewData["Genero"] = new SelectList(_context.Generos, "Id", "Id", libro.Genero);
            return View(libro);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.AutorNavigation)
                .Include(l => l.CategoriaNavigation)
                .Include(l => l.EditorialNavigation)
                .Include(l => l.GeneroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Libros == null)
            {
                return Problem("Entity set 'DbbibliotecaV2Context.Libros'  is null.");
            }
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
          return (_context.Libros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
