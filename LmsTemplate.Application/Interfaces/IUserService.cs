using LmsTemplate.Application.Dtos.Users;

namespace LmsTemplate.Application.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserSummaryDto>> GetAllAsync();

        Task<UserSummaryDto?> GetByIdAsync(string userId);
    }
}
