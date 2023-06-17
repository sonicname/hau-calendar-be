namespace server.Models.DTO;

public class AddScheduleDto
{
    public int UserId { get; set; }
    public int Location { get; set; }
    public string SubjectName { get; set; }
    public int SubjectNumCredit { get; set; }
    public List<DateInfo> Dates { get; set; }
}
public class DateInfo
{
    public List<string> DateStartEnd { get; set; }
    public List<DayInfo> Days { get; set; }
}
public class DayInfo
{
    public int Days { get; set; }
    public List<int> LessonStartEnd { get; set; }
}
