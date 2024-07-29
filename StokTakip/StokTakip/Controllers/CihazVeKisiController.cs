using Microsoft.AspNetCore.Mvc;
using StokTakip.Data;
using StokTakip.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakip.Controllers
{
    public class CihazVeKisiController : Controller
    {
        private readonly AppDbContext _context;

        public CihazVeKisiController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cihazlarVeKisiler = await _context.Cihazlar
                .Include(c => c.Kisi)
                .ToListAsync();

            var viewModel = cihazlarVeKisiler.Select(c => new CihazKisiView
            {
                CihazAdi = c.CihazAdi,
                CihazModeli = c.CihazModeli,
                CihazDurumu = c.CihazDurumu,
                KisiId = c.KisiId,
                KisiAdSoyad = c.Kisi.AdSoyad,
                Email = c.Kisi.Email
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cihaz = await _context.Cihazlar
                .Include(c => c.Kisi)
                .FirstOrDefaultAsync(m => m.KisiId == id);

            if (cihaz == null)
            {
                return NotFound();
            }

            return View(cihaz);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CihazId)
        {
            var cihaz = await _context.Cihazlar.FindAsync(CihazId);

            if (cihaz != null)
            {
                var kisi = await _context.Kisiler.FindAsync(cihaz.KisiId);
                if (kisi != null)
                {
                    _context.Kisiler.Remove(kisi);
                }

                _context.Cihazlar.Remove(cihaz);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }





        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CihazAdi,CihazModeli,CihazDurumu,KisiAdSoyad,Email")] CihazKisiView model)
        {
            if (ModelState.IsValid)
            {
                var kisi = new Kisi
                {
                    AdSoyad = model.KisiAdSoyad,
                    Email = model.Email
                };

                _context.Kisiler.Add(kisi);
                await _context.SaveChangesAsync();

                var cihaz = new Cihaz
                {
                    CihazAdi = model.CihazAdi,
                    CihazModeli = model.CihazModeli,
                    CihazDurumu = model.CihazDurumu,
                    KisiId = kisi.Id
                };

                _context.Cihazlar.Add(cihaz);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
    }
