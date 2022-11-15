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
    public class MedicoController : Controller
    {
        private readonly MVCMascotasContext _context;

        public IEnumerable<Mascota> listaMascotas { get; set; }

        public MedicoController(MVCMascotasContext context)
        {
            _context = context;
        }

        // GET: Medico
        public async Task<IActionResult> Index()
        {
              return _context.Medicos != null ? 
                          View(await _context.Medicos.ToListAsync()) :
                          Problem("Entity set 'MVCMascotasContext.Medicos'  is null.");
        }

        // GET: Medico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // GET: Medico/DetailsList/5
        public async Task<IActionResult> DetailsList(int? id)
        {
            var medico = await _context.Medicos.FirstOrDefaultAsync(m => m.MedicoId == id);
            listaMascotas = _context.Mascotas.Where(m => m.MedicoId == id).ToList();
            return View(medico);
        }

        // GET: Medico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicoId,Nombres,Apellidos,Direccion,Telefono,Tarjeta")] Medico medico)
        {
            if (medico!=null)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicoId,Nombres,Apellidos,Direccion,Telefono,Tarjeta")] Medico medico)
        {
            if (id != medico.MedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.MedicoId))
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
            return View(medico);
        }

        // GET: Medico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medicos == null)
            {
                return Problem("Entity set 'MVCMascotasContext.Medicos'  is null.");
            }
            var medico = await _context.Medicos.FindAsync(id);
            if (medico != null)
            {
                _context.Medicos.Remove(medico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
          return (_context.Medicos?.Any(e => e.MedicoId == id)).GetValueOrDefault();
        }
    }
}
