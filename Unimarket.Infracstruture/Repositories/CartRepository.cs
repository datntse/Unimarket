using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;

namespace Unimarket.Infracstruture.Repositories
{
    public interface ICartRepository : IBaseRepository<CartItem, Guid>
    {

    }
    public class CartRepository : BaseRepository<CartItem,Guid>, ICartRepository
    {
        public CartRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
