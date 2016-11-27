using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NetCoreApp.Migrations
{
    [DbContext(typeof(RutrackerContext))]
    internal class RutrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetCoreApp.TorrentRow", b =>
            {
                b.Property<int>("Id");
                b.Property<DateTime>("Date");
                b.Property<long>("Size");
                b.Property<string>("Title").HasMaxLength(255);
                b.Property<int>("ForumId");
                b.Property<int>("ForumTitle").HasMaxLength(255);
                b.Property<int>("Magnet").HasMaxLength(255);
                b.Property<int>("Url").HasMaxLength(255);
                b.Property<string>("Content");
                b.HasKey("Id");
                b.ToTable("Torrents");
            });
        }
    }
}