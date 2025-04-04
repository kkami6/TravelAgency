using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string FamilyName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public List<Excursion> Excursions { get; set; }

        private Employee() { }

        public Employee(string name, string secondName, string familyName, int age, string position, decimal salary)
        {
            Name = name;
            SecondName = secondName;
            FamilyName = familyName;
            Age = age;
            Position = position;
            Salary = salary;
            Excursions = new List<Excursion>();
        }
    }
}
