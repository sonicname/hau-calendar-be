namespace server.Models.DTO;

public class DaytimeSubject
{
    public int days { get; set; }
    
    public List<int> lesson { get; set; }
}

public class ScheduleDates
{
    public List<string> date { get; set; }
    public List<DaytimeSubject> days { get; set; }
}

public class AddScheduleRequestDTO
{
    public string subject_name { get; set; }
    
    public int learn_type { get; set; }
    
    public int number_subject_credit { get; set; }

    public List<ScheduleDates> dates { get; set; }
}