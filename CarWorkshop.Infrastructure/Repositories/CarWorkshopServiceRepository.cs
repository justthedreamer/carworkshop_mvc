using CarWorkshop.Domain.Entities;
using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistance;
using CarWorkshop.Infrastructure.Seenders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Repositories
{
    public class CarWorkshopServiceRepository : ICarWorkshopServiceRepository
    {
        private readonly CarWorkshopDbContext dbContext;

        public CarWorkshopServiceRepository(CarWorkshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(CarWorkshopService carWorkshopService)
        {
            dbContext.Services.Add(carWorkshopService);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarWorkshopService>> GetAllByEncodedName(string encodedName)
        {
            return await dbContext.Services
                    .Where(s => s.CarWorkshop.EncodedName == encodedName)
                    .ToListAsync();
        }
    }
}
