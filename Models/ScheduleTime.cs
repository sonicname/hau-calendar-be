namespace server.Models;

public partial class ScheduleTime
{
    public int ScheduleTimeId { get; set; }

    public int? ScheduleId { get; set; }

    public DateTime DateStarted { get; set; }

    public DateTime DateEnded { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public virtual ICollection<ScheduleDayInWeek> ScheduleDayInWeeks { get; set; } = new List<ScheduleDayInWeek>();
}
