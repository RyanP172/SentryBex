using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SentryBex.Models.EpeSchemes;
using SentryBex.Models.UsrSchemes;

namespace SentryBex.Database
{
    public partial class AppDbContext: IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }




        /*public virtual DbSet<ApplicationUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<ApplicationRole> AspNetRoles { get; set; } = null!;*/
        /*public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;*/
        public virtual DbSet<EpeEmployee> EpeEmployees { get; set; } = null!;
        public virtual DbSet<EpeEmployeeCompanyLink> EpeEmployeeCompanyLinks { get; set; } = null!;
        public virtual DbSet<EpeEmployeeContractorCompanyLink> EpeEmployeeContractorCompanyLinks { get; set; } = null!;
        public virtual DbSet<EpeEmployeeGroupLink> EpeEmployeeGroupLinks { get; set; } = null!;
        public virtual DbSet<EpeEmployeeShowroomLink> EpeEmployeeShowroomLinks { get; set; } = null!;
        public virtual DbSet<EprShowroom> EprShowrooms { get; set; } = null!;
        public virtual DbSet<UsrAccount> UsrAccounts { get; set; } = null!;
        public virtual DbSet<UsrAccountLoginHistory> UsrAccountLoginHistories { get; set; } = null!;
        public virtual DbSet<UsrAccountStatus> UsrAccountStatuses { get; set; } = null!;
        public virtual DbSet<UsrGroup> UsrGroups { get; set; } = null!;
        public virtual DbSet<UsrActivityLog> UsrActivityLogs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder
                    .UseLazyLoadingProxies(true)
                    .UseSqlServer("server=WYN-D-006V8; Database=SentryBex_DB; Trusted_Connection=True;");
                //optionsBuilder.UseLazyLoadingProxies(false);
            }*/

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable("AspNetUsers");
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("AspNetRoles");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                entity.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("AspNetUserClaims");
                entity.HasKey(uc => uc.Id);
                entity.HasIndex(uc => uc.UserId);
                entity.Property(uc => uc.ClaimType).HasMaxLength(256);
                entity.Property(uc => uc.ClaimValue).HasMaxLength(256);
                entity.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("AspNetRoleClaims");
                entity.HasKey(uc => uc.Id);
                entity.HasIndex(uc => uc.RoleId);
                entity.Property(uc => uc.ClaimType).HasMaxLength(256);
                entity.Property(uc => uc.ClaimValue).HasMaxLength(256);
                entity.HasOne<IdentityRole>()
                    .WithMany()
                    .HasForeignKey(uc => uc.RoleId)
                    .IsRequired();

            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(l => new { l.UserId, l.LoginProvider, l.ProviderKey });
                entity.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(t => new { t.UserId, t.LoginProvider, t.Name }) ;
                entity.ToTable("AspNetUserTokens");
            });



            /*modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });*/
            /*modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable("AspNetUsers");
                entity.Property(e => e.Id).HasColumnName("Id");
                // Add more property mappings as needed
            });*/
            /*modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEnd).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PK_dbo.AspNetUserRoles");

                            j.ToTable("AspNetUserRoles");

                            j.IndexerProperty<string>("UserId").HasMaxLength(128);

                            j.IndexerProperty<string>("RoleId").HasMaxLength(128);
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });*/

            modelBuilder.Entity<EpeEmployee>(entity =>
            {
                entity.ToTable("epe_employee");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountFk).HasColumnName("account_fk");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.ContractorTypeFk)
                    .HasMaxLength(10)
                    .HasColumnName("contractor_type_fk")
                    .IsFixedLength();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created")
                    .HasDefaultValueSql("getdate()");


                entity.Property(e => e.DefaultShowroomFk).HasColumnName("default_showroom_fk");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(80)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsContractor).HasColumnName("is_contractor");

                entity.Property(e => e.LastName)
                    .HasMaxLength(80)
                    .HasColumnName("last_name");

                entity.Property(e => e.MaxLeadCount).HasColumnName("maxLeadCount");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(80)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasColumnName("modified")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.MonthlyBudget).HasColumnName("monthlyBudget");
            });

            modelBuilder.Entity<EpeEmployeeCompanyLink>(entity =>
            {
                entity.ToTable("epe_employee_company_link");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyFk).HasColumnName("company_fk");

                entity.Property(e => e.EmployeeFk).HasColumnName("employee_fk");
            });

            modelBuilder.Entity<EpeEmployeeContractorCompanyLink>(entity =>
            {
                entity.ToTable("epe_employee_contractor_company_link");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContractorCompanyFk).HasColumnName("contractor_company_fk");

                entity.Property(e => e.EmployeeFk).HasColumnName("employee_fk");
            });

            modelBuilder.Entity<EpeEmployeeGroupLink>(entity =>
            {
                entity.ToTable("epe_employee_group_link");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created")
                    .HasDefaultValueSql("getdate()");


                entity.Property(e => e.EmployeeFk).HasColumnName("employee_fk");

                entity.Property(e => e.GroupFk).HasColumnName("group_fk");
            });

            modelBuilder.Entity<EpeEmployeeShowroomLink>(entity =>
            {
                entity.ToTable("epe_employee_showroom_link");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmployeeFk).HasColumnName("employee_fk");

                entity.Property(e => e.ShowroomFk)
                .HasColumnName("showroom_fk")
                .HasDefaultValue(12);
            });

            modelBuilder.Entity<EprShowroom>(entity =>
            {
                entity.ToTable("epr_showroom");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyFk).HasColumnName("company_fk");

                entity.Property(e => e.DefaultConsultantFk).HasColumnName("default_consultant_fk");

                entity.Property(e => e.MonthlyBudget).HasColumnName("monthlyBudget");

                entity.Property(e => e.Name)
                    .HasMaxLength(160)
                    .HasColumnName("name");

                entity.Property(e => e.OrderPrefix).HasColumnName("order_prefix");

                entity.Property(e => e.ShopCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("shop_code");

                entity.Property(e => e.State)
                    .HasMaxLength(10)
                    .HasColumnName("state")
                    .IsFixedLength();
            });

            modelBuilder.Entity<UsrAccount>(entity =>
            {
                entity.ToTable("usr_account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created")
                    .HasDefaultValueSql("getdate()"); 


                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasColumnName("modified")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(100)
                    .HasColumnName("password_salt");

                entity.Property(e => e.ResetPwdDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("reset_pwd_datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ResetPwdGuid)
                    .HasColumnName("reset_pwd_guid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SamAccountName)
                    .HasMaxLength(50)
                    .HasColumnName("sam_account_name");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("user_name")
                    .HasComment("must be a valid email address");
            });

            modelBuilder.Entity<UsrAccountLoginHistory>(entity =>
            {
                entity.ToTable("usr_account_login_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountFk).HasColumnName("account_fk");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");
            });

            modelBuilder.Entity<UsrAccountStatus>(entity =>
            {
                entity.ToTable("usr_account_status");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.StatusDesc)
                    .HasMaxLength(50)
                    .HasColumnName("status_desc");
            });

            modelBuilder.Entity<UsrActivityLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("usr_activity_log");

                entity.Property(e => e.LogId)
                    .HasMaxLength(128)
                    .HasColumnName("log_Id");

                entity.Property(e => e.LogActivityStatus)
                    .HasMaxLength(10)
                    .HasColumnName("log_activity_status")
                    .IsFixedLength();

                entity.Property(e => e.LogActivityType)
                    .HasMaxLength(15)
                    .HasColumnName("log_activity_type");

                entity.Property(e => e.LogDetail).HasColumnName("log_detail");

                entity.Property(e => e.LoggedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("logged_date");

                entity.Property(e => e.UserUuid)
                    .HasMaxLength(128)
                    .HasColumnName("user_uuid");
            });

            modelBuilder.Entity<UsrGroup>(entity =>
            {
                entity.ToTable("usr_group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(100)
                    .HasColumnName("group_name");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasColumnName("modified");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
