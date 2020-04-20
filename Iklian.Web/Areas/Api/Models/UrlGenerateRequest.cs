using System.ComponentModel.DataAnnotations;

namespace Iklian.Web.Areas.Api.Models
{
    public class UrlGenerateRequest
    {
        [Url]
        [StringLength(1000)]
        public string Url { get; set; }
    }
}
