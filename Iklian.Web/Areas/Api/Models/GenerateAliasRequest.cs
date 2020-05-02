using System.ComponentModel.DataAnnotations;

namespace Iklian.Web.Areas.Api.Models
{
    public class GenerateAliasRequest
    {
        [Required]
        [Url]
        [StringLength(1000)]
        public string Url { get; set; }
    }
}
