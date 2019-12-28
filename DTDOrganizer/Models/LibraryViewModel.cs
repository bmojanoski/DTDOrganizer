using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTDOrganizer.Models
{
    public class LibraryViewModel
    {
        public List<BooksModel> books { get; set; }
        public List<CoursesModel> courses { get; set; }
        public List<DocumentsModel> documents { get; set; }
    }
}