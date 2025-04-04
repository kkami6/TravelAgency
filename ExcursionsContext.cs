using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ExcursionsContext: IDb<Excursion, int>
    {
        private TravelAgencyDbContext dbContext;

        public ExcursionsContext(TravelAgencyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Excursion item)
        {
            List<Client> clients = new List<Client>(item.Clients.Count);
            for (int i = 0; i < item.Clients.Count; ++i)
            {
                Client genreFromDb = dbContext.Clients.Find(item.Clients[i].ClientId);
                if (genreFromDb != null) clients.Add(genreFromDb);
                else clients.Add(item.Clients[i]);
            }
            item.Clients = clients;

            List<Employee> employees = new List<Employee>(item.Employees.Count);
            for (int i = 0; i < item.Employees.Count; ++i)
            {
                Employee employeeFromDb = dbContext.Employees.Find(item.Employees[i].EmployeeId);
                if (employeeFromDb != null) employees.Add(employeeFromDb);
                else employees.Add(item.Employees[i]);
            }
            item.Employees = employees;

            dbContext.Excursions.Add(item);
            dbContext.SaveChanges();
        }

        public Excursion Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Excursion> query = dbContext.Excursions;
            if (useNavigationalProperties) query = query
            .Include(e => e.Employees)
            .Include(e => e.Clients);

            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            Excursion excursion = query.FirstOrDefault(e => e.ExcursionId == key);

            if (excursion == null) throw new ArgumentException($"Excursion with id {key} does not exist!");

            return excursion;
        }

        public List<Excursion> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Excursion> query = dbContext.Excursions;
            if (useNavigationalProperties) query = query
            .Include(e => e.Employees)
            .Include(e => e.Clients);

            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            return query.ToList();
        }

        public void Update(Excursion item, bool useNavigationalProperties = false)
        {
            Excursion excursionFromDb = Read(item.ExcursionId, useNavigationalProperties);

            dbContext.Entry<Excursion>(excursionFromDb).CurrentValues.SetValues(item);

            if (useNavigationalProperties)
            {
                List<Employee> employees = new List<Employee>(item.Employees.Count);
                for (int i = 0; i < item.Employees.Count; ++i)
                {
                    Employee employeeFromDb = dbContext.Employees.Find(item.Employees[i].EmployeeId);
                    if (employeeFromDb != null) employees.Add(employeeFromDb);
                    else employees.Add(item.Employees[i]);
                }
                excursionFromDb.Employees = employees;

                List<Client> clients = new List<Client>(item.Clients.Count);
                for (int i = 0; i < item.Clients.Count; ++i)
                {
                    Client clientFromDb = dbContext.Clients.Find(item.Clients[i].ClientId);
                    if (clientFromDb != null) clients.Add(clientFromDb);
                    else clients.Add(item.Clients[i]);
                }
                excursionFromDb.Clients = clients;
            }

            dbContext.SaveChanges();
        }

        public void Delete(int key)
        {
            Excursion excursionFromDb = Read(key);
            dbContext.Excursions.Remove(excursionFromDb);
            dbContext.SaveChanges();
        }
    }
}
