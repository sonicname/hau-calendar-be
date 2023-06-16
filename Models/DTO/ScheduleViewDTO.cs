namespace server.Models.DTO;

public class ScheduleViewDTO
{

    public string SubjectName { get; set; } = null!;

    public int? SubjectNumCredit { get; set; }

    public DateTime? ScheduleTimeStarted { get; set; }

    public DateTime? ScheduleTimeEnded { get; set; }

    public int? DateStarted { get; set; }

    public int? DateEnded { get; set; }

    public int LessonStarted { get; set; }

    public int LessonEnded { get; set; }
}
