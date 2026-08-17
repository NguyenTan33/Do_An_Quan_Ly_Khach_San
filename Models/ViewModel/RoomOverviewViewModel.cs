using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnQuanLyKhachSan.Models.ViewModel
{
    public class RoomOverviewViewModel
    {
        public string TenTang { get; set; }
        public List<RoomInfo> Phong { get; set; }

        public class RoomInfo
        {
            public string SoPhong { get; set; }
            public string TrangThai { get; set; }
            public bool IsBooked { get; set; }
            public int? BookingId { get; set; } 
        }
    }
}
