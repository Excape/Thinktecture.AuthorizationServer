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
        public virtual int ID { get; set; }

        public virtual string OptionLabel { get; set; }
        public virtual int Value { get; set; }
    }
}
