using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    //A view model that is used to represent fields in a form, validate and gather data for a Calendar Event and 
    //pass that data to a CalendarEventModel so it can be stored in a database;
    //For fields meaning check the CalendarEventModel model
    public class CalendarEventViewModel
    {
        public string title { get; set; }
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string startTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string endTime { get; set; }
        public eventColor color { get; set; }
        [MaxLength(150)]
        public string description { get; set; }
        public bool allDay { get; set; }
        public calendarEventType eventType { get; set; }
    } 
}