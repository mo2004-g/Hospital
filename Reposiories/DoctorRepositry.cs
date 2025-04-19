using Hos.Date;
using Hos.Models;
using Hos.Reposiories.IReposiories;

namespace Hos.Reposiories
{
    public class DoctorRepositry : Repositry<Doctor> , IDoctorRepositry
    {
        private readonly ApplicationDbcontext dbcontext;

        public DoctorRepositry(ApplicationDbcontext dbcontext) : base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }
    }
}
