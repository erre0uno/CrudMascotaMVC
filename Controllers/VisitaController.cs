using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMascota.Models;

namespace MVCMascota.Controllers
{
    public class VisitaController : Controller
    {
        private readonly MVCMascotasContext _context;

        public VisitaController(MVCMascotasContext context)
        {
            _context = context;
        }

        // GET: Visita
        public async Task<IActionResult> Index()
        {
            var mVCMascotasContext = _context.Visitas.Include(v => v.Historia).Include(v => v.Medico);
            return View(await mVCMascotasContext.ToListAsync());
        }

        // GET: Visita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Visitas == null)
            {
                return NotFound();
            }

            var visita = await _context.Visitas
                .Include(v => v.Historia)
                .Include(v => v.Medico)
                .FirstOrDefaultAsync(m => m.VisitaId == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // GET: Visita/Create
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId");
            ViewData["HistoriaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId");
            //ViewData["HistoriaId"] = new SelectList(_context.Historias, "HistoriasId", "HistoriasId");
            return View();
        }

        // POST: Visita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitaId,FechaVisita,Temperatura,Peso,FrecuenciaCardiaca,FrecuenciaRespiratoria,EstadoAnimo,Recomendacion,MedicoId,HistoriaId")] Visita visita)
        {
            string mensaje = "";
            /*
                        using (var context = new _context. Mascotas())
                        {
                            var blogs = context.Blogs
                                .Where(b => b.Url.Contains("dotnet"))
                                .ToList();
                        }
            */

            var valida_mascota = _context.Mascotas.FirstOrDefault(m => m.MascotaId == visita.HistoriaId);
            Console.WriteLine("macota id=" + valida_mascota.MascotaId + " con medico id= " + valida_mascota.MedicoId);
            Console.WriteLine("medico id=" + visita.MedicoId);

            //var valido = _context.Historias.FirstOrDefault(h => h.MascotaId == historia.MascotaId);

            if (valida_mascota.MedicoId == visita.MedicoId)
            {
                var valida_historia = _context.Historias.FirstOrDefault(h => h.MascotaId == valida_mascota.MascotaId);
                if (valida_historia != null)
                {
                    // if original
                    visita.HistoriaId = valida_historia.HistoriaId;
                    _context.Add(visita);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    mensaje = "La mascota seleccionada aun no tiene una historia asignada";
                    ModelState.AddModelError("", mensaje);
                    ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", visita.MedicoId);
                    ViewData["HistoriaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", visita.HistoriaId);
                    return View(visita);
                }

            }

            mensaje = "Medico id=" + visita.MedicoId + " | No esta asignado mascota a la mascota seleccionada";
            ModelState.AddModelError("", mensaje);

            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", visita.MedicoId);
            ViewData["HistoriaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", visita.HistoriaId);
            //ViewData["HistoriaId"] = new SelectList(_context.Historias, "HistoriaId", "HistoriaId", visita.HistoriaId);
            return View(visita);
        }

        // GET: Visita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Visitas == null)
            {
                return NotFound();
            }

            var visita = await _context.Visitas.FindAsync(id);
            if (visita == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", visita.MedicoId);
            //ViewData["HistoriaId"] = new SelectList(_context.Historias, "HistoriaId", "HistoriaId", visita.HistoriaId);
            ViewData["HistoriaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", visita.HistoriaId);
            return View(visita);
        }

        // POST: Visita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitaId,FechaVisita,Temperatura,Peso,FrecuenciaCardiaca,FrecuenciaRespiratoria,EstadoAnimo,Recomendacion,MedicoId,HistoriaId")] Visita visita)
        {
            string mensaje="";
            
            if (id != visita.VisitaId)
            {
                return NotFound();
            }

            var valida_mascota = _context.Mascotas.FirstOrDefault(m => m.MascotaId == visita.HistoriaId);

            if (valida_mascota.MedicoId == visita.MedicoId)
            {
                var valida_historia = _context.Historias.FirstOrDefault(h => h.MascotaId == valida_mascota.MascotaId);

                if (valida_historia != null)
                {
                    visita.HistoriaId = valida_historia.HistoriaId;
                    try
                    {
                        _context.Update(visita);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        ModelState.AddModelError("", e.Message);

                        if (!VisitaExists(visita.VisitaId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    mensaje = "La mascota seleccionada aun no tiene una historia asignada";
                    ModelState.AddModelError("", mensaje);
                    ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", visita.MedicoId);
                    ViewData["HistoriaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", visita.HistoriaId);
                    return View(visita);
                }
            }

            mensaje = "Medico id=" + visita.MedicoId + " | No esta asignado mascota a la mascota seleccionada";
            ModelState.AddModelError("", mensaje);

            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", visita.MedicoId);
            ViewData["HistoriaId"] = new SelectList(_context.Mascotas, "MascotaId", "MascotaId", visita.HistoriaId);
            //ViewData["HistoriaId"] = new SelectList(_context.Historias, "HistoriaId", "HistoriaId", visita.HistoriaId);
            return View(visita);            
        }

        // GET: Visita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Visitas == null)
            {
                return NotFound();
            }

            var visita = await _context.Visitas
                .Include(v => v.Historia)
                .Include(v => v.Medico)
                .FirstOrDefaultAsync(m => m.VisitaId == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // POST: Visita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Visitas == null)
            {
                return Problem("Entity set 'MVCMascotasContext.Visitas'  is null.");
            }
            var visita = await _context.Visitas.FindAsync(id);
            if (visita != null)
            {
                _context.Visitas.Remove(visita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitaExists(int id)
        {
            return (_context.Visitas?.Any(e => e.VisitaId == id)).GetValueOrDefault();
        }
    }
}
