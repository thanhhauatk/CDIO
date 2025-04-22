using System.ComponentModel.DataAnnotations;

namespace CDIO.Models
{
    public enum RoomStatus
    {
        [Display(Name = "Trống")]
        Trong = 0,   // Trống

        [Display(Name = "Đã đặt")]
        DaDat = 1,   // Đã đặt

        [Display(Name = "Đang dọn")]
        DangDon = 2, // Đang dọn

        [Display(Name = "Bảo trì")]
        BaoTri = 3   // Bảo trì
    }
}
