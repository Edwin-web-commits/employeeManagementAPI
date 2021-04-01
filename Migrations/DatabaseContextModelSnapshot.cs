﻿// <auto-generated />
using EmployeeManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagementAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeeManagementAPI.Data.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentName = "Human Resource"
                        },
                        new
                        {
                            Id = 2,
                            DepartmentName = "Sales"
                        },
                        new
                        {
                            Id = 3,
                            DepartmentName = "Customs"
                        });
                });

            modelBuilder.Entity("EmployeeManagementAPI.Data.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "23 Nelson Mandel strt",
                            DepartmentID = 3,
                            Email = "edwinmoropane02@gmail.com",
                            EmployeeName = "Moropane Motlokwa",
                            IdentityNo = "9509294764082",
                            Position = "Supervisor",
                            Salary = 50000.0
                        },
                        new
                        {
                            Id = 2,
                            Address = "29 Nelson Mandel strt",
                            DepartmentID = 2,
                            Email = "edwinmoropane03@gmail.com",
                            EmployeeName = "Edwin Motlokwa",
                            IdentityNo = "9909294764082",
                            Position = "Manager",
                            Salary = 70000.0
                        },
                        new
                        {
                            Id = 3,
                            Address = "27 Nelson Mandel strt",
                            DepartmentID = 1,
                            Email = "sharonmoropane04@gmail.com",
                            EmployeeName = "Sharon Motlokwa",
                            IdentityNo = "9709294764082",
                            Position = "Supervisor",
                            Salary = 50000.0
                        });
                });

            modelBuilder.Entity("EmployeeManagementAPI.Data.Employee", b =>
                {
                    b.HasOne("EmployeeManagementAPI.Data.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("EmployeeManagementAPI.Data.Department", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}