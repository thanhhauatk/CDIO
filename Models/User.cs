using System.ComponentModel.DataAnnotations;

namespace CDIO.Models
{
    public class User
    {
        [Key]  // Đánh dấu MaTK là khóa chính
        public int MaTK { get; set; }

        [Required]
        [MaxLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [MaxLength(255)]
        public string MatKhau { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Quyen { get; set; }

        public DateTime NgayTao { get; set; }
    }
}
