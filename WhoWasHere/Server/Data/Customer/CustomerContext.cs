using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhoWasHere.Shared.Customer;
using WhoWasHere.Shared.Round;

namespace WhoWasHere.Server.Data.Customer
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerModel> CustomerModel { get; set; }
        public DbSet<Appointment> Appointment { get; set; }



        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
