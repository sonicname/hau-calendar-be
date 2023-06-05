namespace server.Models.DTO;

public class ScheduleDTO
{
    public int ScheduleId { get; set; }

    public int? UserId { get; set; }

    public int? SubjectId { get; set; }

    public int? ScheduleTimeStarted { get; set; }

    public int? ScheduleTimeEnded { get; set; }
}