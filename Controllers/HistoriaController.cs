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
    public class HistoriaController : Controller
    {
        private readonly MVCMascotasContext _context;

        public HistoriaController(MVCMascotasContext context)
        {
            _context = context;
        }

        // GET: Historia
        public async Task<IActionResult> Index()
        {
            var mVCMascotasContext = _context.Historias.Include(h => h.Mascota);
            return View(await mVCMascotasContext.ToListAsync());
        }

        // GET: Historia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Historias == null)
            {
                return NotFound();
            }

            var historia = await _context.Historias
                .Include(h => h.Mascota)
                .FirstOrDefaultAsync(m => m.HistoriaId == id);
            if (historia == null)
            {
                return NotFound();
            }

            return View(historia);
        }

        // GET: Historia/Create
        public IActionResult Create()
        {
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId");
            return View();
        }

        // POST: Historia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoriaId,FechaCreacion,Diagnostico,Medicamentos,MascotaId")] Historia historia)
        {
            var valido = _context.Historias.FirstOrDefault(h => h.MascotaId == historia.MascotaId);
            if (valido == null)
            {
                _context.Add(historia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "mascota ya tiene una historia asignada");
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", historia.MascotaId);
            return View(historia);
        }

        // GET: Historia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null || _context.Historias == null)
            {
                return NotFound();
            }

            var historia = await _context.Historias.FindAsync(id);
            if (historia == null)
            {
                return NotFound();
            }
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", historia.MascotaId);
            return View(historia);
        }

        // POST: Historia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoriaId,FechaCreacion,Diagnostico,Medicamentos,MascotaId")] Historia historia)
        {
            if (id != historia.HistoriaId)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(historia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {   
                    ModelState.AddModelError("",e.Message);
                    if (!HistoriaExists(historia.HistoriaId))
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
            ViewData["MascotaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", historia.MascotaId);
            return View(historia);
        }

        // GET: Historia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Historias == null)
            {
                return NotFound();
            }

            var historia = await _context.Historias
                .Include(h => h.Mascota)
                .FirstOrDefaultAsync(m => m.HistoriaId == id);
            if (historia == null)
            {
                return NotFound();
            }

            return View(historia);
        }

        // POST: Historia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Historias == null)
            {
                return Problem("Entity set 'MVCMascotasContext.Historias'  is null.");
            }
            var historia = await _context.Historias.FindAsync(id);
            if (historia != null)
            {
                _context.Historias.Remove(historia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriaExists(int id)
        {
          return (_context.Historias?.Any(e => e.HistoriaId == id)).GetValueOrDefault();
        }
    }
}
