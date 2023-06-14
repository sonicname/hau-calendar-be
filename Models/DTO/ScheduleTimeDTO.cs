namespace server.Models.DTO;

public class aScheduleTimeDTO
{
    public int ScheduleId { get; set; }

    public DateTime DateStarted { get; set; }

    public DateTime DateEnded { get; set; }

    public int LessonStarted { get; set; }

    public int LessonEnded { get; set; }
}