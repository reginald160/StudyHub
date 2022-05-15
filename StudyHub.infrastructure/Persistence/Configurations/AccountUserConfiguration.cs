using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Infrastructure.Persistence.Configurations
{
    public class AccountUserConfiguration : IEntityTypeConfiguration<AccountUser>
    {
       

        public void Configure(EntityTypeBuilder<AccountUser> builder)
        {
            builder.ToTable("UserAccount")
                .HasKey(a => a.UserId);
            builder.Property(a => a.UserId)
            .HasColumnName("Id")
            .IsRequired();

            builder.Property(a => a.FirstName)
               .HasColumnName("FirstName")
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(a => a.LastName)
                .HasColumnName("LastName")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.EmailAddress)
                .HasColumnName("EmailAddress")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PhoneNumber)
               .HasColumnName("PhoneNumber")
               .IsRequired();
              
            builder.Property(a => a.Password)
                .HasColumnName("Password")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.HashPasswordSalt)
                .HasColumnName("PasswordHashSalt")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(a => a.Address)
                .HasColumnName("Address")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(a => a.ZipCode)
                .HasColumnName("ZipCode")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Imageurl)
                .HasColumnName("Imageurl");

            builder.Property(a => a.ReferalCode)
               .HasColumnName("ReferalCode")
               .HasMaxLength(100);

            builder.Property(a => a.RefereeCode)
               .HasColumnName("RefereeCode")
               .HasMaxLength(150);

            builder.Property(a => a.InvitationCode)
              .HasColumnName("InvitationCode")
              .HasMaxLength(100);

            builder.Property(a => a.State)
               .HasColumnName("State")
               .HasMaxLength(100);
               

            builder.Property(a => a.City)
                .HasColumnName("City")
                .HasMaxLength(100);
              

            builder.Property(a => a.Country)
                .HasColumnName("Country")
                .HasMaxLength(100);
            

            builder.Property(a => a.IsEmailVerified)
                .HasColumnName("IsEmailVerified");



            builder.Property(a => a.CreatedDate)
                .HasColumnName("CreatedDate");

            builder.Property(a => a.EditedDate)
                .HasColumnName("EditedDate");

            builder.Property(a => a.IsActive)
             .HasColumnName("IsActive");
            builder.Property(a => a.IsDeleted)
            .HasColumnName("IsDeleted");

        }
    }
}
