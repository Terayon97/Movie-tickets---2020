using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektAplikacjeInternetowe.Models
{
    public class BookingTable
    {
        public int Id { get; set; }
        public string seatNo { get; set; }
        public string UserId { get; set; }
        public DateTime DateToPresent { get; set; }
        public int MovieDetailsId { get; set; }
        public string MovieName { get; set; }
        public int Amount { get; set; }
        [ForeignKey("MovieDetailsId")]
        public virtual MovieDetails moviedetails { get; set; }
    }
}