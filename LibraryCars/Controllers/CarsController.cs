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
    public class CarsController : Controller
    {
        private LibraryDataContext db = new LibraryDataContext();

        // GET: Cars
        public ActionResult Index()
        {
            // Obter lista de carros não alugados
            var cars = db.Cars.Where(c => c.Rented == false).ToList();

            return View(cars);
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarId,Brand,Model,Price")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarId,Brand,Model,Price")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
        public ActionResult Rent(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);

            CarRent carRent = new CarRent();
            carRent.Car = car;
            carRent.CarId = car.CarId;

            carRent.clients = db.Clients.ToList();


            if (car == null)
            {
                return HttpNotFound();
            }

            return View(carRent);
        }

        [HttpPost]
        public ActionResult Rent(CarRent carRent)
        {
            carRent.clients = db.Clients.ToList(); // obter lista de clientes para mostrar na view

            if (ModelState.IsValid) // verificar se form é válido
            {
                // adicionar à BD
                carRent.RegDate = DateTime.Now;
                db.CarRents.Add(carRent);
                db.SaveChanges();


                // colocar carro como alugado
                Car car = db.Cars.Find(carRent.CarId);
                 car.Rented = true;

                 // Atualizar info. na BD
                 db.Cars.Attach(car);
                 var entry = db.Entry(car);
                 entry.Property(e => e.Rented).IsModified = true;
                 db.SaveChanges();

                return RedirectToAction("Index", "CarRents");
            }
            return View(carRent); // devolver view (em caso de form invalido)
        }
    }
}
