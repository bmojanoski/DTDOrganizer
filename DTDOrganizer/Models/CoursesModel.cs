using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    public class CoursesModel
    {   
        [Key]
        public int id { get; set; }
        [DisplayName("Course name")]
        public string name { get; set; }
        [DisplayName("Course link")]
        public string link { get; set; }
    }
}