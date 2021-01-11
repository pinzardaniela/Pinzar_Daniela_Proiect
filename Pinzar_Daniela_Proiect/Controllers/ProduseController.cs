using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pinzar_Daniela_Proiect.Data;
using Pinzar_Daniela_Proiect.Models;

namespace Pinzar_Daniela_Proiect.Controllers
{
    public class ProduseController : Controller
    {
        private readonly MagazinContext _context;

        public ProduseController(MagazinContext context)
        {
            _context = context;
        }

        // GET: Produs
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)

        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DenumireSortParm"] = String.IsNullOrEmpty(sortOrder) ? "denumire_desc" : "";
            ViewData["PretSortParm"] = sortOrder == "Pret" ? "pret_desc" : "Pret";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var produse = from b in _context.Produse
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                produse = produse.Where(s => s.Denumire.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "denumire_desc":
                    produse = produse.OrderByDescending(b => b.Denumire);
                    break;
                case "Pret":
                    produse = produse.OrderBy(b => b.Pret);
                    break;
                case "pret_desc":
                    produse = produse.OrderByDescending(b => b.Pret);
                    break;
                default:
                    produse = produse.OrderBy(b => b.Denumire);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<Produs>.CreateAsync(produse.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Produs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produse
            .Include(s => s.Comenzi)
            .ThenInclude(e => e.Client)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            if (produs == null)
            {
                return NotFound();
            }

            return View(produs);
        }

        // GET: Produs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Denumire,Furnizor,Pret")] Produs produs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(produs);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }
            return View(produs);
        }

        // GET: Produs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produse.FindAsync(id);
            if (produs == null)
            {
                return NotFound();
            }
            return View(produs);
        }

        // POST: Produs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Produse.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Produs>(studentToUpdate,"",s => s.Furnizor, s => s.Denumire, s => s.Pret))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Produs/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produse
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produs == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(produs);
        }

        // POST: Produs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produs = await _context.Produse.FindAsync(id);
            if (produs == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            { 
            _context.Produse.Remove(produs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ProdusExists(int id)
        {
            return _context.Produse.Any(e => e.ID == id);
        }
    }
}
