namespace server.DTO.ScheduleDTOs;

public class ScheduleDTO
{
    public int ScheduleId { get; set; }

    public int? UserId { get; set; }

    public int? SubjectId { get; set; }

    public int? Location { get; set; }

    public List<ScheduleTimeDTO> ScheduleTimes { get; set; }

    public SubjectDTO Subject { get; set; } = null;
}
