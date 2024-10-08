using EfWithTph.Models;
using EfWithTph.Services;
using Microsoft.AspNetCore.Mvc;

namespace EfWithTph.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class RoadmapsController : ControllerBase
{
    private readonly RoadmapsService _roadmapsService;

    public RoadmapsController(RoadmapsService roadmapsService)
    {
        _roadmapsService = roadmapsService;
    }

    // GET: api/roadmaps
    [HttpGet]
    [ProducesResponseType<IEnumerable<Roadmap>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _roadmapsService.GetRoadmaps());
    }

    // GET api/roadmaps/5
    [HttpGet("{id}")]
    [ProducesResponseType<Roadmap>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var roadmap = await _roadmapsService.GetRoadmap(id);

        return roadmap is not null ? Ok(roadmap) : NotFound();
    }

    // POST api/roadmaps
    [HttpPost]
    [ProducesResponseType<int>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] string value)
    {
        var roadmap = await _roadmapsService.CreateRoadmap(value);

        return CreatedAtAction(nameof(Get), new { id = roadmap.Id }, roadmap.Id);
    }

    // PUT api/roadmaps/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Put(int id, [FromBody] string value)
    {
        _ = await _roadmapsService.UpdateRoadmap(id, value);

        return NoContent();
    }

    // DELETE api/roadmaps/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        await _roadmapsService.DeleteRoadmap(id);

        return NoContent();
    }

    // GET: api/roadmaps/{id}/items
    [HttpGet("{roadmapId}/items")]
    [ProducesResponseType<IEnumerable<BaseRoadmapItem>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoadmapItems(int roadmapId)
    {
        var roadmapItems = await _roadmapsService.GetRoadmapItems(roadmapId);

        return roadmapItems is not null
            ? Ok(roadmapItems)
            : NotFound();
    }


    #region Roadmap Activities

    // GET api/roadmaps/5/activities/17
    [HttpGet("{roadmapId}/activities/{activityId}")]
    [ProducesResponseType<RoadmapActivity>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetActivity(int roadmapId, int activityId)
    {
        var activity = await _roadmapsService.GetRoadmapActivity(roadmapId, activityId);

        return activity is not null ? Ok(activity) : NotFound();
    }

    [HttpPost("{roadmapId}/activities")]
    [ProducesResponseType<int>(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostActivity(int roadmapId, [FromBody] CreateRoadmapActivityRequest request)
    {
        var activity = await _roadmapsService.CreateRoadmapActivity(roadmapId, request.ParentActivityId, request.Name, request.Start, request.End);

        return CreatedAtAction(nameof(GetActivity), new { roadmapId, activityId = activity.Id }, activity.Id);
    }

    #endregion Roadmap Activities


    #region Roadmap Milestones

    // GET api/roadmaps/5/milestones/17
    [HttpGet("{roadmapId}/milestones/{milestoneId}")]
    [ProducesResponseType<RoadmapMilestone>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMilestone(int roadmapId, int milestoneId)
    {
        var milestone = await _roadmapsService.GetRoadmapMilestone(roadmapId, milestoneId);

        return milestone is not null ? Ok(milestone) : NotFound();
    }

    [HttpPost("{roadmapId}/milestones")]
    [ProducesResponseType<int>(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostMilestone(int roadmapId, [FromBody] CreateRoadmapMilestoneRequest request)
    {
        var milestone = await _roadmapsService.CreateRoadmapMilestone(roadmapId, request.ParentActivityId, request.Name, request.Date);

        return CreatedAtAction(nameof(GetMilestone), new { roadmapId, milestoneId = milestone.Id }, milestone.Id);
    }



    #endregion Roadmap Milestones
}
