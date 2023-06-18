namespace server.DTO.ScheduleDTOs;

public class ScheduleTimeDTO
{
    public int ScheduleTimeId { get; set; }
    public int ScheduleDayInWeekId { get; set; }

    public DateTime DateStarted { get; set; }

    public DateTime DateEnded { get; set; }

    public List<ScheduleDayInWeekDTO> ScheduleDayInWeeks { get; set; }
}
