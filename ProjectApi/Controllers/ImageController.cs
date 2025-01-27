using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Models;

namespace ProjectApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
  private readonly ImageContext _context;

  public ImageController(ImageContext context)
  {
    _context = context;

    Task.Run(async () => {
      await CreateImage();
    }).Wait();
  }

  [HttpGet(Name = "GetImage")]
  public async Task<ActionResult<IEnumerable<ImageDTO>>> Get()
  {
    return await _context.Images.Select(x => ImageToImageDto(x)).ToListAsync();
  }

  [HttpPost(Name = "CreateImage")]
  public async Task<ActionResult<string>> CreateImage()
  {
    _context.Images.Add(new Image { Id = 1, Title = "Primeira Imagem", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400.png" });
    _context.Images.Add(new Image { Id = 2, Title = "Segunda Imagem", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400.png" });
    _context.Images.Add(new Image { Id = 3, Title = "Terceira Imagem", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400.png" });
    _context.Images.Add(new Image { Id = 4, Title = "Quarta Imagem", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400.png" });
    _context.Images.Add(new Image { Id = 5, Title = "Quinta Imagem", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400.png" });
    await _context.SaveChangesAsync();

    return "Images Created";
  }

  public static ImageDTO ImageToImageDto (Image image) => new ImageDTO
  {
    Id = image.Id,
    Title = image.Title,
    Description = image.Description,
    Url = image.Url
  };
}