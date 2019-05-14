using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryCars.Models;

namespace LibraryCars.Controllers
{
    public class CarRentsController : Controller
    {
        private LibraryDataContext db = new LibraryDataContext();

        // GET: CarRents
        public ActionResult Index()
        {
            return View(db.CarRents.ToList());
        }

        // GET: CarRents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarRent carRent = db.CarRents.Include(cr => cr.Car).Include(cr => cr.Client).SingleOrDefault(cr => cr.CarRentId == id);
            if (carRent == null)
            {
                return HttpNotFound();
            }
            return View(carRent);
        }

        // GET: CarRents/Create
        public ActionResult Create()
        {
            CarRent carRent = new CarRent();
            // iniciar listas com dados da base de dados, para os carros são mostrados apenas os não alugados
            carRent.cars = db.Cars.Where(c => c.Rented == false).ToList();
            carRent.clients = db.Clients.ToList();

            return View(carRent);
        }

        // POST: CarRents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarRentId,CarId,ClientId")] CarRent carRent)
        {
            // iniciar listas com dados da base de dados, para os carros são mostrados apenas os não alugados
            carRent.cars = db.Cars.Where(c => c.Rented == false).ToList();
            carRent.clients = db.Clients.ToList();

            if (ModelState.IsValid)
            {
                carRent.RegDate = DateTime.Now;

                // adicionar carrent à BD
                db.CarRents.Add(carRent);
                db.SaveChanges();

                // atualizar carro: colocar rented = true
                var car = carRent.Car;
                car.Rented = true;
                db.Cars.Attach(car);
                var carEntry = db.Entry(car);
                carEntry.Property(e => e.Rented).IsModified = true;
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            return View(carRent);
        }

        // GET: CarRents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarRent carRent = db.CarRents.Include(cr => cr.Car).Include(cr => cr.Client).SingleOrDefault(cr => cr.CarRentId == id);
            if (carRent == null)
            {
                return HttpNotFound();
            }
            return View(carRent);
        }

        // POST: CarRents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(CarRent carRent)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Attach(carRent.Car);
                var carEntry = db.Entry(carRent.Car);
                carEntry.Property(e => e.Brand).IsModified = true;
                carEntry.Property(e => e.Model).IsModified = true;
                carEntry.Property(e => e.Price).IsModified = true;
                db.SaveChanges();


                db.Clients.Attach(carRent.Client);
                var clientEntry = db.Entry(carRent.Client);
                clientEntry.Property(e => e.FirstName).IsModified = true;
                clientEntry.Property(e => e.LastName).IsModified = true;
                clientEntry.Property(e => e.Address).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(carRent);
        }

        // GET: CarRents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarRent carRent = db.CarRents.Find(id);
            if (carRent == null)
            {
                return HttpNotFound();
            }
            return View(carRent);
        }

        // POST: CarRents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarRent carRent = db.CarRents.Include(cr => cr.Car).SingleOrDefault(cr => cr.CarRentId == id);
            carRent.Car.Rented = false;
            db.CarRents.Remove(carRent);
            db.SaveChanges();
            return RedirectToAction("Index", "cars");
        }

        // get da pagina rents by month, devolve a view com a lista de meses definida 
        public ActionResult RentsByMonth()
        {
            CarRent carRent = new CarRent();

            carRent.months = Month.getMonths(); // obtem meses para a lista de meses

            return View(carRent);
        }

        // POST que recebe o carRent  com o número de mês selecionad
        [HttpPost]
        public ActionResult RentsByMonth(CarRent carRent)
        {
            carRent.months = Month.getMonths();
            
            if(ModelState.IsValid) // verifica se forumulario é valdio : se o mês foi selecionado
            {
                List<CarRent> list = db.CarRents.Where(c => c.RegDate.Month == carRent.MonthNumber).ToList();

                ViewBag.total = list.Count; // obtém lista de carros alugados no numero do mes selecionado. guarda o total na viewbag


                double priceSum = 0;
                // iterar coleção
                foreach (var item in list)
                {
                    priceSum += item.Car.Price;
                }

                ViewBag.priceSum = priceSum;           
               
            }

            return View(carRent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
