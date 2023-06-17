namespace server.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public int? SubjectNumCredit { get; set; }

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();
}
