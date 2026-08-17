using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnQuanLyKhachSan.Models.ViewModel
{
    public class RoomTransferViewModel
    {
         public int BookingId { get; set; }
    public int FromRoomId { get; set; }
    public int ToRoomId { get; set; }
    public List<SelectListItem> AvailableRooms { get; set; } // Các phòng có thể chuyển đến
    }
}