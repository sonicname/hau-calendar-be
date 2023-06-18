namespace server.DTO.ScheduleDTOs;

public class UpdatedScheduleDTO
{
	public int ScheduleId { get; set; }
	public int UserId { get; set; }
	public int Location { get; set; }
	public string SubjectName { get; set; }
	public int SubjectNumCredit { get; set; }
	public List<DateInfo> Dates { get; set; }
}
