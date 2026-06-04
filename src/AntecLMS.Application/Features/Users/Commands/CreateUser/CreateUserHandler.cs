using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<CreatedUserResponse>>
{
  private readonly IUserRepository _users;
  private readonly IUnitOfWork _uow;
  private readonly IPasswordHasher _hasher;

  public CreateUserHandler(IUserRepository users, IUnitOfWork uow, IPasswordHasher hasher)
  {
    _users = users;
    _uow = uow;
    _hasher = hasher;
  }

  public async Task<Result<CreatedUserResponse>> Handle(
    CreateUserCommand request,
    CancellationToken ct
  )
  {
    if (await _users.EmailExistsAsync(request.Email, ct))
    {
      return Result<CreatedUserResponse>.Failure(
        "Doğrulama xətası.",
        422,
        new Dictionary<string, string[]> { ["email"] = ["Bu email artıq mövcuddur."] }
      );
    }

    var role = Enum.Parse<UserRole>(request.Role, true);
    var status = Enum.Parse<UserStatus>(request.Status, true);

    var user = User.Create(
      request.Name,
      request.Surname,
      request.Email,
      _hasher.Hash(request.Password),
      role,
      request.Phone,
      status
    );

    await _users.AddAsync(user, ct);
    await _uow.SaveChangesAsync(ct);

    return Result<CreatedUserResponse>.Success(
      new CreatedUserResponse(
        user.Id,
        user.Name,
        user.Surname,
        user.Email,
        user.Role.ToString().ToLower(),
        user.Status.ToString().ToLower(),
        user.CreatedAt
      ),
      201
    );
  }
}
