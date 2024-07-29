using System.ComponentModel.DataAnnotations;

namespace StokTakip.Models
{
    public class Cihaz
    {
        public int CihazId { get; set; }

        [Required]
        public string CihazAdi { get; set; }
        public string CihazModeli { get; set; }
        public string CihazDurumu { get; set; }

        public int KisiId { get; set; }
        public Kisi Kisi { get; set; }
    }
}
