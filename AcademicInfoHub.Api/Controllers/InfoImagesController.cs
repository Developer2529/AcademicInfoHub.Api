using AcademicInfoHub.Api.Data;
using AcademicInfoHub.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class InfoImagesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public InfoImagesController(
        AppDbContext context,
        IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // DELETE: api/infoimages/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInfoImage(int id)
    {
        var image = await _context.InfoImages.FindAsync(id);

        if (image == null)
            return NotFound("Imagen no encontrada");

        // 🗂️ Ruta física del archivo
        var physicalPath = Path.Combine(
            _environment.WebRootPath,
            image.ImageUrl.TrimStart('/')
        );

        // 🧹 Eliminar archivo si existe
        if (System.IO.File.Exists(physicalPath))
        {
            System.IO.File.Delete(physicalPath);
        }

        // 🗑️ Eliminar registro de BD
        _context.InfoImages.Remove(image);
        await _context.SaveChangesAsync();

        return NoContent(); // 204
    }
}
