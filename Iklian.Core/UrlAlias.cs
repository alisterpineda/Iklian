using System;
using System.ComponentModel.DataAnnotations;

namespace Iklian.Core
{
    public class UrlAlias
    {
        [Key]
        public int Id { get; set; }

        [Url]
        [StringLength(1000)]
        public string Url { get; set; }

        [StringLength(255)]
        public string Alias { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
