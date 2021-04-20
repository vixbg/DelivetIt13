using System;
using System.Collections.Generic;
using System.Linq;
using DeliverIt13.Data;
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
            if (id == null)
            {
                throw new NullReferenceException("ID cannot be Null or Empty.");

            }
            var parcel = this.dbContext.Parcels.FirstOrDefault(p => p.ParcelId == id);
            if (parcel == null)
            {
                throw new NullReferenceException("No parcel found with this ID.");
            }
            var newDTO = new ParcelCustomerDTO(parcel);
            return newDTO;
        }

        public List<ParcelCustomerDTO> GetAllCustomer(UserAuthDTO user)
        {
            var parcels = this.dbContext
                .Parcels
                .Include(p => p.CustomerId)
                .Include(p => p.WarehouseId)
                .Where(p => p.CustomerId == user.UserId)
                .ToList();
            if (parcels == null)
            {
                throw new NullReferenceException("No parcels found.");
            }
            var parcelDTOs = new List<ParcelCustomerDTO>();
            foreach (var parcel in parcels)
            {
                var newDTO = new ParcelCustomerDTO(parcel);
                parcelDTOs.Add(newDTO);
            }

            return parcelDTOs;
        }

        public List<ParcelEmployeeDTO> GetAll()
        {
            var parcels = this.dbContext
                .Parcels
                .Include(p => p.CustomerId)
                .Include(p => p.WarehouseId)
                .ToList();
            if (parcels == null)
            {
                throw new NullReferenceException("No parcels found.");
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
                throw new NullReferenceException("Input Parcel is Empty or Null.");
            }
            var newParcel = new Parcel();
            newParcel.Category  = parcel.Category;
            newParcel.CustomerId  = parcel.CustomerId;
            newParcel.Weight  = parcel.Weight;
            newParcel.WarehouseId  = parcel.WarehouseId;
            
            this.dbContext.Parcels.Add(newParcel);
            this.dbContext.SaveChanges();
            
            return parcel;
        }

        public void Delete(int id)
        {
            if (id == null)
            {
                throw new NullReferenceException("ID cannot be Null or Empty.");

            }
            var parcel = this.dbContext.Parcels.FirstOrDefault(p => p.ParcelId == id);
            if (parcel == null)
            {
                throw new NullReferenceException("No parcels found with this ID.");
            }

            this.dbContext.Parcels.Remove(parcel);
            this.dbContext.SaveChanges();

            return;
        }

        public ParcelCreateDTO Update(ParcelCreateDTO parcelDTO)
        {
            if (parcelDTO == null)
            {
                throw new NullReferenceException("Input Parcel is Empty or Null.");
            }
            var parcel = this.dbContext.Parcels.FirstOrDefault(p => p.ParcelId == parcelDTO.ParcelId);

            if (parcel == null)
            {
                throw new NullReferenceException("No parcels found with this ID.");
            }

            parcel.Category = parcelDTO.Category;
            parcel.WarehouseId = parcelDTO.WarehouseId;
            parcel.CustomerId = parcelDTO.CustomerId;
            parcel.Weight = parcelDTO.Weight;
            this.dbContext.SaveChanges();

            return parcelDTO;
        }

        public List<ParcelEmployeeDTO> GetAllFiltered(ParcelFilterDto filter)
        {
            var data = this.dbContext
                .Parcels
                .Include(p => p.CustomerId)
                .Include(p => p.WarehouseId)
                .AsQueryable();

            if (filter.Weight.HasValue)
            {
                data = data.Where(p => p.Weight == filter.Weight);
            }

            if (!string.IsNullOrEmpty(filter.Customer))
            {
                data = data.Where(p => p.Customer.FirstName.Contains(filter.Customer));
            }

            var parcels = data.ToList();
            if (parcels == null)
            {
                throw new NullReferenceException("No parcels found.");
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