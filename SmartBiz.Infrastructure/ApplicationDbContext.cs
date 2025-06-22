using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartBiz.Application.DTO;
using SmartBiz.Domain.Models;

namespace SmartBiz.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet <Inventory> Inventories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FinancialRecord> FinancialRecords { get; set; }
    }
}