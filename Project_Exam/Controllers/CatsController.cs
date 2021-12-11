using Project_Exam.Models;
using Project_Exam.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Exam.Controllers
{
    public class CatsController : Controller
    {

        readonly CatsDbContext db = new CatsDbContext();
        // GET: Cats

        public ActionResult Index()
        {
            return View(db.Cats.Include(x=> x.Breed).ToList());
        }
        public ActionResult IndexPar(int page = 1)
        {            
            ViewBag.totalpages = Math.Ceiling((double)db.Cats.Count() / 5.0);
            ViewBag.CurrentPage = page;
            var data = db.Cats
                        .OrderBy(x => x.CatId)
                        .Skip((page - 1) * 5)
                        .Take(5)
                        .Include(x => x.Breed)
                        .ToList();
           
            return PartialView("_IndexPartial", data);
            
        }
        public ActionResult Create()
        {
            ViewBag.Breed = db.Breeds.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CatCreateViewModel cr)
        {
            if (ModelState.IsValid)
            {
                Cat c = new Cat
                {
                    CatName = cr.CatName,
                    Dob = cr.Dob,
                    Gender = cr.Gender,
                    Available = cr.Available,
                    BreedId = cr.BreedId,
                    Picture = "1.jpg"
                };

                if(cr.Picture != null && cr.Picture.ContentLength > 0)
                {
                    string filepath = Server.MapPath("~/Uploads/");
                    string filename = Guid.NewGuid() + Path.GetExtension(cr.Picture.FileName);
                    cr.Picture.SaveAs(Path.Combine(filepath, filename));
                    c.Picture = filename;
                }
                db.Cats.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Breed = db.Breeds.ToList();
            return View(cr);
        }


        public ActionResult Edit(int id)
        {
            
            var data = db.Cats.First(x => x.CatId == id);

            ViewBag.Breed = db.Breeds.ToList();
            ViewBag.CurrentPic = data.Picture;
            return View(new CatEditViewModel
            {
                CatId = data.CatId,
                CatName = data.CatName,
                Dob= data.Dob,
                Gender = data.Gender,
                Available = data.Available,
                BreedId = data.BreedId                
            });
        }

        [HttpPost]

        public ActionResult Edit(CatEditViewModel ce)
        {
            Cat c = db.Cats.First(x => x.CatId == ce.CatId);

            if (ModelState.IsValid)
            {
                c.CatName = ce.CatName;
                c.Dob = ce.Dob;
                c.Gender = ce.Gender;
                c.BreedId = ce.BreedId;
                c.Available = ce.Available;

                if (ce.Picture != null && ce.Picture.ContentLength > 0)
                {
                    string filepath = Server.MapPath("~/Uploads/");
                    string filename = Guid.NewGuid() + Path.GetExtension(ce.Picture.FileName);
                    ce.Picture.SaveAs(Path.Combine(filepath, filename));
                    c.Picture = filename;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var p = db.Cats.First(x => x.CatId == ce.CatId);
            ViewBag.Breed = db.Breeds.ToList();
            ViewBag.CurrentPic = c.Picture;
            return View(ce);
        }

        public ActionResult Delete(int id)
        {
            return View(db.Cats.Include(x => x.Breed).First(x=> x.CatId == id));
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult ConfirmDelete(int id)
        {
            Cat c = new Cat { CatId = id };
            db.Entry(c).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult info(int id)
        {
            return View(db.Cats.First(x => x.CatId == id));
        }

        [HttpPost, ActionName("info")]

        public ActionResult FullInfo(int id)
        {
            Cat c = new Cat { CatId = id };
            return View(c);
        }
    }
}