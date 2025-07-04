using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using petShop.Data;
using petShop.Models;

namespace petShot.Controllers
{
    public class FichaMedicaController : Controller
    {
        private readonly AppDbContext _context;

        public FichaMedicaController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var fichas = _context.FichasMedicas
                                 .Include(f => f.Usuario)
                                 .ToList();
            return View(fichas);
        }

        // GET: FichaMedica/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FichaMedica/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(FichaMedica ficha)
        {
            if (ModelState.IsValid)
            {
                // Simulación de obtener el ID del usuario autenticado desde TempData
                var nombreUsuario = User.Identity?.Name;
                if (string.IsNullOrEmpty(nombreUsuario))
                {
                    ModelState.AddModelError("", "No se pudo identificar al usuario.");
                    return View(ficha);
                }

                var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreCompleto == nombreUsuario);

                if (usuario != null)
                {
                    ficha.IDUsuario = usuario.IDUsuario;
                    _context.FichasMedicas.Add(ficha);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Usuario no autenticado.");
            }

            return View(ficha);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ficha = await _context.FichasMedicas.FindAsync(id);
            if (ficha == null) return NotFound();

            ViewData["IDUsuario"] = new SelectList(_context.Usuarios, "IDUsuario", "NombreCompleto", ficha.IDUsuario);
            return View(ficha);
        }

        // POST: FichaMedica/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, FichaMedica ficha)
        {
            if (id != ficha.IDFicha) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ficha);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.FichasMedicas.Any(e => e.IDFicha == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["IDUsuario"] = new SelectList(_context.Usuarios, "IDUsuario", "NombreCompleto", ficha.IDUsuario);
            return View(ficha);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ficha = await _context.FichasMedicas
                .Include(f => f.Usuario)
                .FirstOrDefaultAsync(m => m.IDFicha == id);

            if (ficha == null) return NotFound();

            return View(ficha);
        }

        // POST: FichaMedica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ficha = await _context.FichasMedicas.FindAsync(id);
            if (ficha != null)
            {
                _context.FichasMedicas.Remove(ficha);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}