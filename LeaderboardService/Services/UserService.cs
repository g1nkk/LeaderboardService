using LeaderboardService.DataBase.Models;
using LeaderboardService.Repositories;

namespace LeaderboardService.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<int> GetUsersCountAsync()
    {
        return await _userRepository.GetAllCountAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _userRepository.AddAsync(user);
    }
}