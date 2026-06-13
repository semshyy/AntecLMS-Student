using AntecLMS.Application.Common.Models;
using MediatR;
using System;

namespace AntecLMS.Application.Features.Groups.Commands.AddStudentToGroup;

public record AddStudentToGroupCommand(int GroupId, int StudentId)
    : IRequest<Result<AddStudentResponse>>;

public record AddStudentResponse(int GroupId, int StudentId, DateTime JoinedAt, string Status);