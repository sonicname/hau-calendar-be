using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Controllers;

[Route("/api/schedule")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly HauCalendarContext _calendarContext;
    private readonly IConfiguration _configuration;
    
    public ScheduleController(IConfiguration config, HauCalendarContext calendarContext)
    {
        _calendarContext = calendarContext;
        _configuration = config;
    }

    [HttpGet("get")]
    public IActionResult GetSchedule(string userID)
    {
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