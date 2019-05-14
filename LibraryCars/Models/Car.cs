using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryCars.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Display(Name = " Car Brand")]
        public string Brand { get; set; }

        [Display(Name = " Car Model")]
        public string Model { get; set; }

        [NotMapped]
        public string BrandModel
        {
            get
            {
                return Brand + " " + Model;
            }
        }

        [Display(Name = " Price")]
        public int Price { get; set; }

        public bool Rented { get; set; }

    }
}