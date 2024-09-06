using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;

namespace WEB_253505_PAVLOVICH.API.Services.DeviceService;

public interface IDeviceService
{
    public Task<ResponseData<ProductListModel<Device>>> GetDeviceListAsync(string? categoryNormalizedName, int pageNo = 1,
                                                                           int pageSize = 3);
    public Task<ResponseData<Device>> GetDeviceByIdAsync(int id);
    public Task UpdateDeviceAsync(int id, Device device);
    public Task DeleteDeviceAsync(int id);
    public Task<ResponseData<Device>> CreateDeviceAsync(Device device);
    public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
}
