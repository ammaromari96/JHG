using JHGF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHGF.Controllers
{
    public class BookingController : Controller
    {
        private readonly MyDbContext _context;

        public BookingController(MyDbContext context)
        {
            _context = context;
        }

        // ========================== HOTELS ==========================

        public IActionResult Hotel()
        {
            // Get all hotels from the database
            var hotels = _context.Hotels
                .Where(h => h.HotelName != null && h.HotelImage1 != null) // Optional filters
                .OrderByDescending(h => h.Rating) // Sort by rating if desired
                .ToList();

            return View(hotels);
        }


        // GET: HotelDetails
        public IActionResult HotelDetails(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null) return NotFound();

            var reviews = _context.Reviews
                .Where(r => r.HotelId == id)
                .OrderByDescending(r => r.Id)
                .ToList();

            ViewBag.Reviews = reviews;
            return View(hotel);
        }



        // POST: Submit Review
        [HttpPost]
        public IActionResult SubmitHotelReview(int HotelId, int Rating, string Comment)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["Error"] = "Please log in to submit a review.";
                return RedirectToAction("HotelDetails", new { id = HotelId });
            }

            int userId = int.Parse(userIdStr);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("HotelDetails", new { id = HotelId });
            }

            var review = new Review
            {
                UserId = user.Id,
                UserName = user.Name,
                HotelId = HotelId,
                Rating = Rating,
                Comment = Comment
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            TempData["Success"] = "Thanks for your review!";
            return RedirectToAction("HotelDetails", new { id = HotelId });
        }
        [HttpGet]
        public IActionResult HotelPayment(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel); // This will open the payment form with hotel details
        }


        [HttpPost]
        public IActionResult HotelPayment(int HotelId, DateTime CheckIn, DateTime CheckOut, int Adults, int Children)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["Error"] = "Please log in before booking.";
                return RedirectToAction("Login", "User");
            }

            int userId = int.Parse(userIdStr);
            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == HotelId);

            if (hotel == null)
            {
                TempData["Error"] = "Hotel not found.";
                return RedirectToAction("Hotels", "Booking");
            }

            // Booking
            var booking = new Booking
            {
                UserId = userId,
                HotelId = HotelId,
                BookingDate = DateTime.Now,
                CheckIn = DateOnly.FromDateTime(CheckIn),
                CheckOut = DateOnly.FromDateTime(CheckOut),
                Adults = Adults,
                Children = Children
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            // Calculate number of nights
            int totalNights = (CheckOut - CheckIn).Days;
            if (totalNights <= 0) totalNights = 1;

            int totalGuests = Adults + Children;
            decimal pricePerNight = hotel.Price ?? 0;
            decimal totalAmount = pricePerNight * totalNights * totalGuests;

            // Payment
            var payment = new Payment
            {
                UserId = userId,
                BookingId = booking.Id,
                PaymentMethod = "Card",
                PaidAmount = totalAmount,
                PaidAt = DateTime.Now
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            TempData["Success"] = "Booking and payment completed successfully!";
            return RedirectToAction("BookingHistory", "User");
        }



        // ========================== PACKAGES ==========================

        public IActionResult Packages()
        {
            var packages = _context.Packages.ToList();
            return View(packages);
        }

        public IActionResult PackagesDetails(int id)
        {
            var package = _context.Packages.FirstOrDefault(p => p.Id == id);
            if (package == null) return NotFound();

            var reviews = _context.Reviews
                .Where(r => r.PackageId == id)
                .OrderByDescending(r => r.Id)
                .ToList();

            ViewBag.Reviews = reviews;
            return View(package);
        }


        [HttpPost]
        public IActionResult SubmitReviewPackage(int packageId, int rating, string comment)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["Error"] = "You must be logged in to submit a review.";
                return RedirectToAction("PackagesDetails", new { id = packageId });
            }

            if (rating < 1 || string.IsNullOrWhiteSpace(comment))
            {
                TempData["Error"] = "Please fill in all fields.";
                return RedirectToAction("PackagesDetails", new { id = packageId });
            }

            int userId = int.Parse(userIdStr);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("PackagesDetails", new { id = packageId });
            }

            var review = new Review
            {
                UserId = user.Id,
                UserName = user.Name,
                PackageId = packageId,
                Rating = rating,
                Comment = comment
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            TempData["Success"] = "Thank you for your review!";
            return RedirectToAction("PackagesDetails", new { id = packageId });
        }



        [HttpPost]
        public IActionResult BookPackage(int PackageId, DateTime StartDate, int NumberOfAdults, int NumberOfChildren)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["Error"] = "Please login before booking.";
                return RedirectToAction("Login", "User");
            }

            int userId = int.Parse(userIdStr);
            var package = _context.Packages.FirstOrDefault(p => p.Id == PackageId);
            if (package == null)
            {
                TempData["Error"] = "Package not found.";
                return RedirectToAction("Packages");
            }

            var booking = new Booking
            {
                UserId = userId,
                PackageId = PackageId,
                BookingDate = DateTime.Now,
                CheckIn = DateOnly.FromDateTime(StartDate),
                CheckOut = DateOnly.FromDateTime(StartDate),
                Adults = NumberOfAdults,
                Children = NumberOfChildren
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            var payment = new Payment
            {
                UserId = userId,
                BookingId = booking.Id,
                PaymentMethod = "Card",
                PaidAmount = (package.Price ?? 0) * (NumberOfAdults + NumberOfChildren),
                PaidAt = DateTime.Now
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            TempData["Success"] = "Package booking and payment completed successfully!";
            return RedirectToAction("PackagesDetails", new { id = PackageId });
        }



        // ========================== TOURS ==========================

        public IActionResult Tours()
        {
            var tours = _context.Tours.ToList();
            return View(tours);
        }

        public IActionResult ToursDetails(int id)
        {
            var tour = _context.Tours.FirstOrDefault(t => t.Id == id);
            if (tour == null) return NotFound();

            var reviews = _context.Reviews
                .Where(r => r.TourId == id)
                .OrderByDescending(r => r.Id)
                .ToList();

            ViewBag.Reviews = reviews;
            return View(tour);
        }

        // ========================== SUBMIT TOUR REVIEW ==========================
        [HttpPost]
        public IActionResult SubmitTourReview(int TourId, int Rating, string Comment)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["Error"] = "You must be logged in to submit a review.";
                return RedirectToAction("ToursDetails", new { id = TourId });
            }

            int userId = int.Parse(userIdStr);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("ToursDetails", new { id = TourId });
            }

            var review = new Review
            {
                UserId = user.Id,
                UserName = user.Name, // Use the logged-in user's name
                TourId = TourId,
                Rating = Rating,
                Comment = Comment
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            TempData["Success"] = "Thank you for your review!";
            return RedirectToAction("ToursDetails", new { id = TourId });
        }

        // ========================== TOUR BOOKING ==========================

        [HttpPost]
        public IActionResult BookTour(int TourId, DateTime TourDate, int NumberOfAdults, int NumberOfChildren)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Please login before booking.";
                return RedirectToAction("Login", "User");
            }

            var tour = _context.Tours.FirstOrDefault(t => t.Id == TourId);
            if (tour == null)
            {
                TempData["Error"] = "Tour not found.";
                return RedirectToAction("Tours");
            }

            var booking = new Booking
            {
                UserId = int.Parse(userId),
                TourId = TourId,
                BookingDate = DateTime.Now,
                CheckIn = DateOnly.FromDateTime(TourDate),
                CheckOut = DateOnly.FromDateTime(TourDate),
                Adults = NumberOfAdults,
                Children = NumberOfChildren
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            var payment = new Payment
            {
                UserId = int.Parse(userId),
                BookingId = booking.Id,
                PaymentMethod = "Card",
                PaidAmount = tour.Price * (NumberOfAdults + NumberOfChildren),
                PaidAt = DateTime.Now
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            TempData["Success"] = "Booking completed and payment successful!";
            return RedirectToAction("ToursDetails", new { id = TourId });
        }


        // ========================== ROOM BOOKING PAGE ==========================

        




        public IActionResult BookingHistory()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["Error"] = "Please log in to view your booking history.";
                return RedirectToAction("Login", "User");
            }

            int userId = int.Parse(userIdStr);

            var bookings = _context.Bookings
                .Include(b => b.Tour)
                .Include(b => b.Package)
                .Include(b => b.Payments)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookingDate)
                .ToList();

            ViewBag.Count = bookings.Count;

            return View(bookings);
        }


        public IActionResult Hotels(string? search, decimal? maxPrice, bool wifi = false, bool parking = false, bool reset = false)
        {
            var hotels = _context.Hotels.AsQueryable();

            if (!reset)
            {
                if (!string.IsNullOrWhiteSpace(search))
                {
                    hotels = hotels.Where(h => h.HotelName.Contains(search));
                }

                if (maxPrice.HasValue)
                {
                    hotels = hotels.Where(h => h.Price <= maxPrice);
                }

                if (wifi)
                {
                    hotels = hotels.Where(h => h.FreeWifi == true);
                }

                if (parking)
                {
                    hotels = hotels.Where(h => h.ParkingAvailable == true);
                }
            }

            // Pass back filter values to keep them in form
            ViewBag.Search = search;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.WifiChecked = wifi;
            ViewBag.ParkingChecked = parking;
            return View("Hotel", hotels.ToList());

            
        }


        public IActionResult FilterTours(string? search, decimal? maxPrice, List<int> durations, bool reset = false)
        {
            var tours = _context.Tours.AsQueryable();

            if (!reset)
            {
                if (!string.IsNullOrWhiteSpace(search))
                    tours = tours.Where(t => t.TourName.Contains(search));

                if (maxPrice.HasValue)
                    tours = tours.Where(t => t.Price <= maxPrice);

                if (durations.Any())
                {
                    tours = tours.Where(t => t.DurationDays.HasValue && (
                        (durations.Contains(1) && t.DurationDays == 1) ||
                        (durations.Contains(2) && t.DurationDays == 2) ||
                        (durations.Contains(3) && t.DurationDays >= 3 && t.DurationDays <= 5) ||
                        (durations.Contains(7) && t.DurationDays >= 7)
                    ));
                }
            }

            ViewBag.Search = search;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Durations = durations;

            return View("Tours", tours.ToList());
        }



        public IActionResult PackageFilter(string? search, decimal? maxPrice, List<int> durations, bool reset = false)
        {
            var packages = _context.Packages.AsQueryable();

            if (!reset)
            {
                if (!string.IsNullOrWhiteSpace(search))
                    packages = packages.Where(p => p.PackageName.Contains(search));

                if (maxPrice.HasValue)
                    packages = packages.Where(p => p.Price <= maxPrice);

                if (durations.Any())
                {
                    packages = packages.Where(p => p.DurationDays.HasValue && durations.Contains(p.DurationDays.Value));
                }
            }

            ViewBag.Search = search;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Durations = durations;

            return View("Packages", packages.ToList());
        }



    }
}
