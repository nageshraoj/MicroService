using System;

namespace Catalog.Service.Entitiy 
{
    public class ItemEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        
        
        
        
    }
}