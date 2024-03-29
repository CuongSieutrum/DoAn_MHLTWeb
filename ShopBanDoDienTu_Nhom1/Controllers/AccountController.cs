using ShopBanDoDienTu_Nhom1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanDoDienTu_Nhom1.Controllers
{
    public class AccountController : Controller
    {
        MyDataDataContext db = new MyDataDataContext();
        // HTTP GET /Account/Login 
        public ActionResult Login()
        {
            return View();
        }

        // HTTP POST /Account/Login 
        [HttpPost]
        public ActionResult Login(TaiKhoan taikhoan)
        {
            var tk = taikhoan.TaiKhoan1;
            var mk = taikhoan.MatKhau;

            // Kiểm tra xem tài khoản có tồn tại trong cơ sở dữ liệu không
            var taikhoanCheck = db.TaiKhoans.SingleOrDefault(x => x.TaiKhoan1 == tk && x.MatKhau == mk);

            if (taikhoanCheck != null)
            {
                // Đăng nhập thành công, chuyển hướng đến trang chính
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Đăng nhập thất bại, hiển thị thông báo lỗi trên view
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
                return View(taikhoan);
            }
        }


        // HTTP GET /Account/Login 
        public ActionResult Register()
        {
            return View();
        }

        // HTTP POST /Account/Login
        [HttpPost]
        public ActionResult Register(TaiKhoan taikhoan, FormCollection collection)
        {
            var tk = collection["TaiKhoan1"];
            var mk = collection["MatKhau"];
            var email = collection["Email"];
            var vaitro = collection["VaiTro"];
            if (string.IsNullOrEmpty(tk) && string.IsNullOrEmpty(mk))
            {
                ViewBag["Error"] = "Lỗi đăng nhập do thiếu hoặc sai thông tin, yêu cầu kiểm tra lại!";
            }
            else
            {
                taikhoan.TaiKhoan1 = tk.ToString();
                taikhoan.MatKhau = mk.ToString();
                taikhoan.Email = email.ToString();
                taikhoan.VaiTro = vaitro.ToString();
                db.TaiKhoans.InsertOnSubmit(taikhoan);
                db.SubmitChanges();
                return RedirectToAction("Login");
            }
            return this.Register();
        }
    }
}