using DTDOrganizer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTDOrganizer.Controllers
{
    //Handles the HTTP requests for the Resources module
    //Convention: If an action returns a View with no parameters(ex. return View()), 
    //the View's name is [action_name].cshtml under the ~/Views/Resources folder;
    [Authorize]
    public class ResourcesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Resources
        //Returns the main Resources View
        public ActionResult Index()
        {
            return View();
        }

        // POST: Resources/Office
        //Retrieves all the resources classified as Office resources in the Database and
        //returns a Partial View that renders these resources 
        [HttpPost]
        public ActionResult Office()
        {
            return PartialView("~/Views/Resources/_ResourcePartial.cshtml", db.AdminResources.Where(i => i.Type == ResourceType.Office)
                .Select(o => new ResourcesAdminPartialModel()
                {
                    Resource = o,
                    Orders = db.RequestResources.Where(i => i.ResourceName.Equals(o.Name) && !i.done).Select(p => p.Qty).DefaultIfEmpty(0).Sum()

                }
            ).ToList());
        }

        // POST: Resources/WorkMaterials
        //Retrieves all the resources classified as WorkMaterial resources in the Database and
        //returns a Partial View that renders these resources 
        [HttpPost]
        public ActionResult WorkMaterials()
        {
            return PartialView("~/Views/Resources/_ResourcePartial.cshtml", db.AdminResources.Where(i => i.Type == ResourceType.WorkMaterials).Select(o => new ResourcesAdminPartialModel()
            {
                Resource = o,
                Orders = db.RequestResources.Where(i => i.ResourceName.Equals(o.Name) && !i.done).Select(p => p.Qty).DefaultIfEmpty(0).Sum()

            }
            ).ToList());
        }

        // POST: Resources/Utilities
        //Retrieves all the resources classified as Utility resources in the Database and
        //returns a Partial View that renders these resources 
        [HttpPost]
        public ActionResult Utilities()
        {
            return PartialView("~/Views/Resources/_ResourcePartial.cshtml", db.AdminResources.Where(i => i.Type == ResourceType.Utilities).Select(o => new ResourcesAdminPartialModel()
            {
                Resource = o,
                Orders = db.RequestResources.Where(i => i.ResourceName.Equals(o.Name) && !i.done).Select(p => p.Qty).DefaultIfEmpty(0).Sum()

            }
            ).ToList());
        }

        // POST: Resources/Requests
        //Retrieves all the sent requests for resources in the Database and
        //returns a Partial View that renders these requests 
        [HttpPost]
        public ActionResult Requests()
        {
            return PartialView("~/Views/Resources/_RequestsPartial.cshtml", db.RequestResources);
        }

        // GET: Resources/RequestAnItem
        //Returns a View with a form for creating a new resource request.
        //It prepopulates some of the fields that are determined by the resource's attributes
        [HttpGet]
        public ActionResult RequestAnItem(int id) {
            ResourcesAdminModel model = db.AdminResources.Where(i => i.Id == id).FirstOrDefault();
            var viewModel = new ResourcesRequestViewModel()
            {
                id = model.Id,
                type = model.Type,
                ResourceName = model.Name,
                Qty = 0,
                Comment = "",
                Urgent = false
            };

            return View(viewModel);
        }

        // POST: Resources/RequestAnItem
        //Retrieves the data supplied in the form displayed by the RequestAnItem(int id) function and
        //tries to write the new resource request in the database.
        //Returns the aformentioned View with a form if bad informations are supplied. 
        //Finally it redirects to the Index action and subsequently returns the main Resources View if the database update is successful
        [HttpPost]
        public ActionResult RequestAnItem(ResourcesRequestViewModel model)
        {
            try
            {
                db.RequestResources.Add(new ResourcesRequestModel(model));
                db.SaveChanges();
                ViewBag.ResourceDbWriteFail = null;
                return RedirectToAction("Index");
            }
            catch {
                ViewBag.ResourceDbWriteFail = "Resource couldn't be added to the database, try again.";
                return View(model);
            }
        }

        // GET: Resources/ResourceOrdered
        //Changes the status of the Request to whether it's ordered or not
        public ActionResult ResourceOrdered(string id) {
            var resourceId = int.Parse(id);
            try
            {
                var resource = db.RequestResources.Where(r => r.Id == resourceId).FirstOrDefault();
                resource.done = !resource.done;
                db.SaveChanges();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return Json("success");
        }

        // GET: Resources/Create
        //Returns a View with a form for creating a new resource item
        public ActionResult CreateResource()
        {
            return View();
        }

        // POST: Resources/Create
        //Retrieves the data supplied in the form displayed by the CreateResource() function,
        //then it retrieves the image for the resource uploaded by the user, saves it and \
        //adds it's path to the resource item information.
        //Next, it tries to write the new resource item in the database.
        //Returns the aformentioned View with a form if bad informations are supplied. 
        //Finally it redirects to the Index action and subsequently returns the main Resources View if the database update is successful
        [HttpPost]
        public ActionResult CreateResource(ResourcesAdminViewModel model)
        {
            try
            {
                ResourcesAdminModel addResource = new ResourcesAdminModel
                {
                    Name = model.Name,
                    Description = model.Description,
                    Type = model.Type
                };
                if (model.image.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(model.image.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Images/Resources"), _FileName);
                    model.image.SaveAs(_path);
                    addResource.Image = "~/Content/Images/Resources/" + _FileName;
                }

                db.AdminResources.Add(addResource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Resources/Delete
        //Tries to find the resource item with the specific id supplied as a parameter and
        //if the resource item is valid it is removed from the database
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var resource = db.AdminResources.ToList().Find(r => r.Id == id);
                if (resource != null)
                {
                    db.AdminResources.Remove(resource);
                    db.SaveChanges();
                }
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
