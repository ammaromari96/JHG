using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using JHGF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JHGF.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly SmtpSettings _smtpSettings;

        public HomeController(MyDbContext context, IOptions<SmtpSettings> smtpSettings)
        {
            _context = context;
            _smtpSettings = smtpSettings.Value;
        }

        public IActionResult Index()
        {
            // Fetch the latest 6 tours (already in your logic)
            var topTours = _context.Tours
                .OrderByDescending(t => t.Id)
                .Take(6)
                .ToList();

            // Fetch latest 6 approved testimonials
            var testimonials = _context.Testimonials
                .Where(t => t.Status == "Approved")
                .OrderByDescending(t => t.Id)
                .Take(6)
                .ToList();

            ViewBag.Tours = topTours;
            ViewBag.Testimonials = testimonials;

            return View();
        }



        public IActionResult aboutus()
        {
            return View();
        }

        // GET: Contact Us Page
        public IActionResult contactus()
        {
            return View();
        }

        // POST: Handle Contact Us Form Submission
        [HttpPost]
        public IActionResult contactus(ContactU model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please check the form fields.";
                return View(model);
            }

            // Save to database
            _context.ContactUs.Add(model);
            _context.SaveChanges();

            // Prepare email
            var mail = new MailMessage
            {
                From = new MailAddress(_smtpSettings.UserName),
                Subject = $"New Contact Message: {model.Subject}",
                Body = $"Full Name: {model.FullName}\nEmail: {model.Email}\nPhone: {model.PhoneNumber}\nSubject: {model.Subject}\nMessage:\n{model.Message}",
                IsBodyHtml = false
            };
            mail.To.Add("jordangemssuppurt@gmail.com");

            var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl
            };

            smtpClient.Send(mail);

            TempData["Success"] = "Your message has been sent successfully!";
            return RedirectToAction("contactus");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpGet]
        public IActionResult AddTestimonial()
        {
            return View();
        }

        // POST: Home/AddTestimonial
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTestimonial(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                testimonial.Status = "Pending"; // or "Approved" if auto-approval
                _context.Testimonials.Add(testimonial);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Thank you for your testimonial!";
                return RedirectToAction("AddTestimonial");
            }

            return View(testimonial); // return the same view with validation errors
        }




    }
}
