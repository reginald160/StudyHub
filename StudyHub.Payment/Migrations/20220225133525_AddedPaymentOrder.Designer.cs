﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudyHub.Payment.Db;

namespace StudyHub.Payment.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220225133525_AddedPaymentOrder")]
    partial class AddedPaymentOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudyHub.Domain.Models.AccountUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DOB")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashPasswordSalt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imageurl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvitationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProfiled")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefereeCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AccountUser");
                });

            modelBuilder.Entity("StudyHub.Payment.DomainServices.Licence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ActivationIV")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("ActivationKey")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LicenceKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MerchantIdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Token")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Licences");
                });

            modelBuilder.Entity("StudyHub.Payment.DomainServices.Merchant", b =>
                {
                    b.Property<Guid>("MerchantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApiKey")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("MerchantId");

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("StudyHub.Payment.DomainServices.PaymentModel", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ExternalShopperIdentifier")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentOrderUniqueIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("PaymentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("StudyHub.Payment.DomainServices.PaymentOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CancelUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CompletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CompletionUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNewOrder")
                        .HasColumnType("bit");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<string>("RedirectionUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountUserId");

                    b.ToTable("PaymentOrders");
                });

            modelBuilder.Entity("StudyHub.Domain.Models.AccountUser", b =>
                {
                    b.HasOne("StudyHub.Domain.Models.AccountUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudyHub.Payment.DomainServices.Merchant", b =>
                {
                    b.OwnsOne("StudyHub.Payment.DomainServices.CardInformation", "CreditCardInformation", b1 =>
                        {
                            b1.Property<Guid>("MerchantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CardNumber")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<int>("Ccv")
                                .HasMaxLength(5)
                                .HasColumnType("int");

                            b1.HasKey("MerchantId");

                            b1.ToTable("Merchants");

                            b1.WithOwner()
                                .HasForeignKey("MerchantId");

                            b1.OwnsOne("StudyHub.Payment.DomainServices.ExpiryDate", "ExpiryDate", b2 =>
                                {
                                    b2.Property<Guid>("CardInformationMerchantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Month")
                                        .HasColumnType("int");

                                    b2.Property<int>("Year")
                                        .HasColumnType("int");

                                    b2.HasKey("CardInformationMerchantId");

                                    b2.ToTable("Merchants");

                                    b2.WithOwner()
                                        .HasForeignKey("CardInformationMerchantId");
                                });

                            b1.Navigation("ExpiryDate");
                        });

                    b.Navigation("CreditCardInformation");
                });

            modelBuilder.Entity("StudyHub.Payment.DomainServices.PaymentModel", b =>
                {
                    b.OwnsOne("StudyHub.Payment.DomainServices.CardInformation", "CreditCardInformation", b1 =>
                        {
                            b1.Property<Guid>("PaymentModelPaymentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CardNumber")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<int>("Ccv")
                                .HasMaxLength(5)
                                .HasColumnType("int");

                            b1.HasKey("PaymentModelPaymentId");

                            b1.ToTable("Payments");

                            b1.WithOwner()
                                .HasForeignKey("PaymentModelPaymentId");

                            b1.OwnsOne("StudyHub.Payment.DomainServices.ExpiryDate", "ExpiryDate", b2 =>
                                {
                                    b2.Property<Guid>("CardInformationPaymentModelPaymentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Month")
                                        .HasColumnType("int");

                                    b2.Property<int>("Year")
                                        .HasColumnType("int");

                                    b2.HasKey("CardInformationPaymentModelPaymentId");

                                    b2.ToTable("Payments");

                                    b2.WithOwner()
                                        .HasForeignKey("CardInformationPaymentModelPaymentId");
                                });

                            b1.Navigation("ExpiryDate");
                        });

                    b.OwnsOne("StudyHub.Payment.DomainServices.PaymentAmount", "Amount", b1 =>
                        {
                            b1.Property<Guid>("PaymentModelPaymentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("CurrencyCode")
                                .HasMaxLength(5)
                                .HasColumnType("nvarchar(5)");

                            b1.HasKey("PaymentModelPaymentId");

                            b1.ToTable("Payments");

                            b1.WithOwner()
                                .HasForeignKey("PaymentModelPaymentId");
                        });

                    b.Navigation("Amount");

                    b.Navigation("CreditCardInformation");
                });

            modelBuilder.Entity("StudyHub.Payment.DomainServices.PaymentOrder", b =>
                {
                    b.HasOne("StudyHub.Domain.Models.AccountUser", "AccountUser")
                        .WithMany()
                        .HasForeignKey("AccountUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountUser");
                });
#pragma warning restore 612, 618
        }
    }
}
