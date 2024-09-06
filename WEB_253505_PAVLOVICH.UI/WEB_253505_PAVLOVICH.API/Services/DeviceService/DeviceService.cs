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

    public async Task<ResponseData<Device>> GetDeviceByIdAsync(int id)
    {
        var device = await _dbContext.Devices.FirstOrDefaultAsync(d => d.Id == id);

        if (device is null)
        {
            return ResponseData<Device>.Error($"No device with id={id}");
        }

        return ResponseData<Device>.Success(device);
    }

    public async Task<ResponseData<Device>> CreateDeviceAsync(Device device)
    {
        var newDevice = await _dbContext.Devices.AddAsync(device);
        await _dbContext.SaveChangesAsync();

        return ResponseData<Device>.Success(newDevice.Entity);
    }

    public async Task DeleteDeviceAsync(int id)
    {
        var device = await _dbContext.Devices.FirstOrDefaultAsync(d => d.Id == id);

        if(device is null)
        {
            return;
        }

        _dbContext.Remove(device);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateDeviceAsync(int id, Device device)
    {
        var dbDevice = await _dbContext.Devices.FirstOrDefaultAsync(d => d.Id == id);

        if (dbDevice is null)
        {
            return;
        }

        dbDevice.Price = device.Price;
        dbDevice.Description = device.Description;
        dbDevice.Category = device.Category;
        dbDevice.Name = device.Name;
        dbDevice.CategoryId = device.CategoryId;
        dbDevice.Image = device.Image;

        _dbContext.Entry(dbDevice).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
    {
        throw new NotImplementedException();
    }
}
