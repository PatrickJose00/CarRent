using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryCars.Models
{
    // classe auxiliar para permitir selecionar os meses na view 
    public class Month
    {
        public int Number { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Return all months
        /// </summary>
        public static List<Month> getMonths() // devolve os meses 
        {
            List<Month> Months = new List<Month>();

            Months.Add(new Month { Name = "January", Number = 1 });
            Months.Add(new Month { Name = "February", Number = 2 });
            Months.Add(new Month { Name = "March", Number = 3 });
            Months.Add(new Month { Name = "April", Number = 4 });
            Months.Add(new Month { Name = "May", Number = 5 });
            Months.Add(new Month { Name = "June", Number = 6 });
            Months.Add(new Month { Name = "July", Number = 7 });
            Months.Add(new Month { Name = "August", Number = 8 });
            Months.Add(new Month { Name = "September", Number = 9 });
            Months.Add(new Month { Name = "October", Number = 10 });
            Months.Add(new Month { Name = "November", Number = 11 });
            Months.Add(new Month { Name = "December", Number = 12 });

            return Months;
        }
    }
}