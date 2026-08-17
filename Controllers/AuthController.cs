using DoAnQuanLyKhachSan.Models.ViewModel;
using System.Linq;
using System.Web.Mvc;
using DoAnQuanLyKhachSan.Models;

public class AuthController : Controller
{
    private KhachSanEntities db = new KhachSanEntities();

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)

            return View(model);

        var user = db.Users.FirstOrDefault(u =>
            u.Username == model.Username && u.PasswordHash == model.Password);

        if (user == null)
        {
            ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu");
            return View(model);
        }

        // Lưu thông tin user và role vào Session
        Session["UserId"] = user.UserId;
        Session["Username"] = user.Username;
        Session["RoleName"] = db.Roles.Find(user.RoleId).RoleName; 

        return RedirectToAction("Index", "Home");
    }

    public ActionResult Logout()
    {
        Session.Clear();
        return RedirectToAction("Login");
    }
}
