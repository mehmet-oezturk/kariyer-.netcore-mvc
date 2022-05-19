using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kariyercoremvc.Models;//bu 3 kütüphaneyi ekliyoruz
using kariyercoremvc.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace kariyercoremvc.Controllers
{
    public class DeneController : Controller
    {
        private readonly KariyerDbContext _context;// alanın okunabilirliğini sağlıyor

        private readonly IWebHostEnvironment _webHost;
        public DeneController(KariyerDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            //_webHost = webHost;
        }
        public IActionResult Index()
        {
            List<Deneyim> applicants;
            applicants = _context.Deneyims.ToList();
            return View(applicants);
            //return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
           return View();
        }
        [HttpPost]
        public IActionResult Create(Deneyim deneyim)
        {
            _context.Add(deneyim);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var user = await _context.Deneyims.FirstOrDefaultAsync(m => m.DeneyimId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]//normalde metot ismi DeleteConfirmend ama buradan dolayı ismi delete olarak geçiyor
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmend(int id)
        {
            var user = await
                _context.Deneyims.FirstOrDefaultAsync(m => m.DeneyimId == id);
            _context.Deneyims.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var applicant = await
                _context.Deneyims.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //async hızlı çalışmayı sağlıyor   Bind (modellere viewlerin gitmesini sağlar)
        //hangi veriler hangi kolonlarlar çalışacağımızı verileri ayarlar
        public async Task<IActionResult> Edit(int id, [Bind("DeneyimId,BSId,FirmaAdlari,CalismaTanimi,CalismaYili")]
        Deneyim deneyim)
        {
            _context.Update(deneyim);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
