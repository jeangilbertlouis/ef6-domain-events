using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webhost.Contracts;
using Webhost.Handlers;

namespace Webhost.Controllers;

[ApiController]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("/person/{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var request = new GetPersonRequest(id);
        var result = await _mediator.Send(request);
        if (result.IsSuccess) return Ok(result.Value);
        return NotFound(result.Errors.FirstOrDefault());
    }
    
    [HttpGet]
    [Route("/personlist")]
    public async Task<IActionResult> GetList()
    {
        var request = new GetPersonListRequest();
        var result = await _mediator.Send(request);
        if (result.IsSuccess) return Ok(result.Value);
        return NotFound(result.Errors.FirstOrDefault());
    }
    
    [HttpGet]
    [Route("/personlistreadmodel")]
    public async Task<IActionResult> GetListReadModel()
    {
        var request = new GetPersonReadModelListRequest();
        var result = await _mediator.Send(request);
        if (result.IsSuccess) return Ok(result.Value);
        return NotFound(result.Errors.FirstOrDefault());
    }
    
    [HttpPost]
    [Route("/person")]
    public async Task<IActionResult> Create(PersonDto personDto)
    {
        var result = await _mediator.Send(new CreatePersonRequest(personDto.FirstName, personDto.LastName));
        if (result.IsSuccess) return NoContent();
        return BadRequest(result.Errors.FirstOrDefault());
    }
}