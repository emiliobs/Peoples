namespace PeoplesMobile.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Contact
    {
       
        public int ContactId { get; set; }

        public string FirstName { get; set; }

       
        public string LastName { get; set; }

        public string Image { get; set; }

      
        public string Email { get; set; }

      
        public string Phone { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(Image))
                {
                    return "nouser.png";
                }

               // return string.Format("http://192.168.0.11:56064{0}", Image.Substring(1));
               return $"http://192.168.0.11:56064{Image.Substring(1)}";
            }
        }

        public override int GetHashCode()
        {
            return ContactId;
        }
    }
}
