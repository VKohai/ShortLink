﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShortLink.Api.Data;

#nullable disable

namespace shortlink.api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240903163740_Renaming")]
    partial class Renaming
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ShortLink.Api.Models.UrlMapper", b =>
                {
                    b.Property<string>("Guid")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<uint>("Views")
                        .HasColumnType("int unsigned");

                    b.HasKey("Guid");

                    b.ToTable("UrlMappers");
                });
#pragma warning restore 612, 618
        }
    }
}
