using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMascota.Models;

namespace MVCMascota.Controllers
{
    public class DuenoController : Controller
    {
        private readonly MVCMascotasContext _context;
        public IEnumerable<Mascota> listaMascotas { get; set; }

        public DuenoController(MVCMascotasContext context)
        {
            _context = context;
        }

        // GET: Dueno
        public async Task<IActionResult> Index()
        {
              return _context.Duenos != null ? 
                          View(await _context.Duenos.ToListAsync()) :
                          Problem("Dueño 'MVCMascotasContext.Duenos'  esta vacio.");
        }

        // GET: Dueno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Duenos == null)
            {
                return NotFound();
            }

            var dueno = await _context.Duenos
                .FirstOrDefaultAsync(m => m.DuenoId == id);
            if (dueno == null)
            {
                return NotFound();
            }

            return View(dueno);
        }

        // GET: Dueno/DetallesListado/5
        public async Task<IActionResult> DetallesListado(int? id)
        {
            var dueno = await _context.Duenos.FirstOrDefaultAsync(m => m.DuenoId == id);
            listaMascotas = _context.Mascotas.Where(m => m.DuenoId == id).ToList();
            return View(dueno);
        }

        // GET: Dueno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dueno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DuenoId,Nombres,Apellidos,Direccion,Telefono,Correo")] Dueno dueno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dueno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dueno);
        }

        // GET: Dueno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Duenos == null)
            {
                return NotFound();
            }

            var dueno = await _context.Duenos.FindAsync(id);
            if (dueno == null)
            {
                return NotFound();
            }
            return View(dueno);
        }

        // POST: Dueno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DuenoId,Nombres,Apellidos,Direccion,Telefono,Correo")] Dueno dueno)
        {
            if (id != dueno.DuenoId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                try
                {
                    _context.Update(dueno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuenoExists(dueno.DuenoId))
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

            return View(dueno);
        }

        // GET: Dueno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Duenos == null)
            {
                return NotFound();
            }

            var dueno = await _context.Duenos
                .FirstOrDefaultAsync(m => m.DuenoId == id);
            if (dueno == null)
            {
                return NotFound();
            }

            return View(dueno);
        }

        // POST: Dueno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Duenos == null)
            {
                return Problem("Dueño 'MVCMascotasContext.Duenos' esta vacio.");
            }
            var dueno = await _context.Duenos.FindAsync(id);
            if (dueno != null)
            {
                _context.Duenos.Remove(dueno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuenoExists(int id)
        {
          return (_context.Duenos?.Any(e => e.DuenoId == id)).GetValueOrDefault();
        }
    }
}
