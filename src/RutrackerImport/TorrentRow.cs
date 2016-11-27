using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreApp
{
    public class TorrentRow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        public DateTime Date { get; set; }

        [Column(Order = 2)]
        public long Size { get; set; }

        [Column(Order = 3)]
        [StringLength(255)]
        public string Title { get; set; }

        [Column(Order = 4)]
        public int ForumId { get; set; }

        [Column(Order = 5)]
        [StringLength(512)]
        public string ForumTitle { get; set; }

        [Column(Order = 6)]
        [StringLength(512)]
        public string Magnet { get; set; }

        [Column(Order = 7)]
        [StringLength(255)]
        public string Url { get; set; }

        [Column(Order = 8)]
        public string Content { get; set; }
    }
}