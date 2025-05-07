using JHGF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHGF.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDbContext _context;

        public UserController(MyDbContext context)
        {
            _context = context;
        }

        // ========================== DASHBOARD ==========================

        public IActionResult Dashboard() => View();

        // ========================== PROFILE ==========================

        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Id == Convert.ToInt32(userId));
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Id == Convert.ToInt32(userId));
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult EditProfile(User model, IFormFile ProfileImage, string OldPassword, string NewPassword, string ConfirmPassword)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Id == Convert.ToInt32(userId));
            if (user == null) return NotFound();

            user.Name = model.Name;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.DateOfBirth = model.DateOfBirth;

            if (!string.IsNullOrWhiteSpace(NewPassword) || !string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                if (OldPassword != user.Password)
                {
                    TempData["Error"] = "Current password is incorrect.";
                    return View(user);
                }

                if (NewPassword != ConfirmPassword)
                {
                    TempData["Error"] = "New password and confirmation do not match.";
                    return View(user);
                }

                user.Password = NewPassword;
            }

            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(wwwRootPath);

                string fileName = Guid.NewGuid() + Path.GetExtension(ProfileImage.FileName);
                string filePath = Path.Combine(wwwRootPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ProfileImage.CopyTo(stream);
                }

                user.ProfilePic = "/uploads/" + fileName;
            }

            _context.SaveChanges();

            HttpContext.Session.SetString("UserName", user.Name ?? "");
            HttpContext.Session.SetString("UserRole", user.Role ?? "");
            HttpContext.Session.SetString("ProfilePic", user.ProfilePic ?? "");

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }

        // ========================== WISHLIST ==========================

        [HttpGet]
        public IActionResult Wishlist(string type = "All")
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["Error"] = "Please log in to access your wishlist.";
                return RedirectToAction("Login");
            }

            int userId = int.Parse(userIdStr);

            var wishlist = _context.Wishlists
                .Include(w => w.Tour)
                .Include(w => w.Package)
                .Include(w => w.Hotel)
                .Where(w => w.UserId == userId)
                .ToList();

            wishlist = type switch
            {
                "Tour" => wishlist.Where(w => w.TourId != null).ToList(),
                "Package" => wishlist.Where(w => w.PackageId != null).ToList(),
                "Hotel" => wishlist.Where(w => w.HotelId != null).ToList(),
                _ => wishlist
            };

            ViewBag.Type = type;
            return View(wishlist);
        }


   

        [HttpPost]
        public IActionResult AddToWishlist(int? TourId, int? PackageId, int? HotelId)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["Error"] = "Please log in to add items to wishlist.";
                return RedirectToAction("Login", "User");
            }

            int userId = int.Parse(userIdStr);
            bool exists = false;

            if (TourId.HasValue)
            {
                exists = _context.Wishlists.Any(w => w.UserId == userId && w.TourId == TourId);
            }
            else if (PackageId.HasValue)
            {
                exists = _context.Wishlists.Any(w => w.UserId == userId && w.PackageId == PackageId);
            }
            else if (HotelId.HasValue)
            {
                exists = _context.Wishlists.Any(w => w.UserId == userId && w.HotelId == HotelId);
            }

            if (!exists)
            {
                var wishlistItem = new Wishlist
                {
                    UserId = userId,
                    TourId = TourId,
                    PackageId = PackageId,
                    HotelId = HotelId
                };

                _context.Wishlists.Add(wishlistItem);
                _context.SaveChanges();

                TempData["Success"] = "Item added to wishlist!";
            }
            else
            {
                TempData["Error"] = "Item already in wishlist.";
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }




        [HttpPost]
        public IActionResult RemoveFromWishlist(int id)
        {
            var item = _context.Wishlists.Find(id);
            if (item != null)
            {
                _context.Wishlists.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Wishlist");
        }

        // ========================== AUTH ==========================

        public IActionResult Login() => View();

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                TempData["RegisterError"] = "Email already exists!";
                return View("Login");
            }

            user.Role = "User";
            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["RegisterSuccess"] = "Registered successfully!";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Name ?? "Guest");
                HttpContext.Session.SetString("UserRole", user.Role ?? "User");
                HttpContext.Session.SetString("ProfilePic", user.ProfilePic ?? "");

                return user.Role == "Admin"
                    ? RedirectToAction("Dashboard", "Admin")
                    : RedirectToAction("Index", "Home");
            }

            TempData["LoginError"] = "Invalid credentials!";
            return View("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ========================== BOOKING HISTORY ==========================

        public IActionResult BookingHistory(string type)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["Error"] = "Please log in to view your booking history.";
                return RedirectToAction("Login");
            }

            int userId = int.Parse(userIdStr);

            var bookings = _context.Bookings
                .Include(b => b.Tour)
                .Include(b => b.Package)
                .Include(b => b.Hotel)
                .Include(b => b.Payments)
                .Where(b => b.UserId == userId)
                .AsQueryable();

            bookings = type switch
            {
                "Tour" => bookings.Where(b => b.TourId != null),
                "Package" => bookings.Where(b => b.PackageId != null),
                "Hotel" => bookings.Where(b => b.HotelId != null),
                _ => bookings
            };

            var result = bookings.OrderByDescending(b => b.BookingDate).ToList();
            ViewBag.Count = result.Count;
            ViewBag.Filter = type;

            return View(result);
        }


        public ActionResult ForgetPassword()
        {

            return View();
        }


        



    }
}
