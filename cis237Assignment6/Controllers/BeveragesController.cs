using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cis237Assignment6.Models;

namespace cis237Assignment6.Controllers
{
    [Authorize]
    public class BeveragesController : Controller
    {
        private BeverageEntities db = new BeverageEntities();

        // GET: Beverages
        public ActionResult Index()
        {
            //----------------------------------------------------ADDED BY ME-------------------------------------------------------

            //SETUP A VARIABLE TO HOLD THE BEVERAGES DATA SET
            DbSet<Beverage> BeveragesToSearch = db.Beverages;

            //SETUP SOME STRING TO HOLD THE DATA THAT MIGHT BE IN THE SESSION.
            //IF THERE IS NOTHING IN THE SESSION WE CAN STILL USE THESE VARIABLES
            //AS A DEFAULT VALUE.
            string filterName = "";
            string filterPack = "";
            string filterMinPrice = "";
            string filterMaxPrice = "";
            string filterActive = "";

            decimal minPrice = 0m;
            decimal maxPrice = 100m;

            if (Session["name"] != null && String.IsNullOrWhiteSpace((string)Session["name"]))
            {
                filterName = (string)Session["name"];
            }

            if (Session["pack"] != null && String.IsNullOrWhiteSpace((string)Session["pack"]))
            {
                filterPack = (string)Session["pack"];
            }

            if (Session["minPrice"] != null && String.IsNullOrWhiteSpace((string)Session["minPrice"]))
            {
                filterMinPrice = (string)Session["minPrice"];
                minPrice = Decimal.Parse(filterMinPrice);

            }

            if (Session["maxPrice"] != null && String.IsNullOrWhiteSpace((string)Session["maxPrice"]))
            {
                filterMaxPrice = (string)Session["maxPrice"];
                maxPrice = Decimal.Parse(filterMaxPrice);
            }

            if (Session["active"] != null && String.IsNullOrWhiteSpace((string)Session["active"]))
            {
                filterActive = (string)Session["active"];
            }

            //FILTER THE BEVERAGESTOSEARCH DATASET. USE THE WHERE THAT WE USED BEFORE WHEN DOING EF WORK,
            //ONLY THIS TIME SEND IN MORE LAMBDA EXPRESSIONS TO NARROW IT DOWN FURTHER.
            IEnumerable<Beverage> filtered = BeveragesToSearch.Where(beverage => beverage.price >= minPrice &&
                                                                                 beverage.price <= maxPrice &&
                                                                                 beverage.name.Contains(filterName));

            //CONVERT THE DATABASE SET TO A LIST NOW THAT THE QUERY WORK IS DONE ON IT.
            IEnumerable<Beverage> finalFiltered = filtered.ToList();

            //PLACE THE STRING REPRESENTATION OF THE VALUES IN THE SESSION INTO THE VIEWBAG.
            ViewBag.filterName = filterName;
            ViewBag.filterPack = filterPack;
            ViewBag.filterMinPrice = filterMinPrice;
            ViewBag.filterMaxPrice = filterMaxPrice;
            ViewBag.filterActive = filterActive;

            //RETURN THE VIEW WITH A FILTERED SELECTION OF BEVERAGES
            return View(finalFiltered);

            //-------------------------------------------------------ADDED BY ME END  ---------------------------------------------------

        }

        // GET: Beverages/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // GET: Beverages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beverages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,pack,price,active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                db.Beverages.Add(beverage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beverage);
        }

        // GET: Beverages/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: Beverages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,pack,price,active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beverage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beverage);
        }

        // GET: Beverages/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: Beverages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Beverage beverage = db.Beverages.Find(id);
            db.Beverages.Remove(beverage);
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

//----------------------------------------------------------------ADDED BY ME------------------------------------------------------------------
        [HttpPost, ActionName("Filter")]
        [ValidateAntiForgeryToken]
        public ActionResult Filter()
        {
            //GET THE FORM DATA THAT AS SENT OUT OF THE REQUEST OBJECT.
            //THE STRING THAT IS USED AS A KEY TO GET THE DATA MATCHES THE
            //PROPERTY OF THE FORM CONTROL
            String name = Request.Form.Get("name");
            String pack = Request.Form.Get("pack");
            String minPrice = Request.Form.Get("minPrice");
            String maxPrice = Request.Form.Get("maxPrice");
            String active = Request.Form.Get("active");

            //STORE THE FORM DATA INTO THE SESSION SO THAT IT CAN BE RETRIEVED 
            //LATER TO FILTER THE DATA
            Session["name"] = name;
            Session["pack"] = pack;
            Session["minPrice"] = minPrice;
            Session["maxPrice"] = maxPrice;
            Session["active"] = active;

            //REDIRECT TO THE INDEX
            return RedirectToAction("Index");
        }
    }
}
