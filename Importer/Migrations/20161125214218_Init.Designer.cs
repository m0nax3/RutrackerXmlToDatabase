using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RutrackerImport.Migrations
{
    [DbContext(typeof(RutrackerContext))]
    [Migration("20161125214218_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetCoreApp.TorrentRow", b =>
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
                        .HasMaxLength(1024);

                    b.Property<string>("Url")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Torrents");
                });
        }
    }
}
