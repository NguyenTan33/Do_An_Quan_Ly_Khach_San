using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnQuanLyKhachSan.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}