using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyDashboard;

public record GetMyDashboardQuery : IRequest<Result<MyDashboardResponse>>;

public record MyDashboardResponse(
    MyGroupInfo? Group,
    List<MyRecentLesson> RecentLessons,
    List<MyRecentGrade> RecentGrades,
    MyAttendanceSummary AttendanceSummary
);

public record MyGroupInfo(int Id, string Name, string GroupCode, string Status);

public record MyRecentLesson(
    int Id,
    string Title,
    DateOnly LessonDate,
    int MaterialCount
);

public record MyRecentGrade(
    int Id,
    string LessonTitle,
    double Score,
    double MaxScore
);

public record MyAttendanceSummary(
    int Total,
    int Present,
    int Absent,
    int Late
);