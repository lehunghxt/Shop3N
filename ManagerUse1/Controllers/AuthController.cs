using Einvoince.Web.Security;
using ManagerUse1.Enums;
using Shop.Domain;
using Shop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace ManagerUse1.Controllers
{
    public class AuthController : Controller
    {
        public UserBLL _userBLL;
        public SecurityProvider securityProvider;
        public AuthController()
        {
            _userBLL = new UserBLL();
            securityProvider = new SecurityProvider();
        }
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        // GET: Auth/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Auth/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auth/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Auth/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auth/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Auth/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            try
            {
                const int workFactor = 13;
                user.Status = (int)EnumUserStatus.Active;
                user.UserType = (int)EnumUserType.SupperAdmin;
                user.CreateDate = _userBLL.Now;
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor);
                _userBLL.RegisterUser(user);
                return RedirectToAction("Register", "Auth");
            }
            catch (Exception ex)
            {

            }
            
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            try
            {
                var check = securityProvider.ValidateUser(user.Username, user.Password, true);
                if (check == 0)
                    return Redirect("/Product/Index");
                else if (check == -2 || check == -1) ViewBag.Warning = "Tài khoản hoặc mật khẩu không đúng !";
                else if (check == 1) ViewBag.Warning = "Tài khoản đã bị khoá.";
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
    }
}
