using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_253505_PAVLOVICH.API.Data;
using WEB_253505_PAVLOVICH.API.Services.DeviceService;
using WEB_253505_PAVLOVICH.Domain.Entities;

namespace WEB_253505_PAVLOVICH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceServcie;

        public DevicesController(IDeviceService deviceServcie)
        {
            _deviceServcie = deviceServcie;
        }

        [HttpGet]
        [Route("{category?}")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices(string? category, int pageNo = 1, 
                                                                        int pageSize = 3)
        {
            return Ok(await _deviceServcie.GetDeviceListAsync(category, pageNo, pageSize));
        }

        [HttpGet("{id:int}")]
        public Task<ActionResult<Device>> GetDevice(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public Task<IActionResult> PutDevice(int id, Device device)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<ActionResult<Device>> PostDevice(Device device)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteDevice(int id)
        {
            throw new NotImplementedException();
        }

        private bool DeviceExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
