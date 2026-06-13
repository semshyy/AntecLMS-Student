using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Students.Commands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Result>
{
  private readonly IUserRepository _users;
  private readonly ICurrentUserService _currentUser;
  private readonly IPasswordHasher _hasher;
  private readonly IUnitOfWork _uow;

  public ChangePasswordHandler(
      IUserRepository users,
      ICurrentUserService currentUser,
      IPasswordHasher hasher,
      IUnitOfWork uow)
  {
    _users = users;
    _currentUser = currentUser;
    _hasher = hasher;
    _uow = uow;
  }

  public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken ct)
  {
    var user = await _users.GetByIdAsync(_currentUser.UserId, ct);
    if (user is null)
      return Result.Failure("İstifadəçi tapılmadı.", 404);

    if (!_hasher.Verify(request.CurrentPassword, user.Password))
      return Result.Failure("Mövcud şifrə yanlışdır.", 422);

    user.ChangePassword(_hasher.Hash(request.NewPassword));
    await _uow.SaveChangesAsync(ct);

    return Result.Success();
  }
}