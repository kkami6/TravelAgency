using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string FamilyName { get; set; }

        [Required]
        [Range(18, int.MaxValue, ErrorMessage = "Client must be 18 or older!")]
        public int Age { get; set; }

        [Required]
        public string Status { get; set; }

        public List<Excursion> Excursions { get; set; }

        private Client() { }

        public Client(string name, string secondName, string familyName, int age, string status)
        {
            Name = name;
            SecondName = secondName;
            FamilyName = familyName;
            Age = age;
            Status = status;
            Excursions = new List<Excursion>();
        }
    }
}
