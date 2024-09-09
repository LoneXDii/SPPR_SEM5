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
    public async Task<ActionResult<Device>> GetDeviceAsync(int id)
    {
        var response = await _deviceService.GetDeviceByIdAsync(id);
        if (!response.Successfull)
        {
            return NotFound(response.ErrorMessage);
        }
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDeviceAsync(int id, Device device)
    {
        await _deviceService.UpdateDeviceAsync(id, device);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<Device>> PostDeviceAsync(Device device)
    {
        var response = await _deviceService.CreateDeviceAsync(device);
        if (!response.Successfull)
        {
            return BadRequest(response.ErrorMessage);
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDeviceAsync(int id)
    {
        await _deviceService.DeleteDeviceAsync(id);
        return Ok();
    }
}
