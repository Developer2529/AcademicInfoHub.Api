using AcademicInfoHub.Api.Data;
using AcademicInfoHub.Api.DTOs;
using AcademicInfoHub.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AcademicInfoHub.Api.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class InfoPostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InfoPostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/infoposts

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InfoPostFeedDto>>> GetInfoPosts()
        {
            var posts = await _context.InfoPosts
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new InfoPostFeedDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Summary = x.Summary,
                    Category = x.Category,
                    HasExternalRedirect = x.HasExternalRedirect,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            return Ok(posts);
        }

     



        // GET: api/infoposts/5

        [HttpGet("{id}")]
        
        public async Task<ActionResult<InfoPostResponseDto>> GetInfoPost(int id)
        {
            var post = await _context.InfoPosts
                .Include(x => x.Images)
                .Where(x => x.Id == id && x.IsActive)
                .Select(x => new InfoPostResponseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Summary = x.Summary,
                    Content = x.Content,
                    Category = x.Category,
                    ExternalUrl = x.ExternalUrl,
                    HasExternalRedirect = x.HasExternalRedirect,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Images = x.Images.Select(i => new InfoImageResponseDto
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (post == null)
                return NotFound();

            return Ok(post);
        }



         

        [HttpPost("{id}/images")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            var post = await _context.InfoPosts.FindAsync(id);

            if (post == null)
                return NotFound("InfoPost not found");

            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "uploads", "infoposts");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var image = new InfoImage
            {
                InfoPostId = id,
                ImageUrl = $"/uploads/infoposts/{fileName}",
                CreatedAt = DateTime.UtcNow
            };

            _context.InfoImages.Add(image);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                imageId = image.Id,
                imageUrl = image.ImageUrl
            });

        }

        // POST: api/InfoPosts
        //[AdminAuthorize]
        //[Authorize(Roles = "Admin")]
        [Authorize]

        [HttpPost]
        public async Task<IActionResult> CreateInfoPost([FromBody] InfoPostCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = new InfoPost
            {
                Title = dto.Title,
                Summary = dto.Summary,
                Content = dto.Content,
                Category = dto.Category,
                ExternalUrl = dto.ExternalUrl,
                HasExternalRedirect = dto.HasExternalRedirect,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.InfoPosts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetInfoPost),
                new { id = post.Id },
                new
                {
                    post.Id,
                    post.Title
                }
            );
        }

        //[AdminAuthorize]
        //[Authorize(Roles = "Admin")]

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInfoPost(int id, [FromBody] InfoPostUpdateDto dto)
        {
            var post = await _context.InfoPosts.FindAsync(id);

            if (post == null)
                return NotFound();

            post.Title = dto.Title;
            post.Summary = dto.Summary;
            post.Content = dto.Content;
            post.Category = dto.Category;
            post.ExternalUrl = dto.ExternalUrl;
            post.HasExternalRedirect = dto.HasExternalRedirect;
            post.IsActive = dto.IsActive;
            post.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //[AdminAuthorize]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfoPost(int id)
        {
            var post = await _context.InfoPosts.FindAsync(id);

            if (post == null)
                return NotFound();

            post.IsActive = false;
            post.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/images")]
        public async Task<IActionResult> GetImagesByInfoPost(int id)
        {
            var exists = await _context.InfoPosts.AnyAsync(x => x.Id == id);
            if (!exists)
                return NotFound("InfoPost no encontrado");

            var images = await _context.InfoImages
                .Where(i => i.InfoPostId == id)
                .Select(i => new
                {
                    id = i.Id,
                    imageUrl = i.ImageUrl
                })
                .ToListAsync();

            return Ok(images);
        }


    }
}
