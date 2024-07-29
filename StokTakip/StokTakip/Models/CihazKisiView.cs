using System.ComponentModel.DataAnnotations;

namespace StokTakip.Models
{
    public class CihazKisiView
    {
        [Required]
        public string CihazAdi { get; set; }
        public string CihazModeli { get; set; }
        public string CihazDurumu { get; set; }

        [Required]
        public int KisiId { get; set; }
        public string KisiAdSoyad { get; set; }
        public string Email { get; set; }

    }
}
