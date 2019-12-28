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
    //Convention: If an action returns a View with no parameters(ex. return View()), 
    //the View's name is [action_name].cshtml under the ~/Views/Food folder;
    [Authorize]
    public class FoodController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Food
        //Returns the restaurants and daily orders from the Food controller and
        //displays the food orders for the current day and all the restaurants from the Database 
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
        //tries to write the new order request in the database.
        //Returns the aformentioned View with a form if bad informations are supplied. 
        //Finally it redirects to the Index action and subsequently returns the main Food View if the database update is successful
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
        //Returns a View with a form for creating a new restaurant
        public ActionResult AddRestaurant()
        {

            return View();
        }

        // POST: Food/AddRestaurant
        //Retrieves the data supplied in the form displayed by the AddRestaurant() function,
        //then it retrieves the image for the resource uploaded by the user, saves it and \
        //adds it's path to the restaurant  information.
        //Next, it tries to write the new resource item in the database.
        //Returns the aformentioned View with a form if bad informations are supplied. 
        //Finally it redirects to the Index action and subsequently returns the main Food View if the database update is successful

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
        //Tries to find the food item with the specific id supplied as a parameter and
        //if the food item is valid it is removed from the database
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