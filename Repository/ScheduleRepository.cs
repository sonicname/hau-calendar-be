using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Models.DTO;
using System.Collections.Generic;


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
            where scl.UserId == userId
            select new ScheduleViewDTO
            {
                SubjectName = sbj.SubjectName,
                SubjectNumCredit = sbj.SubjectNumCredit,
                ScheduleTimeStarted = scl.ScheduleTimeStarted,
                ScheduleTimeEnded = scl.ScheduleTimeEnded,
                DateEnded = st.DateEnded,
                DateStarted = st.DateStarted,       
                LessonStarted = st.LessonStarted,
                LessonEnded = st.LessonEnded
            }).ToList();
                 
         return all;
    }

    public int addSubject(string subjectName,int subjectNumCredit)
    {
        var newSubject = new Subject
        {
            SubjectName = subjectName,
            SubjectNumCredit = subjectNumCredit
        };
         _calendarContext.Subjects.Add(newSubject);
         _calendarContext.SaveChanges();
         return newSubject.SubjectId;
    }
    

    public  void addSchedule(AddScheduleDto addScheduleDto)
    {
       var newSubject = new Subject
        {
            SubjectName = addScheduleDto.SubjectName,
            SubjectNumCredit =addScheduleDto.SubjectNumCredit
        };
        _calendarContext.Subjects.Add(newSubject);
        _calendarContext.SaveChanges();
        var subjectId = newSubject.SubjectId;
       
        foreach (var dateInfo in addScheduleDto.Dates)
        {
            var dateStarted = dateInfo.DateStartEnd[0];
            var  dateEnded = dateInfo.DateStartEnd[1];
            var newSchedule = new Schedule
            {
                SubjectId = subjectId,
                UserId = addScheduleDto.UserId,
                ScheduleTimeStarted = dateStarted,
                ScheduleTimeEnded = dateEnded
                  
            };
            _calendarContext.Schedules.Add(newSchedule);
            _calendarContext.SaveChanges();
            var scheduleId = newSchedule.ScheduleId;
            
            foreach (var dayInfo  in  dateInfo.Days)
            {
                var showDay = dayInfo.Days;
                var lessonStarted = dayInfo.LessonStartEnd[0];
                var lessonEnded = dayInfo.LessonStartEnd[1];
            
                var scheduleTime = new ScheduleTime
                {
                    ScheduleId = scheduleId,
                    LessonEnded = lessonEnded,
                    DateEnded = showDay,
                    DateStarted = showDay,
                    LessonStarted = lessonStarted,
                };
                _calendarContext.ScheduleTimes.Add(scheduleTime);
                _calendarContext.SaveChanges();

            }
        }
    }
    public void addScheduleTime(){

//     foreach (int lesson in lessonList)
//     {
//         // Create a new ScheduleTime entity
//         ScheduleTime scheduleTime = new ScheduleTime
//         {
//             // Set the necessary properties for ScheduleTime
//             // For example, lesson, date, time, etc.
//         };
//
//         // Save the ScheduleTime entity to the database
//         _calendarContext.ScheduleTimes.Add(scheduleTime);
//         await _calendarContext.SaveChangesAsync();
//     }
// }public async Task SaveLessons(List<int> lessonList)
// {
//     foreach (int lesson in lessonList)
//     {
//         // Create a new ScheduleTime entity
//         ScheduleTime scheduleTime = new ScheduleTime
//         {
//             // Set the necessary properties for ScheduleTime
//             // For example, lesson, date, time, etc.
//         };
//
//         // Save the ScheduleTime entity to the database
//         _calendarContext.ScheduleTimes.Add(scheduleTime);
//         await _calendarContext.SaveChangesAsync();
    

    }
  

    public void Delete(int  id)
    {
     
    }
}