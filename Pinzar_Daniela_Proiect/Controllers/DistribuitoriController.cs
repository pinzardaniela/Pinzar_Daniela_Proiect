using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pinzar_Daniela_Proiect.Data;
using Pinzar_Daniela_Proiect.Models;
using Pinzar_Daniela_Proiect.Models.MagazinViewModels;

namespace Pinzar_Daniela_Proiect.Controllers
{
    public class DistribuitoriController : Controller
    {
        private readonly MagazinContext _context;

        public DistribuitoriController(MagazinContext context)
        {
            _context = context;
        }

        // GET: Distribuitors
        public async Task<IActionResult> Index(int? id, int? produsID)
        {
            var viewModel = new DistribuitorIndexData();
            viewModel.Distribuitori = await _context.Distribuitori.Include(i => i.DistribuitorProduse).ThenInclude(i => i.Produs).ThenInclude(i => i.Comenzi).ThenInclude(i => i.Client).AsNoTracking().OrderBy(i => i.NumeDistribuitor).ToListAsync();

            if (id != null)
            {
                ViewData["DistribuitorID"] = id.Value;
                Distribuitor distribuitor = viewModel.Distribuitori.Where( i => i.ID == id.Value).Single();
                viewModel.Produse = distribuitor.DistribuitorProduse.Select(s => s.Produs);
            }
            if (produsID != null)
            {
                ViewData["ProdusID"] = produsID.Value;
                viewModel.Comenzi = viewModel.Produse.Where(x => x.ID == produsID).Single().Comenzi;
            }
            return View(viewModel);
        }

        // GET: Distribuitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distribuitor = await _context.Distribuitori
                .FirstOrDefaultAsync(m => m.ID == id);
            if (distribuitor == null)
            {
                return NotFound();
            }

            return View(distribuitor);
        }

        // GET: Distribuitors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Distribuitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NumeDistribuitor,Adresa")] Distribuitor distribuitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(distribuitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(distribuitor);
        }

        // GET: Distribuitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var distribuitor = await _context.Distribuitori
            .Include(i => i.DistribuitorProduse).ThenInclude(i => i.Produs)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (distribuitor == null)
            {
                return NotFound();
            }
            PopulateDistribuitorProdusData(distribuitor);
            return View(distribuitor);

        }

        private void PopulateDistribuitorProdusData(Distribuitor distribuitor)
        {
            var allProduse = _context.Produse;
            var distribuitorProduse = new HashSet<int>(distribuitor.DistribuitorProduse.Select(c => c.ProdusID));
            var viewModel = new List<DistribuitorProdusData>();
            foreach (var produs in allProduse)
            {
                viewModel.Add(new DistribuitorProdusData
                {
                    ProdusID = produs.ID,
                    Denumire = produs.Denumire,
                    IsDistribuited = distribuitorProduse.Contains(produs.ID)
                });
            }
            ViewData["Produse"] = viewModel;
        }

        // POST: Distribuitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedProducts)
        {
            if (id == null)
            {
                return NotFound();
            }
            var distribuitorToUpdate = await _context.Distribuitori
            .Include(i => i.DistribuitorProduse)
            .ThenInclude(i => i.Produs)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Distribuitor>(
            distribuitorToUpdate,
            "",
            i => i.NumeDistribuitor, i => i.Adresa))
            {
                UpdateDistribuitorProduse(selectedProducts, distribuitorToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateDistribuitorProduse(selectedProducts, distribuitorToUpdate);
            PopulateDistribuitorProdusData(distribuitorToUpdate);
            return View(distribuitorToUpdate);
        }
        private void UpdateDistribuitorProduse(string[] selectedProducts, Distribuitor distribuitorToUpdate)
        {
            if (selectedProducts == null)
            {
                distribuitorToUpdate.DistribuitorProduse = new List<DistribuitorProdus>();
                return;
            }
            var selectedProductsHS = new HashSet<string>(selectedProducts);
            var distribuitorProduse = new HashSet<int>
            (distribuitorToUpdate.DistribuitorProduse.Select(c => c.Produs.ID));
            foreach (var produs in _context.Produse)
            {
                if (selectedProductsHS.Contains(produs.ID.ToString()))
                {
                    if (!distribuitorProduse.Contains(produs.ID))
                    {
                        distribuitorToUpdate.DistribuitorProduse.Add(new DistribuitorProdus
                        {
                            DistribuitorID =
                       distribuitorToUpdate.ID,
                            ProdusID = produs.ID
                        });
                    }
                }
                else
                {
                    if (distribuitorProduse.Contains(produs.ID))
                    {
                        DistribuitorProdus produsToRemove = distribuitorToUpdate.DistribuitorProduse.FirstOrDefault(i
                       => i.ProdusID == produs.ID);
                        _context.Remove(produsToRemove);
                    }
                }
            }
        }

        // GET: Distribuitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distribuitor = await _context.Distribuitori
                .FirstOrDefaultAsync(m => m.ID == id);
            if (distribuitor == null)
            {
                return NotFound();
            }

            return View(distribuitor);
        }

        // POST: Distribuitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var distribuitor = await _context.Distribuitori.FindAsync(id);
            _context.Distribuitori.Remove(distribuitor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistribuitorExists(int id)
        {
            return _context.Distribuitori.Any(e => e.ID == id);
        }
    }
}
