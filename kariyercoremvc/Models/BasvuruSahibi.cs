using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kariyercoremvc.Models
{
    public class BasvuruSahibi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string AdSoyad { get; set; } = "";// = ""; string bi değer gelecek

        [Required]
        [StringLength(10)]
        public string Cinsiyet { get; set; }
        [Required]
        [Range(25,55,ErrorMessage ="bu pozisyona yaşınız uygun değil")]//girilen yaşın 25 ile 55 arası olmasını sağlıyor eğer değilse error mesaj verecek
        [DisplayName("Yıl olarak")]
        public int Yas { get; set; }

        [Required]
        [Range(1,25,ErrorMessage ="gecersiz")]
        [DisplayName("Toplam Çalışma Yılı")]
        public int ToplamTecrube { get; set; }
        public virtual List<Deneyim> Deneyim { get; set; } = new List<Deneyim>();//list deneyim bire çok ilişkiden dolayı oluyor
    }
}
