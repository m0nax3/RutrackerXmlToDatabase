using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RutrackerImport
{
    public class TorrentRow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public long Size { get; set; }

        [StringLength(512)]
        public string Title { get; set; }

        public int ForumId { get; set; }

        [StringLength(512)]
        public string ForumTitle { get; set; }

        [StringLength(80)]
        public string Hash { get; set; }

        public string TrackerId { get; set; }

        public string Content { get; set; }
    }
}