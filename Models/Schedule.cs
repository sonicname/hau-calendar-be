namespace server.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? UserId { get; set; }

    public int? SubjectId { get; set; }

    public int? Location { get; set; }

    public virtual ICollection<ScheduleTime> ScheduleTimes { get; set; } = new List<ScheduleTime>();

    public virtual Subject? Subject { get; set; }

    public virtual User? User { get; set; }
}
