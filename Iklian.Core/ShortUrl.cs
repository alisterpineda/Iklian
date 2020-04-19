using System;
using System.ComponentModel.DataAnnotations;

namespace Iklian.Core
{
    public class ShortUrl
    {
        [Key]
        [StringLength(255)]
        public string Hash { get; set; }

        [Url]
        [StringLength(1000)]
        public string Url { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
