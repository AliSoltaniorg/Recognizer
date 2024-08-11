namespace Presentation.Models;

public class User
{
  public Guid Guid { get; set; }
  public int Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string FullName => FirstName + ' ' + LastName;
  public string Email { get; set; } = string.Empty;
  public byte[]? Image { get; set; }
  public bool IsHuman { get; set; }
}
