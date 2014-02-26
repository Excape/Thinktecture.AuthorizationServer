using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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