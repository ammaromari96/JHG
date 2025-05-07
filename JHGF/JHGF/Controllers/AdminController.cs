using JHGF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHGF.Controllers
{


    public class AdminController : Controller
    {

        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AdminController(MyDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }



        public IActionResult Dashboard()
        {
            return View();
        }

        // Hotels
        public IActionResult ViewHotels()
        {
            var hotels = _context.Hotels.ToList();
            return View(hotels);
        }





        [HttpPost]
        public IActionResult DeleteHotel(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                TempData["Error"] = "Hotel not found.";
                return RedirectToAction("ViewHotels");
            }

            _context.Hotels.Remove(hotel);
            _context.SaveChanges();

            TempData["Success"] = "Hotel deleted successfully!";
            return RedirectToAction("ViewHotels");
        }

        // GET: Admin/AddHotel
        [HttpGet]
        public IActionResult AddHotel()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddHotel(IFormCollection form)
        {
            var hotel = new Hotel
            {
                HotelName = form["HotelName"],
                Location = form["Location"],
                DeluxeRoomPrice = Convert.ToDecimal(form["Price"]),
                Rating = Convert.ToInt32(form["Rating"]),
                DistanceFromCity = string.IsNullOrEmpty(form["DistanceFromCity"]) ? null : Convert.ToDecimal(form["DistanceFromCity"]),
                CheckInTime = TimeOnly.Parse(form["CheckInTime"]),
                CheckOutTime = TimeOnly.Parse(form["CheckOutTime"]),
                SmallDescription = form["SmallDescription"],
                Price = Convert.ToDecimal(form["Price"]),
                FullDescription = form["FullDescription"],
                FreeWifi = form["FreeWifi"].Count > 0,
                ParkingAvailable = form["ParkingAvailable"].Count > 0
            };

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "hotels");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            for (int i = 1; i <= 4; i++)
            {
                var file = form.Files[$"HotelImage{i}"];
                if (file != null && file.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    typeof(Hotel).GetProperty($"HotelImage{i}")?.SetValue(hotel, $"/uploads/hotels/{fileName}");
                }
            }

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Hotel added successfully!";
            return RedirectToAction("ViewHotels");
        }


        // GET: EditHotel
        public IActionResult EditHotel(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        // POST: EditHotel
        [HttpPost]
        public async Task<IActionResult> EditHotel(int id, Hotel model, IFormFile? HotelImage1, IFormFile? HotelImage2, IFormFile? HotelImage3, IFormFile? HotelImage4)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null) return NotFound();

            // Update text fields
            hotel.HotelName = model.HotelName;
            hotel.Location = model.Location;
            hotel.DeluxeRoomPrice = model.DeluxeRoomPrice;
            hotel.Rating = model.Rating;
            hotel.DistanceFromCity = model.DistanceFromCity;
            hotel.CheckInTime = model.CheckInTime;
            hotel.CheckOutTime = model.CheckOutTime;
            hotel.Price = model.Price;
            hotel.SmallDescription = model.SmallDescription;
            hotel.FullDescription = model.FullDescription;
            hotel.FreeWifi = model.FreeWifi;
            hotel.ParkingAvailable = model.ParkingAvailable;

            string uploadsPath = Path.Combine(_env.WebRootPath, "uploads", "hotels");
            Directory.CreateDirectory(uploadsPath);

            // Helper to delete old file
            void DeleteOldFile(string? oldPath)
            {
                if (!string.IsNullOrEmpty(oldPath))
                {
                    var fullPath = Path.Combine(_env.WebRootPath, oldPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
                }
            }

            // Upload & replace logic
            async Task<string> UploadFile(IFormFile file)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string fullPath = Path.Combine(uploadsPath, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream);
                return "/uploads/hotels/" + fileName;
            }

            if (HotelImage1 != null)
            {
                DeleteOldFile(hotel.HotelImage1);
                hotel.HotelImage1 = await UploadFile(HotelImage1);
            }

            if (HotelImage2 != null)
            {
                DeleteOldFile(hotel.HotelImage2);
                hotel.HotelImage2 = await UploadFile(HotelImage2);
            }

            if (HotelImage3 != null)
            {
                DeleteOldFile(hotel.HotelImage3);
                hotel.HotelImage3 = await UploadFile(HotelImage3);
            }

            if (HotelImage4 != null)
            {
                DeleteOldFile(hotel.HotelImage4);
                hotel.HotelImage4 = await UploadFile(HotelImage4);
            }

            // Save to DB
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Hotel updated successfully!";
            return RedirectToAction("ViewHotels");
        }








        /// //////////////////////////////////////////////////////////////////////////////////////////////


        // Tours
        // In AdminController
        public IActionResult ViewTours()
        {
            var tours = _context.Tours.ToList();
            return View(tours);
        }



        [HttpPost]
        public IActionResult DeleteTour(int id)
        {
            var tour = _context.Tours.FirstOrDefault(t => t.Id == id);
            if (tour == null)
            {
                TempData["Error"] = "Tour not found.";
                return RedirectToAction("ViewTours");
            }

            _context.Tours.Remove(tour);
            _context.SaveChanges();

            TempData["Success"] = "Tour deleted successfully!";
            return RedirectToAction("ViewTours");
        }


        public IActionResult AddTour()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> AddTour(IFormCollection form)
        {
            var tour = new Tour
            {
                TourName = form["TourName"],
                Location = form["Location"],
                DurationDays = int.TryParse(form["DurationDays"], out int days) ? days : 0,
                Price = decimal.TryParse(form["Price"], out decimal parsedPrice) ? parsedPrice : 0,
                SmallDescription = form["SmallDescription"],
                FullDescription = form["FullDescription"],

                ItineraryTime1 = form["ItineraryTime1"],
                ItineraryTitle1 = form["ItineraryTitle1"],
                ItineraryDesc1 = form["ItineraryDesc1"],
                ItineraryTime2 = form["ItineraryTime2"],
                ItineraryTitle2 = form["ItineraryTitle2"],
                ItineraryDesc2 = form["ItineraryDesc2"],
                ItineraryTime3 = form["ItineraryTime3"],
                ItineraryTitle3 = form["ItineraryTitle3"],
                ItineraryDesc3 = form["ItineraryDesc3"],
                ItineraryTime4 = form["ItineraryTime4"],
                ItineraryTitle4 = form["ItineraryTitle4"],
                ItineraryDesc4 = form["ItineraryDesc4"],
                ItineraryTime5 = form["ItineraryTime5"],
                ItineraryTitle5 = form["ItineraryTitle5"],
                ItineraryDesc5 = form["ItineraryDesc5"],
                ItineraryTime6 = form["ItineraryTime6"],
                ItineraryTitle6 = form["ItineraryTitle6"],
                ItineraryDesc6 = form["ItineraryDesc6"],
                ItineraryTime7 = form["ItineraryTime7"],
                ItineraryTitle7 = form["ItineraryTitle7"],
                ItineraryDesc7 = form["ItineraryDesc7"],
                ItineraryTime8 = form["ItineraryTime8"],
                ItineraryTitle8 = form["ItineraryTitle8"],
                ItineraryDesc8 = form["ItineraryDesc8"]
            };

            // Upload images
            for (int i = 1; i <= 4; i++)
            {
                var file = form.Files[$"Image{i}"];
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                    var filePath = Path.Combine("wwwroot/images/tours", uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    typeof(Tour).GetProperty($"Image{i}")?.SetValue(tour, $"/images/tours/{uniqueFileName}");
                }
            }

            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Tour added successfully!";
            return RedirectToAction("ViewTours");
        }








        [HttpGet]
        public IActionResult EditTour(int id)
        {
            var tour = _context.Tours.FirstOrDefault(t => t.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }




        [HttpPost]
        public async Task<IActionResult> EditTour(int id, IFormCollection form, IFormFile? Image1, IFormFile? Image2, IFormFile? Image3, IFormFile? Image4)
        {
            var tour = _context.Tours.FirstOrDefault(t => t.Id == id);
            if (tour == null)
                return NotFound();

            // Update basic fields
            tour.TourName = form["TourName"];
            tour.Location = form["Location"];
            tour.DurationDays = int.TryParse(form["DurationDays"], out int days) ? days : 0;
            tour.Price = decimal.TryParse(form["Price"], out decimal price) ? price : 0;
            tour.SmallDescription = form["SmallDescription"];
            tour.FullDescription = form["FullDescription"];

            // Itinerary
            tour.ItineraryTime1 = form["ItineraryTime1"];
            tour.ItineraryTitle1 = form["ItineraryTitle1"];
            tour.ItineraryDesc1 = form["ItineraryDesc1"];
            tour.ItineraryTime2 = form["ItineraryTime2"];
            tour.ItineraryTitle2 = form["ItineraryTitle2"];
            tour.ItineraryDesc2 = form["ItineraryDesc2"];
            tour.ItineraryTime3 = form["ItineraryTime3"];
            tour.ItineraryTitle3 = form["ItineraryTitle3"];
            tour.ItineraryDesc3 = form["ItineraryDesc3"];
            tour.ItineraryTime4 = form["ItineraryTime4"];
            tour.ItineraryTitle4 = form["ItineraryTitle4"];
            tour.ItineraryDesc4 = form["ItineraryDesc4"];
            tour.ItineraryTime5 = form["ItineraryTime5"];
            tour.ItineraryTitle5 = form["ItineraryTitle5"];
            tour.ItineraryDesc5 = form["ItineraryDesc5"];
            tour.ItineraryTime6 = form["ItineraryTime6"];
            tour.ItineraryTitle6 = form["ItineraryTitle6"];
            tour.ItineraryDesc6 = form["ItineraryDesc6"];
            tour.ItineraryTime7 = form["ItineraryTime7"];
            tour.ItineraryTitle7 = form["ItineraryTitle7"];
            tour.ItineraryDesc7 = form["ItineraryDesc7"];
            tour.ItineraryTime8 = form["ItineraryTime8"];
            tour.ItineraryTitle8 = form["ItineraryTitle8"];
            tour.ItineraryDesc8 = form["ItineraryDesc8"];

            var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "tours");
            Directory.CreateDirectory(imageFolder); // Ensure directory exists

            async Task HandleImageUpload(IFormFile? file, string propName)
            {
                if (file != null && file.Length > 0)
                {
                    // Delete old image
                    var current = typeof(Tour).GetProperty(propName)?.GetValue(tour)?.ToString();
                    if (!string.IsNullOrEmpty(current))
                    {
                        var fullPath = Path.Combine("wwwroot", current.TrimStart('/'));
                        if (System.IO.File.Exists(fullPath))
                            System.IO.File.Delete(fullPath);
                    }

                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                    var path = Path.Combine(imageFolder, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    typeof(Tour).GetProperty(propName)?.SetValue(tour, $"/images/tours/{fileName}");
                }
            }

            await HandleImageUpload(Image1, "Image1");
            await HandleImageUpload(Image2, "Image2");
            await HandleImageUpload(Image3, "Image3");
            await HandleImageUpload(Image4, "Image4");

            _context.Tours.Update(tour);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Tour updated successfully!";
            return RedirectToAction("ViewTours");
        }







        // Packages
        public IActionResult ViewPackages()
        {
            var packages = _context.Packages.ToList();
            return View(packages);
        }

        [HttpPost]
        public IActionResult DeletePackage(int id)
        {
            var package = _context.Packages.Find(id);
            if (package == null)
            {
                TempData["Error"] = "Package not found.";
                return RedirectToAction("ViewPackages");
            }

            _context.Packages.Remove(package);
            _context.SaveChanges();

            TempData["Success"] = "Package deleted successfully!";
            return RedirectToAction("ViewPackages");
        }



        // GET: Admin/AddPackage
        [HttpGet]
        public IActionResult AddPackage()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> AddPackage(IFormCollection form)
        {
            var package = new Package
            {
                PackageName = form["PackageName"],
                Location = form["Location"],
                DurationDays = int.TryParse(form["DurationDays"], out var duration) ? duration : 0,
                Price = decimal.TryParse(form["Price"], out var price) ? price : 0,
                SmallDescription = form["SmallDescription"],
                FullDescription = form["FullDescription"],

                ItineraryTime1 = form["ItineraryTime1"],
                ItineraryTitle1 = form["ItineraryTitle1"],
                ItineraryDesc1 = form["ItineraryDesc1"],
                ItineraryTime2 = form["ItineraryTime2"],
                ItineraryTitle2 = form["ItineraryTitle2"],
                ItineraryDesc2 = form["ItineraryDesc2"],
                ItineraryTime3 = form["ItineraryTime3"],
                ItineraryTitle3 = form["ItineraryTitle3"],
                ItineraryDesc3 = form["ItineraryDesc3"],
                ItineraryTime4 = form["ItineraryTime4"],
                ItineraryTitle4 = form["ItineraryTitle4"],
                ItineraryDesc4 = form["ItineraryDesc4"],
                ItineraryTime5 = form["ItineraryTime5"],
                ItineraryTitle5 = form["ItineraryTitle5"],
                ItineraryDesc5 = form["ItineraryDesc5"],
                ItineraryTime6 = form["ItineraryTime6"],
                ItineraryTitle6 = form["ItineraryTitle6"],
                ItineraryDesc6 = form["ItineraryDesc6"],
                ItineraryTime7 = form["ItineraryTime7"],
                ItineraryTitle7 = form["ItineraryTitle7"],
                ItineraryDesc7 = form["ItineraryDesc7"],
                ItineraryTime8 = form["ItineraryTime8"],
                ItineraryTitle8 = form["ItineraryTitle8"],
                ItineraryDesc8 = form["ItineraryDesc8"]
            };

            // Upload images to wwwroot/images/packages
            var imageFolderPath = Path.Combine("wwwroot", "images", "packages");
            Directory.CreateDirectory(imageFolderPath);

            for (int i = 1; i <= 4; i++)
            {
                var file = form.Files[$"Image{i}"];
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                    var savePath = Path.Combine(imageFolderPath, uniqueFileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    typeof(Package).GetProperty($"Image{i}")?.SetValue(package, $"/images/packages/{uniqueFileName}");
                }
            }

            _context.Packages.Add(package);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Package added successfully!";
            return RedirectToAction("ViewPackages");
        }



        // AdminController.cs

        [HttpGet]
        public IActionResult EditPackage(int id)
        {
            var package = _context.Packages.FirstOrDefault(p => p.Id == id);
            if (package == null)
            {
                return NotFound();
            }
            return View(package);
        }

        [HttpPost]
        public async Task<IActionResult> EditPackage(int id, IFormCollection form, IFormFile? Image1, IFormFile? Image2, IFormFile? Image3, IFormFile? Image4)
        {
            var package = _context.Packages.FirstOrDefault(p => p.Id == id);
            if (package == null)
                return NotFound();

            // Update basic info
            package.PackageName = form["PackageName"];
            package.Location = form["Location"];
            package.DurationDays = int.TryParse(form["DurationDays"], out int duration) ? duration : 0;
            package.Price = decimal.TryParse(form["Price"], out decimal price) ? price : 0;
            package.SmallDescription = form["SmallDescription"];
            package.FullDescription = form["FullDescription"];

            // Update itinerary (fixed .ToString() issue)
            for (int i = 1; i <= 8; i++)
            {
                typeof(Package).GetProperty($"ItineraryTime{i}")?.SetValue(package, form[$"ItineraryTime{i}"].ToString());
                typeof(Package).GetProperty($"ItineraryTitle{i}")?.SetValue(package, form[$"ItineraryTitle{i}"].ToString());
                typeof(Package).GetProperty($"ItineraryDesc{i}")?.SetValue(package, form[$"ItineraryDesc{i}"].ToString());
            }

            // Handle images
            var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "packages");
            Directory.CreateDirectory(imageFolder);

            async Task HandleImageUpload(IFormFile? file, string propName)
            {
                if (file != null && file.Length > 0)
                {
                    // Delete old image
                    var current = typeof(Package).GetProperty(propName)?.GetValue(package)?.ToString();
                    if (!string.IsNullOrEmpty(current))
                    {
                        var fullPath = Path.Combine("wwwroot", current.TrimStart('/'));
                        if (System.IO.File.Exists(fullPath))
                            System.IO.File.Delete(fullPath);
                    }

                    // Save new image
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                    var path = Path.Combine(imageFolder, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    typeof(Package).GetProperty(propName)?.SetValue(package, $"/images/packages/{fileName}");
                }
            }

            await HandleImageUpload(Image1, "Image1");
            await HandleImageUpload(Image2, "Image2");
            await HandleImageUpload(Image3, "Image3");
            await HandleImageUpload(Image4, "Image4");

            // Save changes
            _context.Packages.Update(package);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Package updated successfully!";
            return RedirectToAction("ViewPackages");
        }


















        // Users
        // GET: /Admin/ViewUsers
        public IActionResult ViewUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // POST: /Admin/DeleteUser/5
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            TempData["Success"] = "User deleted successfully!";
            return RedirectToAction("ViewUsers");
        }
        // Contact Messages


        public IActionResult ContactMessages()
        {
            var messages = _context.ContactUs.OrderByDescending(m => m.Id).ToList();
            return View(messages);
        }

        [HttpPost]
        public IActionResult DeleteMessage(int id)
        {
            var message = _context.ContactUs.Find(id);
            if (message != null)
            {
                _context.ContactUs.Remove(message);
                _context.SaveChanges();
                TempData["Success"] = "Message deleted successfully!";
            }
            return RedirectToAction("ContactMessages");
        }


        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();


            TempData["Success"] = "You have been logged out successfully.";
            return RedirectToAction("Login", "User");
        }

        public IActionResult Testimonial()
        {
            var testimonials = _context.Testimonials.OrderByDescending(t => t.Id).ToList();
            return View(testimonials);
        }


        [HttpPost]
        public IActionResult ApproveTestimonial(int id)
        {
            var testimonial = _context.Testimonials.Find(id);
            if (testimonial != null)
            {
                testimonial.Status = "Approved";
                _context.SaveChanges();
            }
            return RedirectToAction("Testimonial");
        }

        [HttpPost]
        public IActionResult RejectTestimonial(int id)
        {
            var testimonial = _context.Testimonials.Find(id);
            if (testimonial != null)
            {
                testimonial.Status = "Rejected";
                _context.SaveChanges();
            }
            return RedirectToAction("Testimonial");
        }

        [HttpPost]
        public IActionResult DeleteTestimonial(int id)
        {
            var testimonial = _context.Testimonials.Find(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
                _context.SaveChanges();
            }
            return RedirectToAction("Testimonial");
        }



    }
}

