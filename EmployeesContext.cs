using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class EmployeesContext: IDb<Employee, int>
    {
        private TravelAgencyDbContext dbContext;

        public EmployeesContext(TravelAgencyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Employee item)
        {
            dbContext.Employees.Add(item);
            dbContext.SaveChanges();
        }

        public Employee Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Employee> query = dbContext.Employees;

            if (useNavigationalProperties) query = query.Include(g => g.Excursions);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            Employee clientFromDb = query.FirstOrDefault(g => g.EmployeeId == key);

            if (clientFromDb is null) throw new ArgumentException($"Employee with id = {key} does not exist!");

            return clientFromDb;
        }

        public List<Employee> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Employee> query = dbContext.Employees;

            if (useNavigationalProperties) query = query.Include(g => g.Excursions);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            return query.ToList();
        }

        public void Update(Employee item, bool useNavigationalProperties = false)
        {
            Employee employeeFromDb = Read(item.EmployeeId, useNavigationalProperties);
            dbContext.Entry<Employee>(employeeFromDb).CurrentValues.SetValues(item);

            if (useNavigationalProperties)
            {
                List<Excursion> excursions = new List<Excursion>(item.Excursions.Count);
                for (int i = 0; i < item.Excursions.Count; i++)
                {
                    Excursion excursionFromDb = dbContext.Excursions.Find(item.Excursions[i].ExcursionId);

                    if (excursionFromDb is not null) excursions.Add(excursionFromDb);
                    else excursions.Add(item.Excursions[i]);
                }

                employeeFromDb.Excursions = excursions;
            }

            dbContext.SaveChanges();
        }

        public void Delete(int key)
        {
            Employee employeeFromDb = Read(key);
            dbContext.Employees.Remove(employeeFromDb);
            dbContext.SaveChanges();
        }
    }
}
