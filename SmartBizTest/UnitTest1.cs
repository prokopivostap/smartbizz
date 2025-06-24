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
        public void GetCustomerById_ShouldReturnNull_WhenIdDoesNotExist()
        {
            var context = GetDbContext();
            var repo = new CustomerRepository(context);

            var result = repo.GetCustomerById(999);

            Assert.Null(result);
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
