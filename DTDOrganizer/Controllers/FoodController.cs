using DTDOrganizer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTDOrganizer.Controllers
{
    //Handles the HTTP requests for the Food module
    [Authorize]
    public class FoodController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Food
        public ActionResult Index()
        {
            FoodViewModel model = new FoodViewModel
            {
                restaurants = db.RestaurantModels.ToList(),
                dailyOrders = db.OrdersModels.ToList()
                                .Where(o => o.orderDate.Equals(DateTime.Now.Date.ToString("dd/MM/yyyy")))
                                .ToList()
            };
            return View(model);
        }

        // POST: Food/PlaceOrder
        [HttpPost]
        public ActionResult PlaceOrder(string order, string restaurant)
        {
            OrdersModel newOrder = new OrdersModel
            {
                order = order,
                restaurantName = restaurant,
                employee = User.Identity.Name, 
                orderDate = DateTime.Now.Date.ToString("dd/MM/yyyy")
            };
            try
            {
                db.OrdersModels.Add(newOrder);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Json(new { redirectToUrl = Url.Action("Index", "Food") });
        }
        // GET: Food/AddRestaurant
        public ActionResult AddRestaurant()
        {

            return View();
        }

        // POST: Food/AddRestaurant
        [HttpPost]
        public ActionResult AddRestaurant(RestaurantViewModel model)
        {
            try
            {
                RestaurantModel addRestaurant = new RestaurantModel
                {
                    name = model.name,
                    priceRange = model.priceRange
                };
                if (model.menuImage.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(model.menuImage.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Images/Restaurants"), _FileName);
                    model.menuImage.SaveAs(_path);
                    addRestaurant.menuImage = "~/Content/Images/Restaurants/" + _FileName;
                }

                db.RestaurantModels.Add(addRestaurant);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Food/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var restaurant = db.RestaurantModels.ToList().Find(r => r.id == id);
                if(restaurant != null) db.RestaurantModels.Remove(restaurant);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Disposes of the database instance so we can be certain that the database resource is released
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}