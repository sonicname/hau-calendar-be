﻿namespace server.Models.DTO;

public class ScheduleTimeDTO
{
    public int ScheduleDayInWeekId { get; set; }

    public DateTime DateStarted { get; set; }

    public DateTime DateEnded { get; set; }
    
}