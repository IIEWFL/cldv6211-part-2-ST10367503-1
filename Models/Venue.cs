using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VenueBookingSystem.Models
{
    public class Venue
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int Capacity { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
