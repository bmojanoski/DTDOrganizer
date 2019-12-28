using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    //A view model that is used to represent fields in a form, validate and gather data for a resource item request and 
    //pass that data to a ResourcesRequestModel so it can be stored in a database;
    //For fields meaning check the ResourcesRequestModel model
    public class ResourcesRequestViewModel
    {
        public int id { get; set; }
        public ResourceType type { get; set; }

        [DisplayName("Name of resource")]
        public string ResourceName { get; set; }

        [DisplayName("Quantity")]
        [Range(1, 100, ErrorMessage = "Only quantities between 1 and 100 are allowed")]
        public int Qty { get; set; }
        [StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters.")]
        public string Comment { get; set; }

        public bool Urgent { get; set; }
    }
}