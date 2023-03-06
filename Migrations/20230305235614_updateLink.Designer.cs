﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SentryBex.Database;

#nullable disable

namespace SentryBex.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230305235614_updateLink")]
    partial class updateLink
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "ProviderKey");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.EpeSchemes.EpeEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("AccountFk")
                        .HasColumnType("bigint")
                        .HasColumnName("account_fk");

                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("code");

                    b.Property<string>("ContractorTypeFk")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("contractor_type_fk")
                        .IsFixedLength();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("DefaultShowroomFk")
                        .HasColumnType("int")
                        .HasColumnName("default_showroom_fk");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsContractor")
                        .HasColumnType("bit")
                        .HasColumnName("is_contractor");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("last_name");

                    b.Property<int?>("MaxLeadCount")
                        .HasColumnType("int")
                        .HasColumnName("maxLeadCount");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("middle_name");

                    b.Property<DateTime?>("Modified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("modified")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("MonthlyBudget")
                        .HasColumnType("int")
                        .HasColumnName("monthlyBudget");

                    b.HasKey("Id");

                    b.ToTable("epe_employee", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.EpeSchemes.EpeEmployeeCompanyLink", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("CompanyFk")
                        .HasColumnType("int")
                        .HasColumnName("company_fk");

                    b.Property<long>("EmployeeFk")
                        .HasColumnType("bigint")
                        .HasColumnName("employee_fk");

                    b.HasKey("Id");

                    b.ToTable("epe_employee_company_link", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.EpeSchemes.EpeEmployeeContractorCompanyLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContractorCompanyFk")
                        .HasColumnType("int")
                        .HasColumnName("contractor_company_fk");

                    b.Property<int>("EmployeeFk")
                        .HasColumnType("int")
                        .HasColumnName("employee_fk");

                    b.HasKey("Id");

                    b.ToTable("epe_employee_contractor_company_link", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.EpeSchemes.EpeEmployeeGroupLink", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.Property<long>("EmployeeFk")
                        .HasColumnType("bigint")
                        .HasColumnName("employee_fk");

                    b.Property<short>("GroupFk")
                        .HasColumnType("smallint")
                        .HasColumnName("group_fk");

                    b.HasKey("Id");

                    b.ToTable("epe_employee_group_link", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.EpeSchemes.EpeEmployeeShowroomLink", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("EmployeeFk")
                        .HasColumnType("bigint")
                        .HasColumnName("employee_fk");

                    b.Property<int>("ShowroomFk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(12)
                        .HasColumnName("showroom_fk");

                    b.HasKey("Id");

                    b.ToTable("epe_employee_showroom_link", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.EpeSchemes.EprShowroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyFk")
                        .HasColumnType("int")
                        .HasColumnName("company_fk");

                    b.Property<int>("DefaultConsultantFk")
                        .HasColumnType("int")
                        .HasColumnName("default_consultant_fk");

                    b.Property<short?>("LoadDay")
                        .HasColumnType("smallint");

                    b.Property<int?>("MonthlyBudget")
                        .HasColumnType("int")
                        .HasColumnName("monthlyBudget");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("nvarchar(160)")
                        .HasColumnName("name");

                    b.Property<int>("OrderPrefix")
                        .HasColumnType("int")
                        .HasColumnName("order_prefix");

                    b.Property<string>("ShopCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("shop_code");

                    b.Property<string>("State")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("state")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("epr_showroom", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.UsrSchemes.UsrAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("Modified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasColumnName("modified")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("password");

                    b.Property<string>("PasswordSalt")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("password_salt");

                    b.Property<DateTime?>("ResetPwdDatetime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasColumnName("reset_pwd_datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid?>("ResetPwdGuid")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("reset_pwd_guid")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("SamAccountName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("sam_account_name");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("status")
                        .HasDefaultValueSql("('A')")
                        .IsFixedLength();

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("user_name")
                        .HasComment("must be a valid email address");

                    b.HasKey("Id");

                    b.ToTable("usr_account", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.UsrSchemes.UsrAccountLoginHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AccountFk")
                        .HasColumnType("bigint")
                        .HasColumnName("account_fk");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("created");

                    b.HasKey("Id");

                    b.ToTable("usr_account_login_history", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.UsrSchemes.UsrAccountStatus", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"), 1L, 1);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("status")
                        .IsFixedLength();

                    b.Property<string>("StatusDesc")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("status_desc");

                    b.HasKey("Id");

                    b.ToTable("usr_account_status", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.UsrSchemes.UsrActivityLog", b =>
                {
                    b.Property<string>("LogId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("log_Id");

                    b.Property<string>("LogActivityStatus")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("log_activity_status")
                        .IsFixedLength();

                    b.Property<string>("LogActivityType")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("log_activity_type");

                    b.Property<string>("LogDetail")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("log_detail");

                    b.Property<DateTime?>("LoggedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("logged_date");

                    b.Property<string>("UserUuid")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("user_uuid");

                    b.HasKey("LogId");

                    b.ToTable("usr_activity_log", (string)null);
                });

            modelBuilder.Entity("SentryBex.Models.UsrSchemes.UsrGroup", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("description");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("group_name");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.HasKey("Id");

                    b.ToTable("usr_group", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
