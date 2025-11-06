using System.Collections.Generic;
using System.Threading.Tasks;
using LmsTemplate.Application.Dtos.Users;

namespace LmsTemplate.Application.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserSummaryDto>> GetAllAsync();
    }
}
