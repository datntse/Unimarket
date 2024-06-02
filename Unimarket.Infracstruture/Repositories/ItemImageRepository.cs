using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;

namespace Unimarket.Infracstruture.Repositories
{

    public interface IItemImageRepository : IBaseRepository<ItemImage, Guid>
    {

    }
    public class ItemImageRepository : BaseRepository<ItemImage, Guid>, IItemImageRepository
    {
        public ItemImageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
