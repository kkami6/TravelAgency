using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        public List<Hotel> Hotels { get; set; }

        public List<Excursion> Excursions { get; set; }

        private City() { }

        public City(string name, string country)
        {
            Name = name;
            Country = country;
            Hotels = new List<Hotel>();
            Excursions = new List<Excursion>();
        }
    }
}
