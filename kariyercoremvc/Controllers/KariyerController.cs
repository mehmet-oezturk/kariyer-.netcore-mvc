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
    public class KariyerController : Controller
    {
        private readonly KariyerDbContext _context;// alanın okunabilirliğini sağlıyor

        private readonly IWebHostEnvironment _webHost;
        public KariyerController(KariyerDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            //_webHost = webHost;
        }
        public IActionResult Index()
        {
            List<BasvuruSahibi> applicants;
            applicants = _context.BasvuruSahibis.ToList();
            return View(applicants);
            //return View();
        }
       [HttpGet]
       public IActionResult Create()
        {
            BasvuruSahibi basvuruSahibi = new BasvuruSahibi();
            basvuruSahibi.Deneyim.Add(new Deneyim() { DeneyimId = 1 });//burayı boş bıraksakta olurdu komple

            return View(basvuruSahibi);
        }
        [HttpPost]
        public IActionResult Create(BasvuruSahibi basvuruSahibi)
        {
            _context.Add(basvuruSahibi);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public async Task<IActionResult>Delete(int id)
        {
            if(id==0)
            {
                return NotFound();
            }
            var user = await _context.BasvuruSahibis.FirstOrDefaultAsync(m => m.Id == id);
            if(user==null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost,ActionName("Delete")]//normalde metot ismi DeleteConfirmend ama buradan dolayı ismi delete olarak geçiyor
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmend(int id)
        {
            var user = await
                _context.BasvuruSahibis.FirstOrDefaultAsync(m => m.Id == id);
            _context.BasvuruSahibis.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));//işlem bittikten sonra name of string olarak döndürüyor
        }
        public async Task<IActionResult>Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var applicant = await
                _context.BasvuruSahibis.FindAsync(id);
            if(applicant==null)
            {
                return NotFound();
            }
            return View(applicant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //async hızlı çalışmayı sağlıyor   Bind (modellere viewlerin gitmesini sağlar)
        //hangi veriler hangi kolonlarlar çalışacağımızı verileri ayarlar
        public async Task<IActionResult>Edit (int id, [Bind("Id,AdSoyad,Cinsiyet,Yas,ToplamTecrube")] 
        BasvuruSahibi basvuruSahibi)
        {
            _context.Update(basvuruSahibi);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
