using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace LibraryCars.Models
{
    public class LibraryDataContext: DbContext
    {
        public LibraryDataContext(): base()

        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CarRent> CarRents { get; set; }
    }
}