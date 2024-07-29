using System.ComponentModel.DataAnnotations;

namespace StokTakip.Models
{
    public class Kisi
    {
        public int Id { get; set; }
        
        [Required]
        public string AdSoyad { get; set; }

        public string Email { get; set; }
        public ICollection<Cihaz> Cihazlar { get; set; }


    }
}
