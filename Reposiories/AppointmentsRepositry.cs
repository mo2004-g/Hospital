using Hos.Date;
using Hos.Models;
using Hos.Reposiories.IReposiories;

namespace Hos.Reposiories
{
    public class AppointmentsRepositry : Repositry<HistoryofDate> , IAppointmentsRepositry
    {
        private readonly ApplicationDbcontext dbcontext;

        public AppointmentsRepositry(ApplicationDbcontext dbcontext) : base(dbcontext) 
        {
            this.dbcontext = dbcontext;
        }
    }
}
