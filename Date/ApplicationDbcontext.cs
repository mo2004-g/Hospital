using Hos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hos.Models.Viewmodel;

namespace Hos.Date
{
    public class ApplicationDbcontext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<HistoryofDate> HistoryofDates { get; set; }

        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) :base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Hospitaltocolage;Integrated Security=True;TrustServerCertificate=True");
        }
        
        
    }
}
