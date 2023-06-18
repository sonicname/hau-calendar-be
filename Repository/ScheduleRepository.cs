using Microsoft.EntityFrameworkCore;
using server.DTO.ScheduleDTOs;
using server.Models;

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

    public ScheduleDTO? GetScheduleById(int scheduleId)
    {
        using var dbContext = new HauCalendarContext();

        var schedule = dbContext.Schedules
            .Where(s => s.ScheduleId == scheduleId)
            .Include(s => s.Subject)
            .Include(s => s.ScheduleTimes)
            .ThenInclude(st => st.ScheduleDayInWeeks)
            .FirstOrDefault();

        if (schedule != null)
        {
            var scheduleDto = new ScheduleDTO
            {
                ScheduleId = schedule.ScheduleId,
                UserId = schedule.UserId,
                Location = schedule.Location,
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
            };

            return scheduleDto;
        }

        return null;
    }

    public List<ScheduleDTO> GetScheduleByDate(int userId, DateTime date)
    {
        using var dbContext = new HauCalendarContext();

        var schedules = dbContext.Schedules
            .Where(schedule => schedule.UserId == userId)
            .Include(schedule => schedule.Subject)
            .Include(schedule => schedule.ScheduleTimes)
            .ThenInclude(scheduleTime => scheduleTime.ScheduleDayInWeeks)
            .Select(schedule => new ScheduleDTO
            {
                ScheduleId = schedule.ScheduleId,
                UserId = schedule.UserId,
                Location = schedule.Location,
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
                    ScheduleDayInWeeks = scheduleTime.ScheduleDayInWeeks.Where(scheduleDayInWeek =>
                        scheduleDayInWeek.Day == (int)date.DayOfWeek &&
                        scheduleTime.DateStarted.Date <= date.Date &&
                        scheduleTime.DateEnded.Date >= date.Date
                    ).Select(scheduleDayInWeek => new ScheduleDayInWeekDTO
                    {
                        ScheduleDayInWeekId = scheduleDayInWeek.ScheduleDayInWeekId,
                        Day = scheduleDayInWeek.Day,
                        LessonStarted = scheduleDayInWeek.LessonStarted,
                        LessonEnded = scheduleDayInWeek.LessonEnded
                    }).ToList()
                }).Where(scheduleTime => scheduleTime.ScheduleDayInWeeks.Any()).ToList()
            })
            .ToList();

        return schedules;
    }

    public ScheduleDTO GetNearestScheduleByDayOfWeek(int userId, DateTime date)
    {
        using var dbContext = new HauCalendarContext();

        var dayOfWeek = (int)date.DayOfWeek;
        var currentDate = DateTime.Now.Date;

        var schedule = dbContext.Schedules
            .Where(schedule => schedule.UserId == userId)
            .Include(schedule => schedule.Subject)
            .Include(schedule => schedule.ScheduleTimes)
            .ThenInclude(scheduleTime => scheduleTime.ScheduleDayInWeeks)
            .Select(schedule => new ScheduleDTO
            {
                ScheduleId = schedule.ScheduleId,
                UserId = schedule.UserId,
                Location = schedule.Location,
                Subject = new SubjectDTO
                {
                    SubjectId = schedule.Subject.SubjectId,
                    SubjectName = schedule.Subject.SubjectName,
                    SubjectNumCredit = schedule.Subject.SubjectNumCredit
                },
                ScheduleTimes = schedule.ScheduleTimes
                    .Select(scheduleTime => new ScheduleTimeDTO
                    {
                        ScheduleTimeId = scheduleTime.ScheduleTimeId,
                        DateStarted = scheduleTime.DateStarted,
                        DateEnded = scheduleTime.DateEnded,
                        ScheduleDayInWeeks = scheduleTime.ScheduleDayInWeeks
                            .Where(sd => sd.Day >= dayOfWeek)
                            .OrderBy(sd => Math.Abs((int)sd.Day - dayOfWeek))
                            .Select(scheduleDayInWeek => new ScheduleDayInWeekDTO
                            {
                                ScheduleDayInWeekId = scheduleDayInWeek.ScheduleDayInWeekId,
                                Day = scheduleDayInWeek.Day,
                                LessonStarted = scheduleDayInWeek.LessonStarted,
                                LessonEnded = scheduleDayInWeek.LessonEnded
                            })
                            .ToList()
                    })
                    .Where(scheduleTime => scheduleTime.ScheduleDayInWeeks.Any())
                    .OrderBy(scheduleTime => Math.Abs((int)scheduleTime.DateStarted.DayOfWeek - dayOfWeek))
                    .ToList()
            })
            .OrderBy(schedule => schedule.ScheduleTimes.Min(scheduleTime => Math.Abs((int)scheduleTime.DateStarted.DayOfWeek - dayOfWeek)))
            .FirstOrDefault();

        return schedule;
    }


    public void AddSchedule(AddScheduleDTO scheduleDto)
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

    public void UpdateSchedule(int scheduleId, UpdatedScheduleDTO updatedScheduleDto)
    {
        using var dbContext = new HauCalendarContext();

        var schedule = dbContext.Schedules
            .Include(s => s.Subject)
            .Include(s => s.ScheduleTimes)
            .ThenInclude(st => st.ScheduleDayInWeeks)
            .FirstOrDefault(s => s.ScheduleId == scheduleId);

        if (schedule != null)
        {
            schedule.UserId = updatedScheduleDto.UserId;
            schedule.Location = updatedScheduleDto.Location;
            schedule.Subject.SubjectName = updatedScheduleDto.SubjectName;
            schedule.Subject.SubjectNumCredit = updatedScheduleDto.SubjectNumCredit;

            dbContext.ScheduleDayInWeeks.RemoveRange(schedule.ScheduleTimes.SelectMany(st => st.ScheduleDayInWeeks));
            dbContext.ScheduleTimes.RemoveRange(schedule.ScheduleTimes);

            foreach (var updatedScheduleTimeDto in updatedScheduleDto.Dates)
            {
                var scheduleTime = new ScheduleTime
                {
                    DateStarted = DateTime.Parse(updatedScheduleTimeDto.DateStartEnd[0]),
                    DateEnded = DateTime.Parse(updatedScheduleTimeDto.DateStartEnd[1]),
                    ScheduleDayInWeeks = new List<ScheduleDayInWeek>()
                };

                foreach (var updatedScheduleDayInWeekDto in updatedScheduleTimeDto.Days)
                {
                    var scheduleDayInWeek = new ScheduleDayInWeek
                    {
                        Day = updatedScheduleDayInWeekDto.Days,
                        LessonStarted = updatedScheduleDayInWeekDto.LessonStartEnd[0],
                        LessonEnded = updatedScheduleDayInWeekDto.LessonStartEnd[1]
                    };

                    scheduleTime.ScheduleDayInWeeks.Add(scheduleDayInWeek);
                }

                schedule.ScheduleTimes.Add(scheduleTime);
            }

            dbContext.SaveChanges();
        }
    }
}
