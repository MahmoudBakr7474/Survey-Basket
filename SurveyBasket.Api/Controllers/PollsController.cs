using SurveyBasket.Contracts.Requests;
using SurveyBasket.Mapping;
namespace SurveyBasket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var polls = _pollService.GetAll();
        return Ok(polls.MapToResponse());
    }


    [HttpGet("{id}")]
    public IActionResult Get([FromRoute] int id)
    {
        var poll = _pollService.Get(id);

        return poll is null ? NotFound() : Ok(poll.MapToResponse());
    }


    [HttpPost("")]
    public IActionResult Add([FromBody] CreatePollRequst request)
    {
        var newPoll = _pollService.Add(request.MapToPoll());
        return CreatedAtAction(nameof(Get), new { newPoll.Id }, newPoll);
    }


    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] CreatePollRequst request)
    {
        var isUpdated = _pollService.Upate(id, request.MapToPoll());

        if (!isUpdated)
            return NotFound();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var isDeleted = _pollService.Delete(id);

        if (!isDeleted)
            return NotFound();
        return NoContent();
    }

  
}
