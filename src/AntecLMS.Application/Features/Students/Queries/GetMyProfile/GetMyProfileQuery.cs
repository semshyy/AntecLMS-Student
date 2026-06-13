using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyProfile;

public record GetMyProfileQuery : IRequest<Result<MyProfileResponse>>;

public record MyProfileResponse(
    int Id,
    string Name,
    string Surname,
    string Email,
    string? Phone,
    DateOnly? BirthDate,
    string? Note,
    string Status
);