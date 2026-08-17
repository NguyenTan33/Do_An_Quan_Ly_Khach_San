using DoAnQuanLyKhachSan.Models;
using DoAnQuanLyKhachSan.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DoAnQuanLyKhachSan.Controllers
{
    public class TongQuanController : Controller
    {
        private KhachSanEntities db = new KhachSanEntities();

        public ActionResult Index()
        {
            var tangList = db.Floors.OrderBy(t => t.FloorName).ToList();

            // Lấy thời gian hiện tại
            var now = DateTime.Now;

            // Duyệt từng tầng
            var result = tangList.Select(t => new RoomOverviewViewModel
            {
                TenTang = t.FloorName,
                Phong = t.Rooms.Select(p =>
                {
                    // Kiểm tra xem phòng có booking nào đang dùng không
                    var booking = db.Bookings.FirstOrDefault(b =>
                        b.RoomId == p.RoomId &&
                        b.CheckInDate <= now &&
                        b.CheckOutDate >= now &&
                        b.Status == "Đang dùng");

                    return new RoomOverviewViewModel.RoomInfo
                    {
                        SoPhong = p.RoomNumber,
                        TrangThai = booking != null ? "Đang dùng" : "Trống",
                        IsBooked = booking != null,
                        BookingId = booking?.BookingId // Gán bookingId nếu có
                    };
                }).ToList()
            }).ToList();

            return View(result);
        }
    }
}
