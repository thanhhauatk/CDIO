using System.ComponentModel.DataAnnotations;

namespace CDIO.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string RoomName { get; set; }

        public string RoomType { get; set; }

        public decimal? Price { get; set; }

        public int Status { get; set; } // 0=Trống, 1=Đã đặt, 2=Đang dọn, 3=Bảo trì

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
