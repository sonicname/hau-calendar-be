using server.Models;
using server.Models.DTO;

namespace server.Repository;

public class ScheduleRepository
{
    private readonly HauCalendarContext _calendarContext;
    private readonly IConfiguration _configuration;

    public ScheduleRepository(HauCalendarContext context)
    {
        _calendarContext = context;
    }

    public List<ScheduleViewDTO> GetAll(int userId)
    {
        var all = (from sbj in _calendarContext.Subjects
            join scl in _calendarContext.Schedules on sbj.SubjectId equals scl.SubjectId
            join st in _calendarContext.ScheduleTimes on scl.ScheduleId equals st.ScheduleId
            join stw in _calendarContext.ScheduleDayInWeeks on st.ScheduleTimeId equals stw.ScheduleTimeId
            where scl.UserId == userId
            select new ScheduleViewDTO
            {
               SubjectNumCredit =sbj.SubjectNumCredit,
               SubjectName = sbj.SubjectName,
               DateStarted = st.DateStarted,
               DateEnded = st.DateEnded,
               LessonEnded = stw.LessonEnded,
               LessonStarted = stw.LessonStarted,
               Day = stw.Day,
               Location =  scl.Location
            }).ToList();
                 
         return all;
    }

    public void addSchedule(AddScheduleDto addScheduleDto)
    {
        var newSubject = new Subject
        {
            SubjectName = addScheduleDto.SubjectName,
            SubjectNumCredit = addScheduleDto.SubjectNumCredit
        };
        _calendarContext.Subjects.Add(newSubject);
        _calendarContext.SaveChanges();
   
        var newSchedule = new Schedule
        {
            SubjectId = newSubject.SubjectId,
            UserId = addScheduleDto.UserId,
            Location = addScheduleDto.location
        };
        _calendarContext.Schedules.Add(newSchedule);
        _calendarContext.SaveChanges();

        foreach (var dateInfo in addScheduleDto.Dates)
        {
            var dateStarted = dateInfo.DateStartEnd[0];
            var dateEnded = dateInfo.DateStartEnd[1];
     
            var scheduleId = newSchedule.ScheduleId;
            var newScheduleTime = new ScheduleTime
            {
                ScheduleId = scheduleId,
                DateEnded = dateEnded,
                DateStarted = dateStarted,
            };
            _calendarContext.ScheduleTimes.Add(newScheduleTime);
            _calendarContext.SaveChanges();
            
            foreach (var dayInfo in dateInfo.Days)
            {
                var showDay = dayInfo.Days;
                var lessonStarted = dayInfo.LessonStartEnd[0];
                var lessonEnded = dayInfo.LessonStartEnd[1];

                _calendarContext.ScheduleDayInWeeks.Add(new ScheduleDayInWeek
                {
                    Day = showDay,
                    LessonStarted = lessonStarted,
                    LessonEnded = lessonEnded,
                    ScheduleTimeId = newScheduleTime.ScheduleTimeId,
                });
            }
        }
        _calendarContext.SaveChanges();

    }
}