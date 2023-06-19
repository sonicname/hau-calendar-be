namespace server.DTO.ScheduleDTOs;

public class ScheduleDayInWeekDTO
{
    public int ScheduleDayInWeekId { get; set; }
    public int Day { get; set; }

    public int LessonStarted { get; set; }

    public int LessonEnded { get; set; }

    public int? ScheduleTimeId { get; set; }


}
