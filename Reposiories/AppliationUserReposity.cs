using Hos.Date;
using Hos.Models;
using Hos.Reposiories.IReposiories;

namespace Hos.Reposiories
{
    public class AppliationUserReposity : Repositry<ApplicationUser> , IAppliationUserReposity
    {
        private readonly ApplicationDbcontext dbcontext;
        public AppliationUserReposity(ApplicationDbcontext dbcontext) : base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }
       
    }
}
