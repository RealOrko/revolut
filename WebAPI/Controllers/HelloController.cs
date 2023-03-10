using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Repository;
using WebAPI.Repository.Entities;

namespace WebAPI.Controllers;

[ApiController]
[Route("hello")]
public class HelloController : ControllerBase
{
    private readonly DataContext _database;

    public HelloController(DataContextFactory databaseFactory)
    {
        _database = databaseFactory.CreateDbContext();
    }

    [HttpGet]
    [Route("{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute] string username)
    {
        var model = new HelloGetRequestModel(username);
        var validation = new ValidationContext(model);
        var results = new List<ValidationResult>();

        if (Validator.TryValidateObject(model, validation, results, true))
        {
            var person = await _database.Persons.SingleOrDefaultAsync(x => x.Name == model.Username);
            return Ok(new HelloGetResponseModel(person.Name, person.DateOfBirth));
        }

        return BadRequest(results);
    }

    [HttpPut]
    [Route("{username}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromRoute] string username, [FromBody] HelloPutRequestDateModel dateOfBirth)
    {
        var model = new HelloPutRequestUserModel(username, dateOfBirth.DateOfBirth);
        var validation = new ValidationContext(model);
        var results = new List<ValidationResult>();

        if (Validator.TryValidateObject(model, validation, results, true))
        {
            _database.Persons.Add(new Person() { 
                Name = model.Username, 
                DateOfBirth = DateTime.SpecifyKind(DateTime.Parse(model.DateOfBirth), DateTimeKind.Utc) 
            });
            await _database.SaveChangesAsync();
            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }

        return BadRequest(results);
    }
}