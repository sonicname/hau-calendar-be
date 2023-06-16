namespace server.Models.DTO;

public class ScheduleDayInWeekDTO
{
    public int Day { get; set; }

    public int LessonStarted { get; set; }

    public int LessonEnded { get; set; }

    public int? ScheduleTimeId { get; set; }
}
