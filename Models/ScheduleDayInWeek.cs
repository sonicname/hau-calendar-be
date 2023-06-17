using System;
using System.Collections.Generic;

namespace server.Models;

public partial class ScheduleDayInWeek
{
    public int ScheduleDayInWeekId { get; set; }

    public int Day { get; set; }

    public int LessonStarted { get; set; }

    public int LessonEnded { get; set; }

    public int? ScheduleTimeId { get; set; }

    public virtual ScheduleTime? ScheduleTime { get; set; }
}
