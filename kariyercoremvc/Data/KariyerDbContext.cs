using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kariyercoremvc.Models;//kütüphane indirmek gerekiyor ki sql de tabloları oluşturabilsin
using Microsoft.EntityFrameworkCore;

namespace kariyercoremvc.Data
{
    public class KariyerDbContext :DbContext//tablolar burada dbcontexten kalıtım alarak oluşuyor
    {
        public KariyerDbContext(DbContextOptions<KariyerDbContext>options):base(options)
        {

        }

        public virtual DbSet<BasvuruSahibi> BasvuruSahibis { get; set; }

        public virtual DbSet<Deneyim>Deneyims { get; set; }

    }
}
