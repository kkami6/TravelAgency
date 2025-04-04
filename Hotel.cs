using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }

        [Required]
        public bool SPA { get; set; }

        [Required]
        public bool Parking { get; set; }

        public List<Excursion> Excursions { get; set; }

        private Hotel() { }

        public Hotel(int cityId, string address, string name, int rating, bool spa, bool parking)
        {
            CityId = cityId;
            Address = address;
            Name = name;
            Rating = rating;
            SPA = spa;
            Parking = parking;
            Excursions = new List<Excursion>();
        }
    }
}
