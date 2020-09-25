using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacjeInternetowe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjektAplikacjeInternetowe.Data;
using ProjektAplikacjeInternetowe.Models;

namespace ProjektAplikacjeInternetowe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //// GET: Admin
        //private ApplicationDbContext _context;
        //private UploadInterface _upload;
        //public AdminController(ApplicationDbContext context, UploadInterface upload)
        //{
        //    _upload = upload;
        //    _context = context;
        //}
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //public IActionResult Movies()
        //{
        //    var getMovieTable = _context.MovieDetails.ToList();
        //        return View(getMovieTable);
        //}
        //public IActionResult MoviesEdit()
        //{

        //    return View();
        //}
        //public IActionResult MoviesDelete(MovieDetails md)
        //{
        //    _context.MovieDetails.Remove(md);
        //    _context.SaveChanges();
        //    return RedirectToAction("Movies", "Admin");
        //}
        //public IActionResult Create(IList<IFormFile> files, MovieDetails vmodel, MovieDetails Movie)
        //{
        //    Movie.Movie_Name = vmodel.Movie_Name;
        //    Movie.Movie_Description = vmodel.Movie_Description;
        //    Movie.DateToPresent = vmodel.DateToPresent;
        //    foreach (var item in files)
        //    {
        //        Movie.MoviePicture = "/uploads/" + item.FileName.Trim();
        //    }
        //    _upload.uploadfilemultiple(files);
        //    _context.MovieDetails.Add(Movie);
        //    _context.SaveChanges();
        //    TempData["Success"] = "Save Your Movie";
        //    return RedirectToAction("Create","Admin");
        //}
        //[HttpGet]
        //public IActionResult CheckBookSeat()
        //{
        //    var getBookingTable = _context.BookingTable.ToList().OrderByDescending(a => a.DateToPresent);
        //    return View(getBookingTable);
        //}
        //[HttpGet]
        //public IActionResult GetUserDetails()
        //{
        //    var getUserTable = _context.Users.ToList();
        //    return View(getUserTable);
        //}
    }
}
