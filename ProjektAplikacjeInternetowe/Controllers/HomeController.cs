using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjektAplikacjeInternetowe.Data;
using ProjektAplikacjeInternetowe.Models;
using ProjektAplikacjeInternetowe.Models.ViewModels;

namespace ProjektAplikacjeInternetowe.Controllers
{
    public class HomeController : Controller
    {
        int count = 1;
        string bookedseat;
        bool flag = true;
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var getMovieList = _context.MovieDetails.ToList();
            return View(getMovieList);
        }
        [Authorize]
        [HttpGet]
        public IActionResult BookTicket(int Id)
        {
            BookNowViewModel vm = new BookNowViewModel();
            var item = _context.MovieDetails.Where(a => a.Id == Id).FirstOrDefault();
            vm.Movie_Name = item.Movie_Name;
            vm.Movie_Date = item.DateToPresent;
            vm.MovieId = Id;
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        public IActionResult BookTicket(BookNowViewModel vm)
        {
            List<Cart> carts = new List<Cart>();
            if (vm.SeatNo == null) { return RedirectToAction("BookTicket"); }
            string seatNo = vm.SeatNo.ToString();
            
            int movieId = vm.MovieId;

            string[] seatNoArray = seatNo.Split(',');
            count = seatNoArray.Length;

            if (checkSeat(seatNo, movieId) == false)
            {
                if (checkCart(seatNo, movieId) == false)
                {
                    foreach (var item in seatNoArray)
                    {
                        carts.Add(new Cart { movieName=vm.Movie_Name, Amount = 25, MovieId = vm.MovieId, UserId = _userManager.GetUserId(HttpContext.User), date = vm.Movie_Date, seatNo = item });
                    }
                    foreach (var item in carts)
                    {
                        _context.Cart.Add(item);
                        _context.SaveChanges();
                    }
                    TempData["Success"] = "Seats added to cart";
                }
                else
                {
                    TempData["Success"] = "You already added a ticket for seat nr. "+bookedseat+" to your cart";
                }
            }
            else
            {
                TempData["Success"] = "The seat "+bookedseat+" is already booked, please change your seat";
            }
            return RedirectToAction("BookTicket");
        }

        private bool checkSeat(string seatNo, int movieId)
        {
            string seats = seatNo;
            string[] seatreserve = seats.Split(',');
            var seatnolist = _context.BookingTable.Where(a => a.MovieDetailsId == movieId).ToList();
            foreach (var item in seatnolist)
            {
                string alreadybook = item.seatNo;
                foreach (var item1 in seatreserve)
                {
                    if (item1==alreadybook)
                    { 
                        flag = false;
                        bookedseat = item1;
                        break;
                    }
                }
            }
            if (flag == false)
                return true;
            else
                return false;
        }

        private bool checkCart(string seatNo, int movieId)
        {
            string seats = seatNo;
            string[] seatreserve = seats.Split(',');
            var seatnolist = _context.Cart.Where(a => a.UserId == _userManager.GetUserId(HttpContext.User)).ToList();
            foreach (var item in seatnolist)
            {
                string alreadybook = item.seatNo;
                foreach (var item1 in seatreserve)
                {
                    if (item1 == alreadybook)
                    {
                        flag = false;
                        bookedseat = item1;
                        break;
                    }
                }
            }
            if (flag == false)
                return true;
            else
                return false;
        }

        [HttpPost]
        public IActionResult checkSeat(DateTime Movie_Date, BookNowViewModel bookTicket)
        {
            ViewBag.Id = bookTicket.MovieId;
            string seatNo = string.Empty;
            var movielist = _context.BookingTable.Where(a => a.DateToPresent == Movie_Date).ToList();
            if (movielist != null)
            {
                var getseatno = movielist.Where(b => b.MovieDetailsId == bookTicket.MovieId).ToList();
                if (getseatno != null)
                {
                    foreach (var item in getseatno)
                    {
                        seatNo = seatNo + " " + item.seatNo.ToString();
                    }
                    TempData["SNO"] = "Booked seats: " + seatNo+", ";
                }
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
