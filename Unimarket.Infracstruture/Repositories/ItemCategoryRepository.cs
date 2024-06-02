using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;

namespace Unimarket.Infracstruture.Repositories
{

    public interface IItemCategoryRepository : IBaseRepository<ItemCategory, Guid>
    {

    }
    public class ItemCategoryRepository : BaseRepository<ItemCategory, Guid>, IItemCategoryRepository
    {
        public ItemCategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
