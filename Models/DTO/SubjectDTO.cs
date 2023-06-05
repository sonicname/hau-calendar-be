namespace server.Models.DTO;

public class SubjectDTO
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public int? SubjectNumCredit { get; set; }
}