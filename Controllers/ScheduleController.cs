using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Repository;
namespace server.Controllers;

[Route("/api/schedule")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly HauCalendarContext _calendarContext;
    private readonly IConfiguration _configuration;
    private readonly ScheduleRepository _scheduleRepository;
    public ScheduleController(IConfiguration config, HauCalendarContext calendarContext,ScheduleRepository scheduleRepository)
    {
        _calendarContext = calendarContext;
        _configuration = config;
        _scheduleRepository = scheduleRepository;
    }

    [HttpGet("get")]
    public IActionResult GetSchedule(int userID)
    {
        this._scheduleRepository.GetAll(userID);
        return Ok();
    }

    [HttpPost("create")]
    public IActionResult CreateSchedule() // missing params
    {
        return Ok();
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