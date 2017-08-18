using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using auction.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace auction.Controllers
{
    public class HomeController : Controller
    {
        private AuctionContext _context;

        public HomeController(AuctionContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterProcess(RegUser NewUser)
        {
            if (ModelState.IsValid)
            {
                List<User> UserExists = _context.Users.Where(theuser => theuser.Username == NewUser.Username).ToList();
                if (UserExists.Count > 0)
                {
                    ViewBag.ErrorRegister = "Username already exists...";
                    return View("Index");
                }
                PasswordHasher<RegUser> Hasher = new PasswordHasher<RegUser>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                User user = new User
                {
                    First = NewUser.First,
                    Last = NewUser.Last,
                    Username = NewUser.Username,
                    Password = NewUser.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                User created = _context.Users.Single(u => u.Username == NewUser.Username);
                HttpContext.Session.SetInt32("id", (int)created.UserId);
                HttpContext.Session.SetString("User", (string)created.First);
                System.Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$");
                return RedirectToAction("Dashboard", "Auction");
            }
            return View("Index");
        }

        [HttpPost]
        [Route("loginprocess")]
        public IActionResult LoginProcess(string LUsername = null, string Password = null)
        {
            if(Password != null && LUsername != null)
            {
                List<User> UserExists = _context.Users.Where(u => u.Username == LUsername).ToList();
                if (UserExists.Count > 0)
                {
                    var Hasher  = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(UserExists[0], UserExists[0].Password, Password))
                    {
                        HttpContext.Session.SetInt32("id", (int)UserExists[0].UserId);
                        HttpContext.Session.SetString("User", (string)UserExists[0].First);
                        return RedirectToAction("Dashboard", "Auction");
                    }
                }
            }
            ViewBag.LoginError = "Invalid Login...";
            return View("Index");
        }        
    }
}