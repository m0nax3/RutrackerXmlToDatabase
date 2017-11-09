using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RutrackerImport;

namespace RutrackerImport.Migrations
{
    [DbContext(typeof(RutrackerContext))]
    [Migration("20170506100408_UrlColumnRemoved")]
    partial class UrlColumnRemoved
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RutrackerImport.TorrentRow", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Content");

                    b.Property<DateTime>("Date");

                    b.Property<int>("ForumId");

                    b.Property<string>("ForumTitle")
                        .HasMaxLength(512);

                    b.Property<string>("Magnet")
                        .HasMaxLength(512);

                    b.Property<long>("Size");

                    b.Property<string>("Title")
                        .HasMaxLength(512);

                    b.HasKey("Id");

                    b.ToTable("Torrents");
                });
        }
    }
}
