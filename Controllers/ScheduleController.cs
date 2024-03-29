﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("getByDate")]
    public IActionResult GetScheduleByDate(int userId, DateTime date)
    {
        var schedules = _scheduleRepository.GetScheduleByDate(userId, date);
        return Ok(schedules);
    }

    [HttpGet("getNearestSchedule")]
    public IActionResult GetNearestScheduleByDate(int userID, DateTime date)
    {
        var schedules = _scheduleRepository.GetNearestScheduleByDate(userID, date);

        return Ok(schedules);
    }

    [HttpPost("create")]
    public IActionResult CreateSchedule([FromBody] AddScheduleDTO requestDto)
    {
        _scheduleRepository.AddSchedule(requestDto);
        return Ok(requestDto);
    }

    [HttpGet("checkDuplicate")]
    public IActionResult CheckDuplicateSchedule(int userID, int lessonStarted, int lessonEnded, DateTime dateStarted, DateTime dateEnded, int day)
    {
        var isDuplicate = _scheduleRepository.CheckDuplicateSchedule(userID, lessonStarted, lessonEnded, dateStarted, dateEnded, day);

        return Ok(isDuplicate);
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
