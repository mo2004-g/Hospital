using Hos.Date;
using Hos.Migrations;
using Hos.Reposiories.IReposiories;

namespace Hos.Reposiories
{
    public class ProfilrRepositry : Repositry<Profile>, IProfilrRepositry
    {
        private readonly ApplicationDbcontext dbcontext;

        public ProfilrRepositry(ApplicationDbcontext dbcontext) : base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }
    }
}
