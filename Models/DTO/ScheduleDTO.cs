namespace server.Models.DTO;

public class ScheduleDTO
{

    public int? UserId { get; set; }

    public int? SubjectId { get; set; }

    public DateTime ScheduleTimeStarted { get; set; }

    public DateTime ScheduleTimeEnded { get; set; }
}