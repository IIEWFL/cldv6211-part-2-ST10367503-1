using System;
using System.ComponentModel.DataAnnotations;

namespace VenueBookingSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Start date and time is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date and time is required")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        // ✅ Removed Location – access it via Venue.Location instead

        [Required(ErrorMessage = "Venue selection is required")]
        public int VenueId { get; set; }
        public Venue Venue { get; set; }

        [Required(ErrorMessage = "Event selection is required")]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
