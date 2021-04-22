using System;
using System.Collections.Generic;
using System.Linq;
using DeliverIt13.Data;
using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models;
using DeliverIt13.Services.Models.ParcelDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliverIt13.Services
{
    public class ParcelService : IParcelService
    {

        private readonly DeliverItContext dbContext;

        public ParcelService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ParcelCustomerDTO Get(int id)
        {
            
            var parcel = this.dbContext.Parcels
                .Include(p=>p.Customer)
                .ThenInclude(c=>c.User)
                .Include(p=>p.Warehouse)
                .ThenInclude(w=>w.City)
                .Include(p=>p.Shipment)
                .FirstOrDefault(p => p.ParcelId == id);
            if (parcel == null)
            {
                throw new Exception("No parcel found with this ID.");
            }
            var newDTO = new ParcelCustomerDTO(parcel);
            return newDTO;
        }

        public List<ParcelSortDTO> GetAllCustomer(UserAuthDTO user)
        {
            var parcels = this.dbContext
                .Parcels
                .Include(p => p.Customer)
                .ThenInclude(c=>c.User)
                .Where(p => p.CustomerId == user.UserId)
                .ToList();
            if (parcels.Count == 0)
            {
                throw new Exception("No parcels found.");
            }
            var parcelDTOs = new List<ParcelSortDTO>();
            foreach (var parcel in parcels)
            {
                var newDTO = new ParcelSortDTO(parcel);
                parcelDTOs.Add(newDTO);
            }

            return parcelDTOs;
        }

        public List<ParcelEmployeeDTO> GetAll()
        {
            var parcels = this.dbContext
                .Parcels
                .Include(p => p.Customer)
                .Include(p => p.Warehouse)
                .Include(p => p.Shipment)
                .ToList();
            if (parcels == null)
            {
                throw new Exception("No parcels found.");
            }
            var parcelDTOs = new List<ParcelEmployeeDTO>();
            foreach (var parcel in parcels)
            {
                var newDTO = new ParcelEmployeeDTO(parcel);
                parcelDTOs.Add(newDTO);
            }

            return parcelDTOs;
        }

        public ParcelCreateDTO Create(ParcelCreateDTO parcel)
        {
            if (parcel == null)
            {
                throw new Exception("Input Parcel is Empty or Null.");
            }
            var newParcel = new Parcel();
            newParcel.Category  = parcel.Category;
            newParcel.CustomerId  = parcel.CustomerId;
            newParcel.Weight  = parcel.Weight;
            newParcel.WarehouseId  = parcel.WarehouseId;
            newParcel.ShipmentId  = parcel.ShipmentId;
            
            this.dbContext.Parcels.Add(newParcel);
            this.dbContext.SaveChanges();
            
            return parcel;
        }

        public void Delete(int id)
        {
            var parcel = this.dbContext.Parcels.FirstOrDefault(p => p.ParcelId == id);

            if (parcel == null)
            {
                throw new Exception("No parcels found with this ID.");
            }

            this.dbContext.Parcels.Remove(parcel);
            this.dbContext.SaveChanges();

            return;
        }

        public ParcelCreateDTO Update(ParcelCreateDTO parcelDTO)
        {
            if (parcelDTO == null)
            {
                throw new Exception("Input Parcel is Empty or Null.");
            }
            var parcel = this.dbContext.Parcels.FirstOrDefault(p => p.ParcelId == parcelDTO.ParcelId);

            if (parcel == null)
            {
                throw new Exception("No parcels found with this ID.");
            }

            parcel.Category = parcelDTO.Category;
            parcel.WarehouseId = parcelDTO.WarehouseId;
            parcel.CustomerId = parcelDTO.CustomerId;
            parcel.Weight = parcelDTO.Weight;
            parcel.ShipmentId = parcelDTO.ShipmentId;
            this.dbContext.SaveChanges();

            return parcelDTO;
        }

        public List<ParcelEmployeeDTO> GetAllFiltered(ParcelFilterDTO filter)
        {
            var data = this.dbContext
                .Parcels
                .Include(p => p.Customer)
                .Include(p => p.Warehouse)
                .AsQueryable();

            if (filter.Weight.HasValue)
            {
                data = data.Where(p => p.Weight == filter.Weight);
            }

            if (!string.IsNullOrEmpty(filter.Customer))
            {
                data = data.Where(p => p.Customer.FirstName.Contains(filter.Customer));
            }

            if (!string.IsNullOrEmpty(filter.Warehouse))
            {
                data = data.Where(p => p.Warehouse.City.Name.Contains(filter.Warehouse));
            }

            if (filter.Category != null)
            {
                data = data.Where(p => p.Category.Equals(filter.Category));
            }

            var parcels = data.ToList();
            if (parcels == null)
            {
                throw new Exception("No parcels found.");
            }
            var parcelDTOs = new List<ParcelEmployeeDTO>();
            foreach (var parcel in parcels)
            {
                var newDTO = new ParcelEmployeeDTO(parcel);
                parcelDTOs.Add(newDTO);
            }

            return parcelDTOs;
        }
    }
}