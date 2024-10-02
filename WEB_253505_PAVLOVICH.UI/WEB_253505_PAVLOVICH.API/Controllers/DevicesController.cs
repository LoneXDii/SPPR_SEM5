using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_253505_PAVLOVICH.API.Services.DeviceService;
using WEB_253505_PAVLOVICH.Domain.Entities;

namespace WEB_253505_PAVLOVICH.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DevicesController(IDeviceService deviceServcie)
    {
        _deviceService = deviceServcie;
    }

    [HttpGet]
    [Route("{category?}")]
    public async Task<ActionResult<IEnumerable<Device>>> GetDevices(string? category, int pageNo = 1, 
                                                                    int pageSize = 3)
    {
        return Ok(await _deviceService.GetDeviceListAsync(category, pageNo, pageSize));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Device>> GetDevice(int id)
    {
        var response = await _deviceService.GetDeviceByIdAsync(id);
        if (!response.Successfull)
        {
            return NotFound(response.ErrorMessage);
        }
        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> PutDevice(int id, Device device)
    {
        await _deviceService.UpdateDeviceAsync(id, device);
        return Ok();
    }

    [HttpPost]
    [Authorize(Policy = "admin")]
    public async Task<ActionResult<Device>> PostDevice(Device device)
    {
        var response = await _deviceService.CreateDeviceAsync(device);
        if (!response.Successfull)
        {
            return BadRequest(response.ErrorMessage);
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> DeleteDevice(int id)
    {
        await _deviceService.DeleteDeviceAsync(id);
        return Ok();
    }
}
