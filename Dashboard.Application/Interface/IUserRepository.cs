using Dashboard.Domain.Entities;
using Dashboard.Domain.Models;

namespace Dashboard.Application.Interface;

public interface IUserRepository : IRepository<Users>
{
    Task<Users?> LoginAsync(DtoUser? userRequest);
}