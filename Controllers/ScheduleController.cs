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
    public IActionResult CreateSchedule([FromBody] AddScheduleDto requestDto) // missing params
    {
        _scheduleRepository.AddSchedule(requestDto);
        return Ok(requestDto);
    }

    [HttpDelete("delete")]
    public IActionResult DeleteSchedule(int scheduleID)
    {
        _scheduleRepository.RemoveSchudule(scheduleID);
        return Ok();
    }

    [HttpPatch("update")]
    public IActionResult UpdateSchedule(string scheduleID) // missing params
    {
        return Ok();
    }
}
