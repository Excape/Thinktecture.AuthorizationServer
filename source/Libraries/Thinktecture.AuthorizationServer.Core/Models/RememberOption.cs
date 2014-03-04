using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thinktecture.AuthorizationServer.Models
{
    public class RememberOption
    {
        [Key]
        public int ID { get; set; }

        public string OptionLabel { get; set; }
        public int Value { get; set; }
    }
}
