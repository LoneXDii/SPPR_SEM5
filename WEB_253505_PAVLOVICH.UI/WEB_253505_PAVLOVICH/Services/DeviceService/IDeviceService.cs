using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;

namespace WEB_253505_PAVLOVICH.UI.Services.DeviceService;

public interface IDeviceService
{
    public Task<ResponseData<ProductListModel<Device>>> GetDeviceListAsync(string? categoryNormalizedName, int pageNo = 1);
    public Task<ResponseData<Device>> GetDeviceByIdAsync(int id);
    public Task UpdateDeviceAsync(int id, Device device, IFormFile? formFile);
    public Task DeleteDeviceAsync(int id);
    public Task<ResponseData<Device>> CreateDeviceAsync(Device device, IFormFile?formFile);
}
