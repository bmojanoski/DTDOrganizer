using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    //Model that represents the data that is stored in the database for a Calendar Event
    public class CalendarEventModel
    {
        [Key]
        public int id { get; set; }
        //Title of the Calendar Event
        [MaxLength(50)]
        public string title { get; set; }
        //Date and time string representing the start of the event;
        //it's stored in the format [year]-[month]-[day]T[hours]:[minutes]:[seconds].[milliseconds]Z
        public string start { get; set; }
        //Date and time string representing the end of the event;
        //it's stored in the format [year]-[month]-[day]T[hours]:[minutes]:[seconds].[milliseconds]Z;
        //It can be NULL for a full day event
        public string end { get; set; }
        //The color with which the event is represented on the Calendar in the Calendar module
        public string color { get; set; }
        //Description of the Calendar Event
        [MaxLength(150)]
        public string description { get; set; }
        //Whether the Calendar Event lasts all day
        public bool allDay { get; set; }
        //Whether the Calendar Event is a Birthday Event, a meeting or some other type of event
        public calendarEventType eventType { get; set; }
    }

    //Enumerator with available colors for a Calendar Event
    public enum eventColor
    {
        Red, Blue, Aqua, BlueViolet, BurlyWood, DarkOrange, DodgerBlue, Gold, Green, LightGreen, Silver, Yellow
    }

    //Enumerator with available types for a Calendar Event
    public enum calendarEventType { 
        Other, Meeting, Birthday
    }
}