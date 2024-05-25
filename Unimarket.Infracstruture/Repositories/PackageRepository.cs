using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;

namespace Unimarket.Infracstruture.Repositories
{
    public interface IPackageRepository : IBaseRepository<Package, Guid>
    {

    }
    public class PackageRepository : BaseRepository<Package,Guid>, IPackageRepository
    {
        public PackageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
