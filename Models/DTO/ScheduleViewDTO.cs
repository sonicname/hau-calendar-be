namespace server.Models.DTO;

public class ScheduleViewDTO
{

    public string SubjectName { get; set; } = null!;

    public int? SubjectNumCredit { get; set; }

    public DateTime DateStarted { get; set; }

    public DateTime DateEnded { get; set; }
    
    public int Day { get; set; }
    
    public int? Location { get; set; }

    public int LessonStarted { get; set; }

    public int LessonEnded { get; set; }
}
