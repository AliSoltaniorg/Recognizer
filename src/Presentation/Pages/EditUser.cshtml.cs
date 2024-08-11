using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Pages
{
  public class EditUserModel : PageModel
  {
    private readonly UserService userService;
    private readonly FaceService faceService;
    public EditUserModel(
      UserService userService,
      FaceService faceService
    )
    {
      this.userService = userService;
      this.faceService = faceService;
    }

    [FromQuery(Name = "Guid")]
    public Guid Guid { get; set; }

    public async Task<IActionResult> OnGet()
    {
      User = await userService.GetUserByGuid(Guid);
      if(User is null)
        return NotFound();
      return Page();
    }

    [BindProperty]
    public new User? User { get; set; }

    [BindProperty]
    public IFormFile ImageFile { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (User != null)
      {
        if(ImageFile != null)
        {
          UserImage detectHumanResult = faceService.DetectImageIsHuman(User.Guid, ImageFile.OpenReadStream());
          User.IsHuman = detectHumanResult.Detected;
          if (User.IsHuman)
            User.Image = detectHumanResult.Image;
        }
        else
        {
          User.Image = await userService.GetUserImageBufferByGuid(User.Guid);
        }
        await userService.UpdateUserAsync(User.Guid,User);
      }
      return RedirectToPage("./Index");
    }
  }
}
