using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Farmaciaaa.Models;
using Microsoft.AspNetCore.Authorization;

namespace Farmaciaaa.Controllers
{
    public class FarmacieController : Controller
    {
        private readonly FarmacieContext _context;

        public FarmacieController(FarmacieContext context)
        {
            _context = context;
        }

        // GET: Farmacie
        public async Task<IActionResult> Index()
        {
            var farmacieContext = _context.Farmacies.Include(f => f.IdComuneNavigation);
            return View(await farmacieContext.ToListAsync());
        }

        // GET: Farmacie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmacie == null)
            {
                return NotFound();
            }

            return View(farmacie);
        }

        // GET: Farmacie/Create
        public IActionResult Create()
        {
            ViewData["IdComune"] = new SelectList(_context.Comunes, "Id", "Id");
            return View();
        }

        // POST: Farmacie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codiceidentificativofarmacia,Codfarmaciaassegnatodaasl,Indirizzo,Descrizionefarmacia,Partitaiva,Cap,IdComune,Datainiziovalidita,Datafinevalidita,Descrizionetipologia,Codicetipologia,Latitudine,Longitudine,Localize")] Farmacie farmacie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmacie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdComune"] = new SelectList(_context.Comunes, "Id", "Id", farmacie.IdComune);
            return View(farmacie);
        }

        // GET: Farmacie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacie = await _context.Farmacies.FindAsync(id);
            if (farmacie == null)
            {
                return NotFound();
            }
            ViewData["IdComune"] = new SelectList(_context.Comunes, "Id", "Id", farmacie.IdComune);
            return View(farmacie);
        }

        // POST: Farmacie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codiceidentificativofarmacia,Codfarmaciaassegnatodaasl,Indirizzo,Descrizionefarmacia,Partitaiva,Cap,IdComune,Datainiziovalidita,Datafinevalidita,Descrizionetipologia,Codicetipologia,Latitudine,Longitudine,Localize")] Farmacie farmacie)
        {
            if (id != farmacie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmacie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmacieExists(farmacie.Id))
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
            ViewData["IdComune"] = new SelectList(_context.Comunes, "Id", "Id", farmacie.IdComune);
            return View(farmacie);
        }

        // GET: Farmacie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmacie == null)
            {
                return NotFound();
            }

            return View(farmacie);
        }

        // POST: Farmacie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmacie = await _context.Farmacies.FindAsync(id);
            if (farmacie != null)
            {
                _context.Farmacies.Remove(farmacie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmacieExists(int id)
        {
            return _context.Farmacies.Any(e => e.Id == id);
        }





        [HttpGet]
        public IActionResult SearchByRegione()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchByRegione(string regione)
        {
            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                .ThenInclude(p => p.IdRegioneNavigation)
                .Where(f => f.IdComuneNavigation.IdProvinciaNavigation.IdRegioneNavigation.Denominazione == regione)
                .ToListAsync();

            return View("Index", farmacie);
        }

        [HttpGet]
        public IActionResult SearchByProvincia()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchByProvincia(string provincia)
        {
            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                .ThenInclude(p => p.IdRegioneNavigation)
                .Where(f => f.IdComuneNavigation.IdProvinciaNavigation.Denominazione == provincia)
                .ToListAsync();

            return View("Index", farmacie);
        }

        [HttpGet]
        public IActionResult SearchByComune()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchByComune(string comune)
        {
            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                .ThenInclude(p => p.IdRegioneNavigation)
                .Where(f => f.IdComuneNavigation.Denominazione == comune)
                .ToListAsync();

            return View("Index", farmacie);
        }

        [HttpGet]
        public IActionResult RicercaNome()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RicercaNome(string denominazione)
        {
            var farmacie = await _context.Farmacies
                .Include(f => f.IdComuneNavigation)
                .ThenInclude(c => c.IdProvinciaNavigation)
                .ThenInclude(p => p.IdRegioneNavigation)
                .Where(f => f.Descrizionefarmacia.Contains(denominazione))
                .ToListAsync();

            return View("Index", farmacie);
        }
    }
}


