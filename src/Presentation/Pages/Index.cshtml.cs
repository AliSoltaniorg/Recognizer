using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Models;
using Presentation.Protos;
using Presentation.Services;

namespace Presentation.Pages
{
  public class IndexModel : PageModel
  {
    private readonly UserService userService;

    public IndexModel(UserService userService)
    {
      this.userService = userService;
    }

    public User[] Users { get; private set; } = Array.Empty<User>();

    public async Task OnGet()
    {
      Users = await userService.GetUsersAsync();
    }

    [FromQuery]
    public Guid? Guid { get; set; }
    public async Task<IActionResult> OnGetDeleteAsync()
    {
      if (Guid.GetValueOrDefault() == System.Guid.Empty)
        return RedirectToPage("./Index");

      await userService.DeleteByGuidAsync(Guid.Value);
      return RedirectToPage("./Index");
    }
  }
}
