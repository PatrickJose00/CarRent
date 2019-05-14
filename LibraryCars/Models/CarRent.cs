using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryCars.Models
{
    public class CarRent
    {
        [Key]
        public int CarRentId { get; set; }


        // chaves estrangeiras, cada carrent só referencia um cliente e um carro

        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }


        public int CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        [NotMapped] // não aparece na BD, útil para DropDownList
        public List<Client> clients { get; set; }

        [NotMapped]
        public List<Car> cars { get; set; }

        public DateTime RegDate { get; set; }

        // auxiliares: para selecionar meses 

        [NotMapped]
        public List<Month> months { get; set; }

        [Required(ErrorMessage = "Please, select a Month")]
        [NotMapped]
        public int MonthNumber { get; set; }

        //um car rent contem mts carros
        //public virtual ICollection<Car> Cars { get; set; }

        //public virtual ICollection<Client> Clients { get; set; }
    }
}