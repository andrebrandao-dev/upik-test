namespace ProjectApi.Models;

public class Image
{
  public int Id {get; set;}

  public required string Title {get; set;}

  public required string Description {get; set; }

  public required string Url {get; set;}

  public int LikeCount {get; set;}

  public int DislikeCount {get; set;}
}