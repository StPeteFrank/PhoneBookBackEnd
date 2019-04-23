using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookBackEnd.Models;

namespace PhoneBookBackEnd.Controllers
{
  [Route("api/[controller]")]
  // localhost:5000/api/values
  [ApiController]
  public class PeopleController : ControllerBase
  {
    //GET that returns all People 
    //GET /api/people
    [HttpGet]
    public ActionResult<IEnumerable<People>> GetAction()
    {
      // query my database
      var db = new PhoneBookDbContext();
      //SELECT * FROM People
      var results = db.People.OrderBy(people => people.FirstName);
      //return the results
      return results.ToList();
    }
    [HttpPost]
    public ActionResult<People> AddPeople([FromBody] People incomingPeople)
    {
      var db = new PhoneBookDbContext();
      db.People.Add(incomingPeople);
      db.SaveChanges();
      return incomingPeople;
    }
    [HttpDelete("{id}")]
    public ActionResult<Object> DeletePeople([FromRoute]int id)
    {
      var db = new PhoneBookDbContext();
      var peopleToDelete = db.People.FirstOrDefault(people => people.Id == id);
      if (peopleToDelete != null)
      {
        db.People.Remove(peopleToDelete);
        db.SaveChanges();
        return peopleToDelete;
      }
      else
      {
        return new { message = "Person or people not found" };
      }
    }

    [HttpPut("{id}")]
    public ActionResult<object> UpdatePeople([FromRoute]int id, [FromBody] People newInformation)
    {
      var db = new PhoneBookDbContext();
      var peopleToUpdate = db.People.FirstOrDefault(people => people.Id == id);
      if (peopleToUpdate != null)
      {
        peopleToUpdate.FirstName = newInformation.FirstName;
        peopleToUpdate.LastName = newInformation.LastName;
        peopleToUpdate.PhoneNumber = newInformation.PhoneNumber;
        peopleToUpdate.Email = newInformation.Email;
        db.SaveChanges();
        return Ok(peopleToUpdate);
      }
      else
      {
        return NotFound();
      }
    }

  }
}