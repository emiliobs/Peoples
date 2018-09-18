namespace Peoples.Wackend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required(ErrorMessage ="You must enter a {0}")]
        [StringLength(30, ErrorMessage ="The field {0} can contain maximun {1} and minimum {2}", MinimumLength =1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(30, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2}", MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Image { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2}", MinimumLength = 7)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2}", MinimumLength = 7)]
        public string Phone { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }



    }
}