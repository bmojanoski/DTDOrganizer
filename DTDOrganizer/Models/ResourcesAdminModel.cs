using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    //Model that represents the data that is stored in the database for a resource item
    public class ResourcesAdminModel
    {
        [Key]
        public int Id { get; set; }
        //Whether the Resource item is an Office item, a WorkMaterial item or an Utility item
        public ResourceType Type { get; set; }
        //Name of the Resource item
        public String Name { get; set; }
        [StringLength(150, ErrorMessage = "Description cannot be longer than 150 characters.")]
        //Description of the Resource item
        public String Description { get; set; }
        //The path to the image of the resource item
        public string Image { get; set; }
    }

    //Enumerator that specifies the available types of Resource items
    public enum ResourceType
    {
        Office, WorkMaterials, Utilities
    }
}