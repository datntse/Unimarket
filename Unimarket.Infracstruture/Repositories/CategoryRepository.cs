using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;

namespace Unimarket.Infracstruture.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category, Guid>
    {

    }
    public class CategoryRepository : BaseRepository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
