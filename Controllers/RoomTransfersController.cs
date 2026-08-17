using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DoAnQuanLyKhachSan.Models; 
using DoAnQuanLyKhachSan.Models.ViewModel; 
namespace DoAnQuanLyKhachSan.Controllers
{
    public class RoomTransfersController : Controller
    {
        private KhachSanEntities db = new KhachSanEntities();

        // GET: RoomTransfers/Create
        public ActionResult Create(int bookingId)
        {
            var booking = db.Bookings.Find(bookingId);
            if (booking == null)
            {
                return HttpNotFound();
            }

            var currentRoomId = booking.RoomId;

            // Lấy danh sách phòng trống (tức là không có booking nào trong thời gian hiện tại)
            var availableRooms = db.Rooms
                .Where(r => !db.Bookings
                    .Any(b => b.RoomId == r.RoomId &&
                              b.Status != "Đã trả" &&
                              b.CheckInDate <= DateTime.Now &&
                              b.CheckOutDate >= DateTime.Now))
                .ToList();

            var viewModel = new RoomTransferViewModel
            {
                BookingId = bookingId,
                FromRoomId = (int)currentRoomId,
                AvailableRooms = availableRooms.Select(r => new SelectListItem
                {
                    Value = r.RoomId.ToString(),
                    Text = r.RoomNumber
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: RoomTransfers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomTransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var booking = db.Bookings.Find(model.BookingId);
                if (booking == null)
                {
                    return HttpNotFound();
                }

                // Ghi nhận lịch sử chuyển phòng
                var transfer = new RoomTransfer
                {
                    BookingId = model.BookingId,
                    FromRoomId = model.FromRoomId,
                    ToRoomId = model.ToRoomId,
                    TransferDate = DateTime.Now,
                    Note = "Chuyển phòng theo yêu cầu khách"
                };

                // Cập nhật lại phòng của booking
                booking.RoomId = model.ToRoomId;

                db.RoomTransfers.Add(transfer);
                db.SaveChanges();

                return RedirectToAction("Index", "TongQuan");
            }

            return View(model);
        }
    }
}
