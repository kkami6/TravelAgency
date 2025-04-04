using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Excursion
    {
        [Key]
        public int ExcursionId { get; set; }

        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }

        [Required]
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Duration { get; set; }

        public List<Client> Clients { get; set; }

        public List<Employee> Employees { get; set; }

        private Excursion() { }

        public Excursion(int cityId, int hotelId, decimal price, int duration, List<Client> clients, List<Employee> employees)
        {
            CityId = cityId;
            HotelId = hotelId;
            Price = price;
            Duration = duration;
        }
    }
}
