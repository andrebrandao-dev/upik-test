namespace ProjectApi.Models;

public class ImageDTO
{
  public int Id {get; set;}

  public required string Title {get; set;}

  public required string Description {get; set; }

  public required string Url {get; set;}

}