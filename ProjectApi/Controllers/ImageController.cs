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
  public async Task<ActionResult<ImageDTO>> LikeDislikeImage(long id, LikeRequest likeRequest)
  {
    var image = await _context.Images.FindAsync(id); 
    if (image == null) 
    {
      return NotFound(); 
    }

    if(likeRequest.Liked)
    {
      image.LikeCount++;
    }
    else
    {
      image.DislikeCount++;
    }

    await _context.SaveChangesAsync();
    
    var nextId = await NextImageId(id);
    var nextImage = await _context.Images.FindAsync(nextId);
    if(nextImage == null)
    {
      return NotFound();
    }

    return ImageToImageDto(nextImage);
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
    _context.Images.Add(new Image { Id = 1, Title = "Imagem Cinza", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400/232523/FFF.png" });
    _context.Images.Add(new Image { Id = 2, Title = "Imagem Roxa", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400/744086/FFF.png" });
    _context.Images.Add(new Image { Id = 3, Title = "Imagem Amarela", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400/F9CC3C/744086.png" });
    _context.Images.Add(new Image { Id = 4, Title = "Imagem Laranja", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400/E2604C/FFF.png" });
    _context.Images.Add(new Image { Id = 5, Title = "Imagem Verde", Description = "Lorem ipsum dolor sit amet", Url = "https://placehold.co/600x400/155E4B/FFF.png" });
    await _context.SaveChangesAsync();

    return "Default Images Created";
  }

  public async Task<long> NextImageId (long id)
  {
    var nextId = id + 1;
    if (await _context.Images.AnyAsync(i => i.Id == nextId)) {
      return nextId;
    }
    return 1;
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