using Microsoft.EntityFrameworkCore;
using AnimalShelterApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AnimalShelterApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class DogsController : ControllerBase
  {
    private readonly AnimalShelterApiContext _db;
    public DogsController(AnimalShelterApiContext db)
    {
      _db = db;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dog>>> Get(string name, int age, string description)
    {
     IQueryable<Dog> query = _db.Dogs.AsQueryable();
      
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
    public async Task<ActionResult<Dog>> GetDog(int id)
    {
      Dog dog = await _db.Dogs.FindAsync(id);

      if (dog == null)
      {
        return NotFound();
      }
      return dog;
    }
    [HttpPost]
    public async Task<ActionResult<Dog>> Post(Dog dog)
    {
      _db.Dogs.Add(dog);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetDog), new { id = dog.DogId }, dog);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Dog dog)
    {
      if (id != dog.DogId)
      {
        return BadRequest();
      }
      _db.Dogs.Update(dog);
      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!DogExists(id))
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
    private bool DogExists(int id)
    {
      return _db.Dogs.Any(e => e.DogId == id);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDog(int id)
    {
      Dog dog = await _db.Dogs.FindAsync(id);
      if (dog == null)
      {
        return NotFound();
      }
      _db.Dogs.Remove(dog);
      await _db.SaveChangesAsync();
      return NoContent();
    }
  
  }
}