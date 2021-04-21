using DeliverIt13.Data;
using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models.CustomerDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliverIt13.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DeliverItContext dbContext;
        public CustomerService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CustomerCreateDTO Create(CustomerCreateDTO customer)
        {
            if (customer == null)
            {
                throw new ArgumentException("Cannot create customer with no data.");
            }

            if (this.dbContext.Users.FirstOrDefault(u => u.Email.Equals(customer.Email)) != null)
            {
                throw new ArgumentException($"User with email {customer.Email} already exists.");
            }

            Customer newCustomer = new Customer()
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserId = customer.UserId,
                User = customer.User,
                CityId = customer.CityId,
                City = customer.City,
                Street = customer.Street
            };

            User newUser = new User()
            {
                UserId = customer.UserId,
                Type = UserType.Customer,
                Email = customer.Email,
                Password = customer.Password
            };

            this.dbContext.Add(newCustomer);
            this.dbContext.Add(newUser);
            this.dbContext.SaveChanges();

            return customer;
        }

        public bool Delete(int id)
        {
            var customer = this.dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                throw new ArgumentException($"User with id {id} does not exist.");
            }

            var user = this.dbContext.Users.FirstOrDefault(u => u.UserId == customer.UserId);

            if (user == null)
            {
                throw new ArgumentException($"User with id {id} does not exist.");
            }

            this.dbContext.Remove(customer);
            this.dbContext.Remove(user);
            this.dbContext.SaveChanges();

            return true;
        }

        public CustomerGetDTO Get(int id)
        {
            var customer = this.dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                throw new ArgumentException($"Customer with id {id} does not exist.");
            }

            var user = this.dbContext.Users.FirstOrDefault(u => u.UserId == customer.UserId);

            var customerPublicDTO = new CustomerGetDTO(customer);

            return customerPublicDTO;
        }

        public List<CustomerGetDTO> GetAll()
        {
            var customers = this.dbContext.Customers.Include(c => c.User).ToList();

            if (customers.Count == 0)
            {
                throw new ArgumentException("No users found.");
            }

            var customerList = customers.Select(c => new CustomerGetDTO(c)).ToList();

            return customerList;
        }

        public int GetCount()
        {
            return this.dbContext.Customers.Count();
        }

        public List<CustomerGetDTO> GetByEmail(string email)
        {
            //var user = this.dbContext.Customers.Include(c => c.User).FirstOrDefault(c => c.User.Email == email);
            var customers = this.GetAll();

            var customersByEmail = customers.Where(c => c.Email.Contains(email)).ToList();

            if (customersByEmail.Count == 0)
            {
                throw new ArgumentException($"No users found with email {email}.");
            }

            return customersByEmail;
        }

        public List<CustomerGetDTO> GetByFirstName(string firstName)
        {
            var customers = this.GetAll();

            var customersByFirstName = customers.Where(c => c.FirstName.Equals(firstName)).ToList();

            if (customersByFirstName.Count == 0)
            {
                throw new ArgumentException($"No users found with first name {firstName}.");
            }

            return customersByFirstName;
        }

        public List<CustomerGetDTO> GetByLastName(string lastName)
        {
            var customers = this.GetAll();

            var customersByLastName = customers.Where(c => c.LastName.Equals(lastName)).ToList();

            if (customersByLastName.Count == 0)
            {
                throw new ArgumentException($"No users found with last name {lastName}.");
            }

            return customersByLastName;
        }

        public List<CustomerGetDTO> GetAllFiltered(CustomerFilterDTO customer)
        {
            var customers = this.GetAll();

            if (!string.IsNullOrEmpty(customer.Email))
            {
                customers = customers.Where(c => c.Email.Contains(c.Email)).ToList();
            }

            if (!string.IsNullOrEmpty(customer.FirstName))
            {
                customers = customers.Where(c => c.FirstName.Equals(c.FirstName)).ToList();
            }

            if (!string.IsNullOrEmpty(customer.LastName))
            {
                customers = customers.Where(c => c.LastName.Equals(c.LastName)).ToList();
            }            

            if (customers == null)
            {
                throw new ArgumentException("No users found");
            }

            return customers;
        }

        public CustomerUpdateDTO Update(int id, CustomerUpdateDTO customerDTO)
        {
            if (customerDTO == null)
            {
                throw new ArgumentException("Input customer cannot be null or empty!");
            }

            var customer = this.dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                throw new ArgumentException("No shipment found with this ID.");
            }

            customer.CityId = customerDTO.CityId;
            customer.City = customerDTO.City;
            customer.Street = customerDTO.Street;
            customer.User.Email = customerDTO.Email;
            customer.User.Password = customerDTO.Password;

            this.dbContext.SaveChanges();

            return customerDTO;


        }
    }
}
