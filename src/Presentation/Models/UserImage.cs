namespace Presentation.Models;

public class UserImage
{
  public Guid Guid { get; set; }
  public bool Detected { get; set; }
  public byte[]? Image { get; set; }
}
