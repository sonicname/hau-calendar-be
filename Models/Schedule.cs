using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? UserId { get; set; }

    public int? SubjectId { get; set; }

    public int? Location { get; set; }
}
