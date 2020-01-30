﻿// <auto-generated />
using AzureBasedMicroservice.EntityFramework.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AzureBasedMicroservice.EntityFramework.Migrations
{
    [DbContext(typeof(AzureBasedMicroserviceContext))]
    partial class AzureBasedMicroserviceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113");

            modelBuilder.Entity("AzureBasedMicroservice.EntityFramework.Alterings.Altering", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<int>("Direction");

                    b.Property<int>("Operation");

                    b.Property<int>("State");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Alterings");
                });

            modelBuilder.Entity("AzureBasedMicroservice.EntityFramework.Customers.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullName");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new { Id = 1, FullName = "User 1" },
                        new { Id = 2, FullName = "User 2" }
                    );
                });

            modelBuilder.Entity("AzureBasedMicroservice.EntityFramework.Customers.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlteringId");

                    b.Property<double>("Amount");

                    b.Property<string>("TrackingCode");

                    b.HasKey("Id");

                    b.HasIndex("AlteringId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("AzureBasedMicroservice.EntityFramework.Alterings.Altering", b =>
                {
                    b.HasOne("AzureBasedMicroservice.EntityFramework.Customers.Customer", "Customer")
                        .WithMany("Alterings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AzureBasedMicroservice.EntityFramework.Customers.Payment", b =>
                {
                    b.HasOne("AzureBasedMicroservice.EntityFramework.Alterings.Altering", "Altering")
                        .WithMany()
                        .HasForeignKey("AlteringId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
