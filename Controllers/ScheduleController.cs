using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.DTO;
using server.Repository;
namespace server.Controllers;

[Route("/api/schedule")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleRepository _scheduleRepository;
    public ScheduleController(ScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    [HttpGet("get")]
    public IActionResult GetSchedule(int userID)
    {
        var schedules = _scheduleRepository.GetAll(userID);
        return Ok(schedules);
    }

    [HttpPut("create")]
    public IActionResult CreateSchedule([FromBody] AddScheduleRequestDTO requestDto) // missing params
    {
        return Ok(requestDto);
    }

    [HttpDelete("delete")]
    public IActionResult DeleteSchedule(string scheduleID)
    {
        return Ok();
    }

    [HttpPatch("update")]
    public IActionResult UpdateSchedule(string scheduleID) // missing params
    {
        return Ok();
    }
}
