using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    //A view model that is used to represent fields in a form, validate and gather data for a resource item and 
    //pass that data to a ResourcesAdminModel so it can be stored in a database;
    //For fields meaning check the ResourcesAdminModel model
    public class ResourcesAdminViewModel
    {
        public ResourceType Type { get; set; }

        public String Name { get; set; }

        [StringLength(150, ErrorMessage = "Description cannot be longer than 150 characters.")]
        public String Description { get; set; }

        //A field that allows image upload on the user side
        [Required(ErrorMessage = "image required")]
        public HttpPostedFileBase image { get; set; }
    }
}