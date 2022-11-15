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
    public class MascotaController : Controller
    {
        private readonly MVCMascotasContext _context;

        public MascotaController(MVCMascotasContext context)
        {
            _context = context;
        }

        // GET: Mascota
        public async Task<IActionResult> Index()
        {
            var mVCMascotasContext = _context.Mascotas.Include(m => m.Dueno).Include(m => m.Medico);
            return View(await mVCMascotasContext.ToListAsync());
        }

        // GET: Mascota/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .Include(m => m.Dueno)
                .Include(m => m.Medico)
                .FirstOrDefaultAsync(m => m.MascotaId == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // GET: Mascota/Create
        public IActionResult Create()
        {
            ViewData["DuenoId"] = new SelectList(_context.Duenos, "DuenoId", "DuenoId");
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId");
            return View();
        }

        // POST: Mascota/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MascotaId,Nombre,Color,Especie,Raza,MedicoId,DuenoId")] Mascota mascota)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(mascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DuenoId"] = new SelectList(_context.Duenos, "DuenoId", "DuenoId", mascota.DuenoId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", mascota.MedicoId);
            return View(mascota);
        }

        // GET: Mascota/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }
            ViewData["DuenoId"] = new SelectList(_context.Duenos, "DuenoId", "DuenoId", mascota.DuenoId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", mascota.MedicoId);
            return View(mascota);
        }

        // POST: Mascota/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MascotaId,Nombre,Color,Especie,Raza,MedicoId,DuenoId")] Mascota mascota)
        {
            if (id != mascota.MascotaId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(mascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaExists(mascota.MascotaId))
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
            ViewData["DuenoId"] = new SelectList(_context.Duenos, "DuenoId", "DuenoId", mascota.DuenoId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", mascota.MedicoId);
            return View(mascota);
        }

        // GET: Mascota/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .Include(m => m.Dueno)
                .Include(m => m.Medico)
                .FirstOrDefaultAsync(m => m.MascotaId == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // POST: Mascota/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mascotas == null)
            {
                return Problem("Entity set 'MVCMascotasContext.Mascotas'  is null.");
            }
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota != null)
            {
                _context.Mascotas.Remove(mascota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaExists(int id)
        {
          return (_context.Mascotas?.Any(e => e.MascotaId == id)).GetValueOrDefault();
        }
    }
}
