using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {

    private static readonly List<ItemDto> items = new List<ItemDto>()
     {
        new ItemDto(Guid.NewGuid(),"Antidone","Medicine",200,DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(),"Paracitamal","Pain killer",120,DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(),"Anit-Biotic","Good for health",100,DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(),"Vitamin","Healthy",100,DateTimeOffset.UtcNow)
    };

    [HttpGet]
    public IEnumerable<ItemDto> GetCatalogItems()
    {
        return items.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult< ItemDto> GetCatalogItem(Guid id)
    {

        var item = items.Where(item => item.Id == id).SingleOrDefault();
        if(item !=null){
           return   item;  
        }
        return NotFound();
    }

    [HttpPost]
    public ActionResult<ItemDto> AddCatalogItem(CreateItemDto item)
    {
        var newItem = new ItemDto(Guid.NewGuid(),item.Name, item.Description,item.Price,  DateTimeOffset.UtcNow);
        items.Add(newItem);
        return CreatedAtAction(nameof(GetCatalogItem),new {id =newItem.Id}, newItem);
    }

    [HttpPut("{id}")]

    public IActionResult UpdateItem(Guid id, UpdateItemDto item )
    {
        var index = items.FindIndex(itm => itm.Id == id);
        if(index==-1) return NotFound();
        var currentTtem = items.Where(each => each.Id == id).SingleOrDefault();

        var updatedItem = currentTtem with
        {
            Name = item.Name,
            Description = item.Description,
            Price = item.Price
        };
        Console.WriteLine(updatedItem);
   
        items[index] = updatedItem;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveItem(Guid id)
    {
        var index = items.FindIndex(item => item.Id == id);
        if(index==-1) return NotFound();
        items.RemoveAt(index);
        return NoContent();

    }

    }
