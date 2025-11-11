using System.Collections.Generic;
using System.Linq;
using SmartBiz.Domain;
using SmartBiz.Application.DTO;
using SmartBiz.Application.Interfaces;

namespace SmartBiz.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCustomer(CustomerDto dto)
        {
            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Age = dto.Age 
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            return _context.Customers
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Age = c.Age 
                })
                .ToList();
        }

        public CustomerDto GetCustomerById(int id)
        {
            var c = _context.Customers.Find(id);
            return c == null ? null : new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Age = c.Age 
            };
        }

        public void UpdateCustomer(CustomerDto dto)
        {
            var customer = _context.Customers.Find(dto.Id);
            if (customer != null)
            {
                customer.FirstName = dto.FirstName;
                customer.LastName = dto.LastName;
                customer.Email = dto.Email;
                customer.PhoneNumber = dto.PhoneNumber;
                customer.Age = dto.Age; 

                _context.SaveChanges();
            }
        }
    }
}