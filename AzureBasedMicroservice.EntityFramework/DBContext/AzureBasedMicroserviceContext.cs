﻿using AzureBasedMicroservice.EntityFramework.Alterings;
using AzureBasedMicroservice.EntityFramework.Customers;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AzureBasedMicroservice.EntityFramework.DBContext
{
    public class AzureBasedMicroserviceContext : DbContext, IUnitOfWork
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var sqlitePath = Path.Combine("D:", "SampleDb.db");
            options.UseSqlite($"Data Source={sqlitePath}");
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Altering> Alterings { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
