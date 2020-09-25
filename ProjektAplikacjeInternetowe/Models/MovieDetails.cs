using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektAplikacjeInternetowe.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Movie_Name { get; set; }
        public string Movie_Description { get; set; }
        public DateTime DateToPresent { get; set; }
        public string MoviePicture { get; set; }
        public virtual ICollection<BookingTable> booking { get; set; }

    }
}