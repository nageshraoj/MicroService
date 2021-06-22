using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Service;
using Catalog.Service.Entities;
using Catalog.Service.Repositories;
using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {

    private readonly IItemRepository itemRepository;

    public CatalogController(IItemRepository itemRepo)
    {
        this.itemRepository = itemRepo;
    }

    [HttpGet]
    public async Task< IEnumerable<ItemDto>> GetCatalogItemsAsync()
    {
        var items = (await itemRepository.GetItemsAsync())
                    .Select(item => item.AsDto());
        return items;
    }

    [HttpGet("{id}")]
    public async Task< ActionResult< ItemDto>> GetCatalogItemAsync(Guid id)
    {
        
        var item = (await itemRepository.GetItemAsync(id));
        if(item==null)
        {
            return NotFound();
        }
        return item.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> AddCatalogItemAsync(CreateItemDto item) 
    {
        var newItem = new Item { Id= Guid.NewGuid(),
        Name= item.Name,
        Description= item.Description, 
        Price= item.Price,
        CreatedDateAt= DateTimeOffset.UtcNow };
        await itemRepository.AddItemAsync(newItem);
        
        return CreatedAtAction(nameof(GetCatalogItemAsync), new {id = newItem.Id}, newItem);
    }

    [HttpPut("{id}")]

    public async Task< IActionResult> UpdateItemAsync(Guid id, UpdateItemDto item )
    {
        var existingItem = await itemRepository.GetItemAsync(id);
        if(existingItem==null)
        {
            return NotFound();
        }
        existingItem.Name = item.Name;
        existingItem.Description = item.Description;
        existingItem.Price = existingItem.Price;
        await itemRepository.UpdateItemAsync(existingItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task< IActionResult> RemoveItemAsync(Guid id)
    {
         var existingItem = await itemRepository.GetItemAsync(id);
        if(existingItem==null)
        {
            return NotFound();
        }
        await itemRepository.RemoveItemAsync(id);
        return NoContent();

    }

    }
