using Microsoft.EntityFrameworkCore;
using Presentation.DbContext;
using Presentation.Models;

namespace Presentation.Services;

public class UserService
{
  private readonly RecognizerContext _context;

  public UserService(RecognizerContext context)
  {
    _context = context;
  }

  public async Task<User?> GetUserByGuid(Guid guid)
  {
    return await _context.Users.FindAsync(guid);
  }

  public async Task AddUserAsync(User user)
  {
    _context.Users.Add(user);

    await _context.SaveChangesAsync();
  }

  public async Task<User[]> GetUsersAsync()
  {
    return await _context.Users.OrderBy(u => u.Id).ToArrayAsync();
  }

  public async Task<byte[]?> GetUserImageBufferByGuid(Guid guid)
  {
    return (await _context.Users.FindAsync(guid))?.Image;
  }

  public async Task DeleteByGuidAsync(Guid guid)
  {
    await _context.Users.Where(u => u.Guid == guid).ExecuteDeleteAsync();
  }

  public async Task UpdateUserAsync(Guid guid, User user)
  {
    await _context.Users.Where(u => u.Guid == guid).ExecuteUpdateAsync(pu => pu
      .SetProperty(u => u.Email,user.Email)
      .SetProperty(u => u.FirstName,user.FirstName)
      .SetProperty(u => u.LastName,user.LastName)
      .SetProperty(u => u.Image,user.Image)
    );
  }
}
