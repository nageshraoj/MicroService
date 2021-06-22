using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Service.Entities;

namespace Catalog.Service.Repositories
{
    public interface IItemRepository
    {
        Task AddItemAsync(Item item);
        Task<Item> GetItemAsync(Guid id);
        Task<IReadOnlyCollection<Item>> GetItemsAsync();
        Task RemoveItemAsync(Guid id);
        Task UpdateItemAsync(Item item);
    }
}