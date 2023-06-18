using Microsoft.AspNetCore.Mvc;
using server.DTO.ScheduleDTOs;
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

    [HttpGet("getAll")]
    public IActionResult GetSchedule(int userID)
    {
        var schedules = _scheduleRepository.GetAll(userID);
        return Ok(schedules);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetScheduleByID(int id)
    {
        var schedule = _scheduleRepository.GetScheduleById(id);
        return Ok(schedule);
    }

    [HttpPost("create")]
    public IActionResult CreateSchedule([FromBody] AddScheduleDTO requestDto)
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

    [HttpPut("update")]
    public IActionResult UpdateSchedule([FromBody] UpdatedScheduleDTO updatedScheduleDTO)
    {
        _scheduleRepository.UpdateSchedule(updatedScheduleDTO.ScheduleId, updatedScheduleDTO);
        return Ok();
    }
}
