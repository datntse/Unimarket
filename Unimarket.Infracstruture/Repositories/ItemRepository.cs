using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;

namespace Unimarket.Infracstruture.Repositories
{
    public interface IItemRepository : IBaseRepository<Item, Guid>
    {

    }
    public class ItemRepository : BaseRepository<Item,Guid>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
