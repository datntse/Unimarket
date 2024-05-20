using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;
using Unimarket.Infracstruture.Repositories;

namespace Unimarket.Infracstruture.Services
{
    public interface IItemService
    {
        Task<Item> FindAsync(Guid id);
    }
    internal class ItemService : IItemService
    {
        private IUnitOfWork _unitOfWork;
        private IItemRepository _itemRepository;

        public ItemService(IUnitOfWork unitOfWork, IItemRepository itemRepository)
        {
            _unitOfWork = unitOfWork;
            _itemRepository = itemRepository;
        }

        public async Task<Item> FindAsync(Guid id)
        {
            return await _itemRepository.FindAsync(id);
        }
    }
}
