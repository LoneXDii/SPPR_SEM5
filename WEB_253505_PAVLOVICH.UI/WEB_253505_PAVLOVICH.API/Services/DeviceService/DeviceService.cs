using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WEB_253505_PAVLOVICH.API.Data;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;

namespace WEB_253505_PAVLOVICH.API.Services.DeviceService;

public class DeviceService : IDeviceService
{
    private readonly AppDbContext _dbContext;
    private readonly int _maxPageSize = 20;

    public DeviceService(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public async Task<ResponseData<ProductListModel<Device>>> GetDeviceListAsync(string? categoryNormalizedName, int pageNo = 1,
                                                                           int pageSize = 3)
    {
        if (pageSize > _maxPageSize)
            pageSize = _maxPageSize;

        var query = _dbContext.Devices.AsQueryable();
        var dataList = new ProductListModel<Device>();

        query = query.Where(d => categoryNormalizedName == null 
                            || d.Category!.NormalizedName.Equals(categoryNormalizedName));

        var count = await query.CountAsync();
        if (count == 0)
        {
            return ResponseData<ProductListModel<Device>>.Success(dataList);
        }

        int totalPages = (int)Math.Ceiling(count / (double)pageSize);
        if (pageNo > totalPages)
        {
            return ResponseData<ProductListModel<Device>>.Error("No such page");
        }

        dataList.Items = await query.OrderBy(d => d.Id)
                                    .Skip((pageNo - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
        dataList.CurrentPage = pageNo;
        dataList.TotalPages = totalPages;

        return ResponseData<ProductListModel<Device>>.Success(dataList);
    }

    public Task<ResponseData<Device>> CreateDeviceAsync(Device device)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDeviceAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<Device>> GetDeviceByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDeviceAsync(int id, Device device)
    {
        throw new NotImplementedException();
    }
}
