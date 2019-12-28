using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    public class DocumentUploadModel
    {
        [Required(ErrorMessage = "File name required")]
        public string fileName { get; set; }

        [Required(ErrorMessage = "file required")]
        public HttpPostedFileBase File { get; set; }
    }
}