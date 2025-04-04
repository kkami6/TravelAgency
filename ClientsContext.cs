using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DataLayer
{
    public class ClientsContext: IDb<Client, int>
    {
        private TravelAgencyDbContext dbContext;

        public ClientsContext(TravelAgencyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Client item)
        {
            dbContext.Clients.Add(item);
            dbContext.SaveChanges();
        }

        public Client Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Client> query = dbContext.Clients;

            if (useNavigationalProperties) query = query.Include(g => g.Excursions);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            Client clientFromDb = query.FirstOrDefault(g => g.ClientId == key);

            if (clientFromDb is null) throw new ArgumentException($"Client with id = {key} does not exist!");

            return clientFromDb;
        }

        public List<Client> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Client> query = dbContext.Clients;

            if (useNavigationalProperties) query = query.Include(g => g.Excursions);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            return query.ToList();
        }

        public void Update(Client item, bool useNavigationalProperties = false)
        {
            Client clientFromDb = Read(item.ClientId, useNavigationalProperties);

            dbContext.Entry<Client>(clientFromDb).CurrentValues.SetValues(item);

            if (useNavigationalProperties)
            {
                List<Excursion> excursions = new List<Excursion>(item.Excursions.Count);
                for (int i = 0; i < item.Excursions.Count; i++)
                {
                    Excursion excursionFromDb = dbContext.Excursions.Find(item.Excursions[i].ExcursionId);

                    if (excursionFromDb is not null) excursions.Add(excursionFromDb);
                    else excursions.Add(item.Excursions[i]);
                }

                clientFromDb.Excursions = excursions;
            }

            dbContext.SaveChanges();
        }

        public void Delete(int key)
        {
            Client clientFromDb = Read(key);
            dbContext.Clients.Remove(clientFromDb);
            dbContext.SaveChanges();
        }
    }
}
