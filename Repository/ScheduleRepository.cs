using server.Models;
using server.Models.DTO;

namespace server.Repository;

public class ScheduleRepository
{
    private readonly HauCalendarContext _calendarContext;

    public ScheduleRepository(HauCalendarContext context)
    {
        _calendarContext = context;
    }

    public List<ScheduleViewDTO> GetAll(int userId)
    {
        // var all = (from sbj in _calendarContext.Subjects
        //            join scl in _calendarContext.Schedules on sbj.SubjectId equals scl.SubjectId
        //            join st in _calendarContext.ScheduleTimes on scl.ScheduleId equals st.ScheduleId
        //            where scl.UserId == userId
        //            select new ScheduleViewDTO
        //            {
        //                SubjectName = sbj.SubjectName,
        //                SubjectNumCredit = sbj.SubjectNumCredit,
        //                ScheduleTimeStarted = scl.ScheduleTimeStarted,
        //                ScheduleTimeEnded = scl.ScheduleTimeEnded,
        //                DateEnded = st.DateEnded,
        //                DateStarted = st.DateStarted,
        //                LessonStarted = st.LessonStarted,
        //                LessonEnded = st.LessonEnded
        //            }).ToList();

        // return all;

        //     List<ScheduleViewDTO> schedules =  _calendarContext.Subjects
        //         .Join(
        //             _calendarContext.Schedules.Join(
        //
        //                 ),
        //                 subject => subject.SubjectId,
        //                 schedule => schedule.SubjectId,
        //             (subject,schedule) => new {Subject = subject,Schedule=schedule}
        //             )
        //         .Join(_calendarContext.ScheduleTimes,
        //
        //         )
        //         .Where()
        //         .ToList();
        //
    }

    public void Delete(int id)
    {

    }
}
