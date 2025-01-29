using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
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
      var images = _context.Images.Select(x => ImageToImageDto(x)).ToListAsync();
      if(images.Result.Count == 0) {
        await CreateDefaultImages();
      }
    }).Wait();
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<ImageDTO>>> GetImages()
  {
    return await _context.Images.Select(x => ImageToImageDto(x)).ToListAsync();
  }
  
  [HttpGet("{id}")]
  public async Task<ActionResult<ImageDTO>> GetImageById(long id)
  {
    var image = await _context.Images.FindAsync(id);

    if(image == null)
    {
      return NotFound();
    }

    return ImageToImageDto(image);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> LikeDislikeImage(long id, bool liked)
  {
    var image = await _context.Images.FindAsync(id); 
    if (image == null) 
    {
      return NotFound(); 
    }

    if(liked)
    {
      image.LikeCount++;
    }
    else
    {
      image.DislikeCount++;
    }
    await _context.SaveChangesAsync(); 
    return NoContent();
  }

  [HttpPost("CreateImage")]
  public async Task<ActionResult<string>> CreateImage(Image image)
  {
    _context.Images.Add(image);
    
    await _context.SaveChangesAsync();

    return "ImageCreated";
  }

  [HttpPost("CreateDefaultImages")]
  public async Task<ActionResult<string>> CreateDefaultImages()
  {
    _context.Images.Add(new Image { Id = 1, Title = "Primeira Imagem", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400.png" });
    _context.Images.Add(new Image { Id = 2, Title = "Segunda Imagem", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400.png" });
    _context.Images.Add(new Image { Id = 3, Title = "Terceira Imagem", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400.png" });
    _context.Images.Add(new Image { Id = 4, Title = "Quarta Imagem", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400.png" });
    _context.Images.Add(new Image { Id = 5, Title = "Quinta Imagem", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400.png" });
    await _context.SaveChangesAsync();

    return "Default Images Created";
  }

  public static ImageDTO ImageToImageDto (Image image) => new ImageDTO
  {
    Id = image.Id,
    Title = image.Title,
    Description = image.Description,
    Url = image.Url,
    LikeCount = image.LikeCount,
    DislikeCount = image.DislikeCount
  };
}