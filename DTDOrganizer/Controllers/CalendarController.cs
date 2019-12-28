using DTDOrganizer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTDOrganizer.Controllers
{
    //Handles the HTTP requests for the Calendar module;
    //Convention: If an action returns a View with no parameters(ex. return View()), 
    //the View's name is [action_name].cshtml under the ~/Views/Calendar folder;
    [Authorize]
    public class CalendarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Calendar
        //Returns the main calendar from the Calendar controller and
        //displays the number of events during the present day
        public ActionResult Index()
        {
            List<CalendarEventModel> dailyEvents = db.CalendarEventModels.ToList().FindAll(e => e.start.StartsWith(DateTime.Now.Date.ToString("yyyy-MM-dd")));
            ViewBag.data = dailyEvents;
            return View();
        }

        // POST: Calendar/GetCalendarData
        //Retrieves all the calendar events in the Database and
        //returns them in Json format for the calendar component in the View
        [HttpPost]
        public ActionResult GetCalendarData()
        {
            var model = db.CalendarEventModels.ToList();
            return Json(model);

        }

        // GET: Calendar/AddEvent
        //Returns View with a form for creating a new Calendar Event
        public ActionResult AddEvent()
        {
            return View();
        }

        // POST: Calendar/AddEvent
        //Retrieves the data supplied in the form displayed by the AddEvent() function and
        //tries to write the new Calendar Event in the database.
        //Returns the aformentioned View with a form if bad informations are supplied. 
        //Finally it redirects to the Index action and subsequently returns the main calendar View if the database update is successful
        [HttpPost]
        public ActionResult AddEvent(CalendarEventViewModel newEvent)
        {
            try
            {
                CalendarEventModel calendarEvent = new CalendarEventModel
                {
                    title = newEvent.title,
                    color = Enum.GetName(typeof(eventColor), newEvent.color),
                    description = newEvent.description,
                    start = newEvent.startDate.Date.ToString("yyyy-MM-dd") + "T" + newEvent.startTime + ":00",
                    allDay = newEvent.allDay
                };
                if (!calendarEvent.allDay) {
                    calendarEvent.end = newEvent.endDate + "T" + newEvent.endTime + ":00";
                }
                else {
                    calendarEvent.end = null;
                }
                try
                {
                    db.CalendarEventModels.Add(calendarEvent);
                    db.SaveChanges();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }

        // POST: Calendar/DeleteEvent
        //Tries to find the Calendar Event with the specific id supplied as a parameter and
        //if the Calendar Event is valid it is removed from the database
        [HttpPost]
        public ActionResult DeleteEvent(string id)
        {
            var calendarEvent = db.CalendarEventModels.ToList().FirstOrDefault(e => e.id == int.Parse(id));
            if (calendarEvent != null) {
                try
                {
                    db.CalendarEventModels.Remove(calendarEvent);
                    db.SaveChanges();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            return Json("success");
        }

        // POST: Calendar/EditEvent
        //Retrieves an existing Calendar Event as a CalendarEventModel.
        //Tries to find the specified Calendar Event in the database.
        //If the Calendar Event is valid the existing record is updated with the new information supplied in the model about
        //whether an Event is a full day event, when does it start and when does it end.
        //Finally it redirects to the Index action and subsequently returns the main calendar View 
        [HttpPost]
        public ActionResult EditEvent(CalendarEventModel newEvent)
        {
            var calendarEvent = db.CalendarEventModels.ToList().FirstOrDefault(e => e.id == newEvent.id);
            if (calendarEvent != null)
            {
                calendarEvent.allDay = newEvent.allDay;
                calendarEvent.start = newEvent.start;
                calendarEvent.end = newEvent.end;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return RedirectToAction("Index");
        }

        //Disposes of the database instance so we can be certain that the database resource is released
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}