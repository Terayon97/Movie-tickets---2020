using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjektAplikacjeInternetowe.Data;
using ProjektAplikacjeInternetowe.Models;
using ProjektAplikacjeInternetowe.Models.ViewModels;

namespace ProjektAplikacjeInternetowe.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;
        public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var item = _context.Cart.Where(a => a.UserId == _userManager.GetUserId(HttpContext.User)).ToList();

            return View(item);
        }
        public IActionResult UserBooked()
        {
            var item = _context.BookingTable.Where(a => a.UserId == _userManager.GetUserId(HttpContext.User)).ToList();

            return View(item);
        }

        public IActionResult cartEmpty()
        {
            TempData["cartempty"] = "Your cart is empty";
            return View();
        }
        [HttpGet]
        public IActionResult proceed(Cart Cart)
        {
            var CartList = _context.Cart.Where(a => a.UserId == _userManager.GetUserId(HttpContext.User)).ToList();
            if (CartList.Count == 0)
            {
                return RedirectToAction("cartempty","Cart");
            }
            else
            {
                return View(CartList);
            }
        }

        [HttpPost]
        public IActionResult BookTicket(Cart Cart, BookingTable TicketProceed)
        {
            var CartList = _context.Cart.Where(a => a.UserId == _userManager.GetUserId(HttpContext.User)).ToList();
            var Tickets = new List<BookingTable>();
            foreach (var item in CartList)
            {
                Tickets.Add(new BookingTable {  
                    seatNo = item.seatNo,
                    MovieDetailsId = item.MovieId,
                    Amount = item.Amount,
                    DateToPresent = item.date,
                    MovieName = item.movieName,
                    UserId = _userManager.GetUserId(HttpContext.User) });
                    _context.Cart.Remove(item);
            }
            _context.BookingTable.AddRange(Tickets);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
        [HttpGet]
        public IActionResult Delete(Cart cart)
        {
            _context.Cart.Remove(cart);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }

    }
}
