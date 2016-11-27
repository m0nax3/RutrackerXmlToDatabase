using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RutrackerImport
{
    public class RutrackerContext : DbContext
    {
        public RutrackerContext()
        {
        }

        public RutrackerContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<TorrentRow> Torrents { get; set; }

#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"ConnectionStrings.json", false, false)
                .Build();

            var connection = configuration.GetSection("ConnectionStrings")["Connection"];

            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }
#endif
    }
}