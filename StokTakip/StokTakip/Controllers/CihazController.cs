using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokTakip.Data;
using StokTakip.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakip.Controllers
{
    public class CihazController : Controller
    {
        private readonly AppDbContext _context;

        public CihazController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cihazlar = await _context.Cihazlar
                .Include(c => c.Kisi) 
                .ToListAsync();
            return View(cihazlar);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CihazId,CihazAdi,CihazModeli,CihazDurumu")] Cihaz cihaz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cihaz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cihaz);
        }
    }
}