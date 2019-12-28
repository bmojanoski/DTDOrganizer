using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    public class CoursesModel
    {   
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }
}