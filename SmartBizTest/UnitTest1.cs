using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartBiz.Application.DTO;
using SmartBiz.Domain;
using SmartBiz.Infrastructure;
using SmartBiz.Infrastructure.Repositories;
using Xunit;

namespace SmartBiz.Tests
{
    public class CustomerRepositoryTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public void AddCustomer_ShouldAddSuccessfully()
        {
            
        }
        [Fact]
        public void AddCustomer_ShouldNotAdd_WhenMissingRequiredFields()
        {
            var context = GetDbContext();
            var repo = new CustomerRepository(context);

            var customerDto = new CustomerDto
            {
                LastName = "Test",
                PhoneNumber = "123"
            };

            repo.AddCustomer(customerDto);
            var customers = context.Customers.ToList();

            Assert.Empty(customers);
        }

        [Fact]
        public void GetCustomerById_ShouldReturnNull_WhenIdDoesNotExist()
        {
            var context = GetDbContext();
            var repo = new CustomerRepository(context);

            var result = repo.GetCustomerById(999);

            Assert.Null(result);
        }

        [Fact]
        public void UpdateCustomer_ShouldThrow_WhenCustomerNotFound()
        {
            var context = GetDbContext();
            var repo = new CustomerRepository(context);

            var dto = new CustomerDto
            {
                Id = 123,
                FirstName = "Does",
                LastName = "NotExist",
                Email = "not@found.com",
                PhoneNumber = "000"
            };

            Assert.Throws<KeyNotFoundException>(() => repo.UpdateCustomer(dto));
        }

        [Fact]
        public void DeleteCustomer_ShouldDoNothing_WhenIdNotFound()
        {
            var context = GetDbContext();
            var repo = new CustomerRepository(context);

            repo.DeleteCustomer(999);

            var all = context.Customers.ToList();
            Assert.Empty(all);
        }
    }
}
