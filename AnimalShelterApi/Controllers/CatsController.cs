using Microsoft.EntityFrameworkCore;
using AnimalShelterApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AnimalShelterApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CatsController : ControllerBase
  {
    private readonly AnimalShelterApiContext _db;
    public CatsController(AnimalShelterApiContext db)
    {
      _db = db;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cat>>> Get(string name, int age, string description)
    {
     IQueryable<Cat> query = _db.Cats.AsQueryable();
      
      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }
      if (age != 0)
      {
        query = query.Where(entry => entry.Age == age);
      }
      if (description != null)
      {
        query = query.Where(entry => entry.Description == description);
      }
      return await query.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Cat>> GetCat(int id)
    {
      Cat cat = await _db.Cats.FindAsync(id);

      if (cat == null)
      {
        return NotFound();
      }
      return cat;
    }
    [HttpPost]
    public async Task<ActionResult<Cat>> Post(Cat cat)
    {
      _db.Cats.Add(cat);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetCat), new { id = cat.CatId }, cat);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Cat cat)
    {
      if (id != cat.CatId)
      {
        return BadRequest();
      }
      _db.Cats.Update(cat);
      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CatExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return NoContent();
    }
    private bool CatExists(int id)
    {
      return _db.Cats.Any(e => e.CatId == id);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCat(int id)
    {
      Cat cat = await _db.Cats.FindAsync(id);
      if (cat == null)
      {
        return NotFound();
      }
      _db.Cats.Remove(cat);
      await _db.SaveChangesAsync();
      return NoContent();
    }
  
  }
}