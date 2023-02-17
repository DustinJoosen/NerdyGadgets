﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NerdyGadgets.Data;

#nullable disable

namespace NerdyGadgets.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230215115530_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NerdyGadgets.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("country");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("street");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("zipcode");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("NerdyGadgets.Models.Category", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)")
                        .HasColumnName("code");

                    b.Property<string>("Image")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("name");

                    b.HasKey("Code");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("NerdyGadgets.Models.CouponCode", b =>
                {
                    b.Property<string>("Coupon")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("coupon");

                    b.Property<bool>("OneTimeUse")
                        .HasColumnType("bit")
                        .HasColumnName("one_time_use");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("percentage");

                    b.HasKey("Coupon");

                    b.ToTable("CouponCodes");
                });

            modelBuilder.Entity("NerdyGadgets.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("address");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("comments");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("customer");

                    b.Property<int>("DiscountPercentage")
                        .HasColumnType("int")
                        .HasColumnName("discount_percentage");

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("discount_price");

                    b.Property<DateTime>("ExpectedDeliveryDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("expected_delivery_date");

                    b.Property<bool>("IsDelivered")
                        .HasColumnType("bit")
                        .HasColumnName("delivered");

                    b.Property<DateTime>("OrderedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("ordered_at");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("NerdyGadgets.Models.OrderLine", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("order");

                    b.Property<int>("ProductNumber")
                        .HasColumnType("int")
                        .HasColumnName("product");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("OrderId", "ProductNumber");

                    b.HasIndex("ProductNumber");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("NerdyGadgets.Models.Product", b =>
                {
                    b.Property<int>("ProductNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("product_number");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductNumber"));

                    b.Property<string>("CategoryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)")
                        .HasColumnName("category");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("description");

                    b.Property<string>("Media")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("media");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("name");

                    b.Property<int>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("unit_price");

                    b.HasKey("ProductNumber");

                    b.HasIndex("CategoryCode");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("NerdyGadgets.Models.ProductSpec", b =>
                {
                    b.Property<int>("ProductNumber")
                        .HasColumnType("int")
                        .HasColumnName("product");

                    b.Property<string>("SpecName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("spec_name");

                    b.Property<string>("SpecVal")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("spec_val");

                    b.HasKey("ProductNumber", "SpecName");

                    b.ToTable("ProductSpecs");
                });

            modelBuilder.Entity("NerdyGadgets.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("AccountCreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("account_created_at");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("address");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("email");

                    b.Property<string>("FistName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("firstname");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("lastname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("password");

                    b.Property<string>("Preposition")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("preposition");

                    b.Property<int>("Role")
                        .HasColumnType("int")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NerdyGadgets.Models.Order", b =>
                {
                    b.HasOne("NerdyGadgets.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NerdyGadgets.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("NerdyGadgets.Models.OrderLine", b =>
                {
                    b.HasOne("NerdyGadgets.Models.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NerdyGadgets.Models.Product", "Product")
                        .WithMany("OrderLines")
                        .HasForeignKey("ProductNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("NerdyGadgets.Models.Product", b =>
                {
                    b.HasOne("NerdyGadgets.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("NerdyGadgets.Models.ProductSpec", b =>
                {
                    b.HasOne("NerdyGadgets.Models.Product", "Product")
                        .WithMany("ProductSpec")
                        .HasForeignKey("ProductNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("NerdyGadgets.Models.User", b =>
                {
                    b.HasOne("NerdyGadgets.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("NerdyGadgets.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("NerdyGadgets.Models.Order", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("NerdyGadgets.Models.Product", b =>
                {
                    b.Navigation("OrderLines");

                    b.Navigation("ProductSpec");
                });
#pragma warning restore 612, 618
        }
    }
}
