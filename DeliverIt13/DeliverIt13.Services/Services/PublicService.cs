using System.Collections.Generic;
using System.Linq;
using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliverIt13.Services
{
    public class PublicService : IPublicService
    {
        private readonly DeliverItContext dbContext;
        public PublicService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CustomerPublicDTO GetCustCount()
        {
            var customers = this.dbContext.Customers.Count();
            var customersDTO = new CustomerPublicDTO();
            customersDTO.CustomerCount = customers;

            return customersDTO;
        }

        public List<WarehouseDTO> GetWarehouses()
        {
            var warehouses = this.dbContext
                .Warehouses
                .Include(w => w.City)
                .ThenInclude(c => c.Country)
                .ToList();

            var warehousePublicDTOs = new List<WarehouseDTO>();
            foreach (var warehouse in warehouses)
            {
                var newDTO = new WarehouseDTO(warehouse);
                warehousePublicDTOs.Add(newDTO);
            }

            return warehousePublicDTOs;
        }

        public UserPublicDTO Register(UserPublicDTO user)
        {
            var newUser = new User();
            newUser.Type = user.Type;
            newUser.Password = user.Password;
            newUser.Email = user.Email;

            this.dbContext.Users.Add(newUser);
            this.dbContext.SaveChanges();
            
            return user;
        }
    }
}