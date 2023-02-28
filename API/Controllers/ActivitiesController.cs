using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet] // api/activites
    public async Task<ActionResult<List<Activity>>> GetActivites()
    {
        return await Mediator.Send(new AList.Query());
    }

    [HttpGet("{id}")] // api/activites/id
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity(Activity activity)
    {
        return Ok(await Mediator.Send(new Create.Command { Activity = activity }));
    }
    
}