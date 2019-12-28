using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DTDOrganizer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<ResourcesAdminModel> AdminResources { get; set; }
        public DbSet<ResourcesRequestModel> RequestResources { get; set; }

        public System.Data.Entity.DbSet<DTDOrganizer.Models.BooksModel> BooksModels { get; set; }

        public System.Data.Entity.DbSet<DTDOrganizer.Models.CalendarEventModel> CalendarEventModels { get; set; }

        public System.Data.Entity.DbSet<DTDOrganizer.Models.CoursesModel> CoursesModels { get; set; }

        public System.Data.Entity.DbSet<DTDOrganizer.Models.DocumentsModel> DocumentsModels { get; set; }

        public System.Data.Entity.DbSet<DTDOrganizer.Models.RestaurantModel> RestaurantModels { get; set; }

        public System.Data.Entity.DbSet<DTDOrganizer.Models.OrdersModel> OrdersModels { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}