using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB_253505_PAVLOVICH.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly string _imagePath;

    public FilesController(IWebHostEnvironment webHost)
    {
        _imagePath = Path.Combine(webHost.WebRootPath, "Images");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SaveFile(IFormFile file)
    {
        if (file is null) 
        {
            return BadRequest();
        }

        var filePath = Path.Combine(_imagePath, file.FileName);
        var fileInfo = new FileInfo(filePath);
 
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }

        using var fileStream = fileInfo.Create();
        await file.CopyToAsync(fileStream);

        var host = HttpContext.Request.Host;
        var fileUrl = $"Https://{host}/Images/{file.FileName}";
        return Ok(fileUrl);
    }

    [HttpDelete("{fileName}")]
    [Authorize(Policy = "admin")]
    public IActionResult DeleteFile(string fileName) 
    {
        var filePath = Path.Combine(_imagePath, fileName);
        var fileInfo = new FileInfo(filePath);

        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }
        return Ok();
    }
}
