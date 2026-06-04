using AntecLMS.Application.Common.Exceptions;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<UpdatedUserResponse>>
{
  private readonly IUserRepository _users;
  private readonly IUnitOfWork _uow;

  public UpdateUserHandler(IUserRepository users, IUnitOfWork uow)
  {
    _users = users;
    _uow = uow;
  }

  public async Task<Result<UpdatedUserResponse>> Handle(
    UpdateUserCommand request,
    CancellationToken ct
  )
  {
    var user =
      await _users.GetByIdAsync(request.Id, ct) ?? throw new NotFoundException("User", request.Id);

    var status = Enum.Parse<UserStatus>(request.Status, true);
    user.Update(request.Name, request.Surname, request.Phone, status);

    _users.Update(user);
    await _uow.SaveChangesAsync(ct);

    return Result<UpdatedUserResponse>.Success(
      new UpdatedUserResponse(
        user.Id,
        user.Name,
        user.Surname,
        user.Phone,
        user.Status.ToString().ToLower()
      )
    );
  }
}
