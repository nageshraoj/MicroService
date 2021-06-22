using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Service.Entitiy;
using MongoDB.Driver;

namespace Catalog.Service.Repositories 
{
    public class ItemRepository
    {
        private const string collectionName = "items";
        private readonly IMongoCollection<ItemEntity> dbCollection;
        private readonly FilterDefinitionBuilder<ItemEntity> filterBuiler = Builders<ItemEntity>.Filter;

        public ItemRepository()
        {
            var dbClient = new MongoClient("mongodb://localhost:27017");
            var database =  dbClient.GetDatabase("Catalog");
            dbCollection = database.GetCollection<ItemEntity>(collectionName);
        }

        public async Task<IReadOnlyCollection<ItemEntity>> GetItemsAsync()
        {
            return await dbCollection.Find(filterBuiler.Empty).ToListAsync();
        }

        public async Task<ItemEntity> GetItemAsync(Guid id)
        {
            FilterDefinition<ItemEntity> filter = filterBuiler.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddItemAsync(ItemEntity item)
        {
            if(item==null)
            {
                throw new ArgumentNullException(nameof(item));
            }
          await  dbCollection.InsertOneAsync(item);
        }

        public async Task UpdateItemAsync( ItemEntity item)
        {
             if(item==null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            FilterDefinition<ItemEntity> filter = filterBuiler.Eq(entity => entity.Id, item.Id);
            await dbCollection.ReplaceOneAsync(filter, item);
        }

        public async Task RemoveItemAsync(Guid id)
        {
            FilterDefinition<ItemEntity> filter = filterBuiler.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}