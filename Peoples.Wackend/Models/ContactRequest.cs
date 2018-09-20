namespace Peoples.Wackend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [NotMapped]
    public class ContactRequest : Contact
    {
        public byte[] ImageArray { get; set; }
    }
}