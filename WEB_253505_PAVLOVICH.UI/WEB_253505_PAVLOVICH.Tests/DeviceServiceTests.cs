using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WEB_253505_PAVLOVICH.API.Data;
using WEB_253505_PAVLOVICH.API.Services.DeviceService;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;

namespace WEB_253505_PAVLOVICH.Tests;

public class DeviceServiceTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;
    public DeviceServiceTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                                    .UseInMemoryDatabase("DeviceServiceTests")
                                    .Options;
    }

    [Fact]
    public void Handle_ValidRequest_ShouldReturnPaginatedListWith3ItemsAndCorrectTotalPagesCount()
    {
        // Arrange
        using var context = CreateContext();
        var service = new DeviceService(context);

        // Act
        var result = service.GetDeviceListAsync(null).Result;

        // Assert
        Assert.IsType<ResponseData<ProductListModel<Device>>>(result);
        Assert.True(result.Successfull);
        Assert.Equal(1, result.Data.CurrentPage);
        Assert.Equal(3, result.Data.Items.Count);
        Assert.Equal(2, result.Data.TotalPages);
        Assert.Equal(context.Devices.First(), result.Data.Items[0]);
    }

    [Fact]
    public void Handle_ValidReuqest_ShouldCorrectlyChooseGivenPage()
    {
        // Arrange
        using var context = CreateContext();
        var service = new DeviceService(context);
        int pageNo = 2;

        // Act
        var result = service.GetDeviceListAsync(null, pageNo: 2).Result;

        // Assert
        Assert.IsType<ResponseData<ProductListModel<Device>>>(result);
        Assert.True(result.Successfull);
        Assert.Equal(2, result.Data.CurrentPage);
    }

    [Fact]
    public void Handle_ValidRequest_ShouldCorrectlyFilterByCategory()
    {
        // Arrange
        using var context = CreateContext();
        var service = new DeviceService(context);
        string category = "name-1";

        // Act
        var result = service.GetDeviceListAsync(category).Result;

        // Assert
        Assert.IsType<ResponseData<ProductListModel<Device>>>(result);
        Assert.True(result.Successfull);
        Assert.Equal(2, result.Data.Items.Count);
    }

    [Fact]
    public void Handle_SetPageSizeGreaterThanMaximum_ShouldNotAllowSet()
    {
        // Arrange
        using var context = CreateContext();
        var service = new DeviceService(context);
        int pageSize = 54;

        // Act
        var result = service.GetDeviceListAsync(null, pageSize: pageSize).Result;

        // Assert
        Assert.IsType<ResponseData<ProductListModel<Device>>>(result);
        Assert.True(result.Successfull);
        Assert.True((int)Math.Ceiling(result.Data.Items.Count / (double)result.Data.TotalPages) != pageSize);
    }

    [Fact]
    public void Handle_PageNoGreaterThanMaximumRequest_ReturnsSuccesfullIsFalse()
    {
        // Arrange
        using var context = CreateContext();
        var service = new DeviceService(context);
        int pageNo = 54;

        // Act
        var result = service.GetDeviceListAsync(null, pageNo: pageNo).Result;

        // Assert
        Assert.IsType<ResponseData<ProductListModel<Device>>>(result);
        Assert.False(result.Successfull);
    }

    private AppDbContext CreateContext()
    {
        var context = new AppDbContext(_dbContextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Devices.AddRange(
            new Device { Description = "Descr1", Price = 1, CategoryId = 1, Name = "Device1" },
            new Device { Description = "Descr2", Price = 2, CategoryId = 2, Name = "Device2" },
            new Device { Description = "Descr3", Price = 3, CategoryId = 3, Name = "Device3" },
            new Device { Description = "Descr4", Price = 4, CategoryId = 1, Name = "Device4" },
            new Device { Description = "Descr5", Price = 5, CategoryId = 2, Name = "Device5" },
            new Device { Description = "Descr6", Price = 6, CategoryId = 3, Name = "Device6" }
        );

        context.Categories.AddRange(
            new Category { Id = 1, Name = "Name1", NormalizedName = "name-1" },
            new Category { Id = 2, Name = "Name2", NormalizedName = "name-2" },
            new Category { Id = 3, Name = "Name3", NormalizedName = "name-3" });

        context.SaveChanges();

        return context;
    }
}
