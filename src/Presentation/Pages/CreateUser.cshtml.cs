using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Pages
{
  public class CreateUserModel : PageModel
  {
    private readonly UserService userService;
    private readonly FaceService faceService;
    public CreateUserModel(
      UserService userService,
      FaceService faceService
    )
    {
      this.userService = userService;
      this.faceService = faceService;
    }

    public IActionResult OnGet()
    {
      return Page();
    }

    [BindProperty]
    public new User? User { get; set; }

    [BindProperty]
    public IFormFile ImageFile { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      if (User != null)
      {
        UserImage detectHumanResult = faceService.DetectImageIsHuman(Guid.NewGuid(),ImageFile.OpenReadStream());
        User.Guid = detectHumanResult.Guid;
        User.IsHuman = detectHumanResult.Detected;
        if(User.IsHuman)
          User.Image = detectHumanResult.Image;
        await userService.AddUserAsync(User);
      }

      return RedirectToPage("./Index");
    }
  }
}
