using Project_Exam.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Exam.Controllers
{
    public class BreedsController : Controller
    {
        readonly CatsDbContext db = new CatsDbContext(); 
        // GET: Breeds
        public ActionResult Index(int page= 1)
        {             
            ViewBag.totalpages = (int)Math.Ceiling((double)db.Breeds.Count() / 5);
            ViewBag.CurrentPage = page;
            var data = db.Breeds
                        .OrderBy(x => x.BreedId)
                        .Skip((page - 1) * 5)
                        .Take(5)                       
                        .ToList();

            return View(data);
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(Breed b)
        {
            if (ModelState.IsValid)
            {
                db.Breeds.Add(b);
                db.SaveChanges();
                return PartialView("_PartialMgs", true);
            }
            return PartialView("_PartialMgs", false);
           
        }

        public ActionResult Edit(int id)
        {
            return View(db.Breeds.First(x => x.BreedId == id));
        }

        [HttpPost]

        public ActionResult Edit(Breed b)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b);
        }

        public ActionResult Delete(int id)
        {
            return View(db.Breeds.First(x => x.BreedId == id));
        }

        [HttpPost, ActionName("Delete")]
       
        public ActionResult ConfirmDelete(int id)
        {
            Breed b = new Breed { BreedId = id };
            
            if(!db.Cats.Any(x=> x.BreedId == id))
            {
                db.Entry(b).State = EntityState.Deleted;
                db.SaveChanges();
                return PartialView("_DeleteMgs",true);
            }
            return PartialView("_DeleteMgs",false);

        }

        public ActionResult info(int id)
        {
            return View(db.Breeds.First(x => x.BreedId == id));
        }

        [HttpPost, ActionName("info")]

        public ActionResult FullInfo(int id)
        {
            Breed b = new Breed { BreedId = id };
            return View(b);
        }
    }
}