using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokTakip.Data;
using StokTakip.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakip.Controllers
{
    public class KisiController : Controller
    {
        private readonly AppDbContext _context;

        public KisiController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kisiler = await _context.Kisiler.ToListAsync();
            return View(kisiler);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kisi kisi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kisi);
        }
    }
}