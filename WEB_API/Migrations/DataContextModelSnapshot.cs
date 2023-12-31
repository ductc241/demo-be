﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WEB_API.Model;

#nullable disable

namespace WEB_API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WEB_API.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Customer_Id")
                        .HasColumnType("int");

                    b.Property<string>("Customer_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Customer_Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Order_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Payment_Method")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Shipping_Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Total_Amount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("orders", "dbo");
                });

            modelBuilder.Entity("WEB_API.Model.Order_Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("Order_Id")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Product_Id")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Order_Id");

                    b.HasIndex("Product_Id");

                    b.ToTable("order_item", "dbo");
                });

            modelBuilder.Entity("WEB_API.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("products", "dbo");
                });

            modelBuilder.Entity("WEB_API.Model.Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Actual_Arrival_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Actual_Delivery_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Customer_Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Customer_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Customer_Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("Estimated_Arrival_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Estimated_Delivery_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order_Id")
                        .HasColumnType("int");

                    b.Property<int>("Shipment_Status_Id")
                        .HasColumnType("int");

                    b.Property<decimal>("Shipping_Fee")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("Order_Id")
                        .IsUnique();

                    b.HasIndex("Shipment_Status_Id");

                    b.ToTable("shipments", "dbo");
                });

            modelBuilder.Entity("WEB_API.Model.Shipment_Detail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Barcode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Driver_Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Driver_Phone")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Packaging_Type")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Shipment_Id")
                        .HasColumnType("int");

                    b.Property<int>("Shipping_Carrier_Id")
                        .HasColumnType("int");

                    b.Property<string>("Shipping_Method")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("Shipment_Id")
                        .IsUnique();

                    b.HasIndex("Shipping_Carrier_Id");

                    b.ToTable("shipment_detail", "dbo");
                });

            modelBuilder.Entity("WEB_API.Model.Shipment_Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("shipment_status", "dbo");
                });

            modelBuilder.Entity("WEB_API.Model.Shipping_Carrier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Contact_Person")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Note")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone_Number")
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("shipping_carrier", "dbo");
                });

            modelBuilder.Entity("WEB_API.Model.Tracking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("From_Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Shipment_Id")
                        .HasColumnType("int");

                    b.Property<string>("To_Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tracking_Status_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Shipment_Id");

                    b.HasIndex("Tracking_Status_Id");

                    b.ToTable("tracking", "dbo");
                });

            modelBuilder.Entity("WEB_API.Model.Tracking_Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("tracking_status", "dbo");
                });

            modelBuilder.Entity("WEB_API.Model.Order_Item", b =>
                {
                    b.HasOne("WEB_API.Model.Order", "Order")
                        .WithMany("Order_Item")
                        .HasForeignKey("Order_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEB_API.Model.Product", "Product")
                        .WithMany("Order_Item")
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WEB_API.Model.Shipment", b =>
                {
                    b.HasOne("WEB_API.Model.Order", "Order")
                        .WithOne("Shipment")
                        .HasForeignKey("WEB_API.Model.Shipment", "Order_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEB_API.Model.Shipment_Status", "Shipment_Status")
                        .WithMany("Shipments")
                        .HasForeignKey("Shipment_Status_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Shipment_Status");
                });

            modelBuilder.Entity("WEB_API.Model.Shipment_Detail", b =>
                {
                    b.HasOne("WEB_API.Model.Shipment", "shipment")
                        .WithOne("Shipment_Detail")
                        .HasForeignKey("WEB_API.Model.Shipment_Detail", "Shipment_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEB_API.Model.Shipping_Carrier", "Shipping_Carrier")
                        .WithMany("Shipment_Detail")
                        .HasForeignKey("Shipping_Carrier_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shipping_Carrier");

                    b.Navigation("shipment");
                });

            modelBuilder.Entity("WEB_API.Model.Tracking", b =>
                {
                    b.HasOne("WEB_API.Model.Shipment", "Shipment")
                        .WithMany("Trackings")
                        .HasForeignKey("Shipment_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEB_API.Model.Tracking_Status", "Tracking_Status")
                        .WithMany("Trackings")
                        .HasForeignKey("Tracking_Status_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shipment");

                    b.Navigation("Tracking_Status");
                });

            modelBuilder.Entity("WEB_API.Model.Order", b =>
                {
                    b.Navigation("Order_Item");

                    b.Navigation("Shipment")
                        .IsRequired();
                });

            modelBuilder.Entity("WEB_API.Model.Product", b =>
                {
                    b.Navigation("Order_Item");
                });

            modelBuilder.Entity("WEB_API.Model.Shipment", b =>
                {
                    b.Navigation("Shipment_Detail")
                        .IsRequired();

                    b.Navigation("Trackings");
                });

            modelBuilder.Entity("WEB_API.Model.Shipment_Status", b =>
                {
                    b.Navigation("Shipments");
                });

            modelBuilder.Entity("WEB_API.Model.Shipping_Carrier", b =>
                {
                    b.Navigation("Shipment_Detail");
                });

            modelBuilder.Entity("WEB_API.Model.Tracking_Status", b =>
                {
                    b.Navigation("Trackings");
                });
#pragma warning restore 612, 618
        }
    }
}
