using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    //Model that represents the data that is stored in the database for a resource item request
    public class ResourcesRequestModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        //Whether the Resource item that is requested is an Office item, a WorkMaterial item or an Utility item
        public ResourceType type { get; set; }
        //Name of the Resource item that is requested
        [DisplayName("Name of resource")]
        public string ResourceName { get; set; }
        //How much resource items are requested
        [DisplayName("Quantity")]
        [Range(1, 100, ErrorMessage = "Only quantities between 1 and 100 are allowed")]
        public int Qty { get; set; }
        //Some comment about the request
        [StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters.")]
        public string Comment { get; set; }
        //Whether the delivery of requested item is urgent for effiecient workflow
        public bool Urgent { get; set; }
        //Has the item been delivered to the requestee
        public bool done { get; set; }
        //The date that the item has been ordered on in the format [year]-[month]-[day]
        public string orderDate { get; set; }

        public ResourcesRequestModel() { }

        public ResourcesRequestModel(ResourcesRequestViewModel viewModel) {
            type = viewModel.type;
            ResourceName = viewModel.ResourceName;
            Qty = viewModel.Qty;
            Comment = viewModel.Comment;
            Urgent = viewModel.Urgent;
            done = false;
            orderDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
        }
    }
}
