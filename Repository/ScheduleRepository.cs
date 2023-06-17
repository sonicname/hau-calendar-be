using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Models.DTO;

namespace server.Repository;

public class ScheduleRepository
{
    public ScheduleRepository() { }

    public List<ScheduleDTO> GetAll(int userId)
    {

        List<ScheduleDTO> scheduleDTOs;

        using (var dbContext = new HauCalendarContext())
        {
            scheduleDTOs = dbContext.Schedules
                .Where(schedule => schedule.UserId == userId)
                .Include(schedule => schedule.Subject)
                .Select(schedule => new ScheduleDTO
                {
                    ScheduleId = schedule.ScheduleId,
                    Subject = new SubjectDTO
                    {
                        SubjectId = schedule.Subject.SubjectId,
                        SubjectName = schedule.Subject.SubjectName,
                        SubjectNumCredit = schedule.Subject.SubjectNumCredit
                    },
                    ScheduleTimes = schedule.ScheduleTimes.Select(scheduleTime => new ScheduleTimeDTO
                    {
                        ScheduleTimeId = scheduleTime.ScheduleTimeId,
                        DateStarted = scheduleTime.DateStarted,
                        DateEnded = scheduleTime.DateEnded,
                        ScheduleDayInWeeks = scheduleTime.ScheduleDayInWeeks.Select(scheduleDayInWeek => new ScheduleDayInWeekDTO
                        {
                            ScheduleDayInWeekId = scheduleDayInWeek.ScheduleDayInWeekId,
                            Day = scheduleDayInWeek.Day,
                            LessonStarted = scheduleDayInWeek.LessonStarted,
                            LessonEnded = scheduleDayInWeek.LessonEnded
                        }).ToList()
                    }).ToList()
                })
                .ToList();
        }

        return scheduleDTOs;
    }

    public void AddSchedule(AddScheduleDto scheduleDto)
    {
        var schedule = new Schedule
        {
            UserId = scheduleDto.UserId,
            Location = scheduleDto.Location,
            Subject = new Subject
            {
                SubjectName = scheduleDto.SubjectName,
                SubjectNumCredit = scheduleDto.SubjectNumCredit
            },
            ScheduleTimes = new List<ScheduleTime>()
        };

        foreach (var dateInfo in scheduleDto.Dates)
        {
            var scheduleTime = new ScheduleTime
            {
                DateStarted = DateTime.Parse(dateInfo.DateStartEnd[0]),
                DateEnded = DateTime.Parse(dateInfo.DateStartEnd[1]),
                ScheduleDayInWeeks = new List<ScheduleDayInWeek>()
            };

            foreach (var dayInfo in dateInfo.Days)
            {
                var scheduleDayInWeek = new ScheduleDayInWeek
                {
                    Day = dayInfo.Days,
                    LessonStarted = dayInfo.LessonStartEnd[0],
                    LessonEnded = dayInfo.LessonStartEnd[1]
                };

                scheduleTime.ScheduleDayInWeeks.Add(scheduleDayInWeek);
            }

            schedule.ScheduleTimes.Add(scheduleTime);
        }

        using var dbContext = new HauCalendarContext();
        dbContext.Schedules.Add(schedule);
        dbContext.SaveChanges();
    }

    public void RemoveSchudule(int scheduleID)
    {
        using var dbContext = new HauCalendarContext();

        var schedule = dbContext.Schedules
            .Include(s => s.ScheduleTimes)
            .ThenInclude(st => st.ScheduleDayInWeeks)
            .FirstOrDefault(s => s.ScheduleId == scheduleID);

        if (schedule != null)
        {
            dbContext.ScheduleDayInWeeks.RemoveRange(schedule.ScheduleTimes.SelectMany(st => st.ScheduleDayInWeeks));
            dbContext.ScheduleTimes.RemoveRange(schedule.ScheduleTimes);
            dbContext.Schedules.Remove(schedule);

            dbContext.SaveChanges();
        }
    }
}
