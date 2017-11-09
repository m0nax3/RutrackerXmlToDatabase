using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RutrackerImport
{
    public class Program
    {
        private static Stopwatch timer = Stopwatch.StartNew();

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"ConnectionStrings.json", false, false)
                .Build();


            var builder = new DbContextOptionsBuilder();

            var provider = configuration.GetSection("ConnectionStrings")["Provider"];
            var connection = configuration.GetSection("ConnectionStrings")["Connection"];

            switch (provider)
            {
                case "SqlServer":
                    builder.UseSqlServer(connection);
                    break;
                case "Sqlite":
                    builder.UseSqlite(connection);
                    break;
                default:
                    throw new Exception("No database provider specified");
            }

            Console.WriteLine("input full path to rutracker xml and press enter:");

            string sourcePath =
                Console.ReadLine();
                //@"D:\PtpDownload\backup.20170916154625.xml";
            if (!File.Exists(sourcePath))
            {
                Console.WriteLine("File not found");
                return;
            }

            using (var context = new RutrackerContext(builder.Options))
            {
                Console.WriteLine("Check database schema");
                context.Database.Migrate();

                Console.WriteLine("TRUNCATE table");

                var rawSqlString = new RawSqlString($"TRUNCATE TABLE {nameof(context.Torrents)}");
                context.Database.ExecuteSqlCommand(rawSqlString);

                Console.WriteLine("work");

                using (var loader = new SqlBulkCopy(connection))
                {
                    loader.DestinationTableName = nameof(context.Torrents);

                    FillMapping(loader);

                    using (
                        var reader =
                            new XmlDataReader(sourcePath))
                    {
                        var size = 8 * 1024;

                        loader.BatchSize = size;
                        loader.BulkCopyTimeout = int.MaxValue;
                        loader.EnableStreaming = true;
                        loader.NotifyAfter = size / 2;
                        loader.SqlRowsCopied += Loader_SqlRowsCopied;
                        
                        timer = Stopwatch.StartNew();
                        Console.Clear();
                        loader.WriteToServer(reader);
                    }
                }
            }
        }

        private static void FillMapping(SqlBulkCopy loader)
        {
            var sample = new TorrentRow();

            loader.ColumnMappings.Add(nameof(sample.Id), 0);
            loader.ColumnMappings.Add(nameof(sample.Date), 1);
            loader.ColumnMappings.Add(nameof(sample.Size), 2);
            loader.ColumnMappings.Add(nameof(sample.Title), 3);
            loader.ColumnMappings.Add(nameof(sample.ForumId), 4);
            loader.ColumnMappings.Add(nameof(sample.ForumTitle),5);
            loader.ColumnMappings.Add(nameof(sample.Hash), 7);
            loader.ColumnMappings.Add(nameof(sample.TrackerId), 8);
            loader.ColumnMappings.Add(nameof(sample.Content), 6);
        }

        private static void Loader_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            Console.Write("Imported: "+e.RowsCopied + ", speed: " + (int)(e.RowsCopied / timer.Elapsed.TotalSeconds) + " rows/sec");
            Console.SetCursorPosition(0, 0);
        }
    }
}