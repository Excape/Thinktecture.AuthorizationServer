using System;
using System.ComponentModel.DataAnnotations;

namespace Thinktecture.AuthorizationServer.WebHost.Areas.Admin.Models
{
    public class RememberOptionModel
    {
        [Required]
        [Range(-1, Int32.MaxValue)]
        public int OptionSelect { get; set; }
        [Range(0, 1000)]
        public int? UserValue { get; set; }
    }
}