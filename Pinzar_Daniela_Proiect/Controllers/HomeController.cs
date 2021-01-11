using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pinzar_Daniela_Proiect.Models;
using Microsoft.EntityFrameworkCore;
using Pinzar_Daniela_Proiect.Data;
using Pinzar_Daniela_Proiect.Models.MagazinViewModels;


namespace Pinzar_Daniela_Proiect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MagazinContext _context;

        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
            from comanda in _context.Comenzi
            group comanda by comanda.DataComenzii into dateGroup
            select new OrderGroup()
            {
                DataComenzii = dateGroup.Key,
                ProduseCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public HomeController(ILogger<HomeController> logger, MagazinContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chat()
        {
            return View();
        }

    }
}
