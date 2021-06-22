using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Service.Entities;
using MongoDB.Driver;

namespace Catalog.Service.Repositories
{

    public class ItemRepository : IItemRepository
    {
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> dbCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuiler = Builders<Item>.Filter;

        public ItemRepository(IMongoDatabase database)
        {
            // var dbClient = new MongoClient("mongodb://localhost:27017");
            // var database = dbClient.GetDatabase("Catalog");
            dbCollection = database.GetCollection<Item>(collectionName);
        }

        public async Task<IReadOnlyCollection<Item>> GetItemsAsync()
        {
            return await dbCollection.Find(filterBuiler.Empty).ToListAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuiler.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddItemAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            await dbCollection.InsertOneAsync(item);
        }

        public async Task UpdateItemAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            FilterDefinition<Item> filter = filterBuiler.Eq(entity => entity.Id, item.Id);
            await dbCollection.ReplaceOneAsync(filter, item);
        }

        public async Task RemoveItemAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuiler.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}