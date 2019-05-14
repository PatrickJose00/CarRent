using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryCars.Models
{
    public class Client
    {

        [Key]
        public int ClientId { get; set; }

        [Display(Name = " FirstName")]
        public string FirstName { get; set; }

        [Display(Name = " LastName")]

        public string LastName { get; set; }

        [NotMapped]
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = " Address")]
        public string Address { get; set; }

        //public virtual ICollection<CarRent> CarRents { get; set; }      

    }
}