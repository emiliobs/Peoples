namespace Peoples.Wackend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [NotMapped]
    public class ContactView : Contact
    {
        [Display(Name ="Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}