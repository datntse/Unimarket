using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;

namespace Unimarket.Infracstruture.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order, Guid>
    {

    }
    public class OrderRepository : BaseRepository<Order,Guid>, IOrderRepository 
    {
        public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
