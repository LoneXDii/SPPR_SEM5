using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_253505_PAVLOVICH.Domain.Cart;
using WEB_253505_PAVLOVICH.UI.Extensions;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;
namespace WEB_253505_PAVLOVICH.UI.Controllers;

public class CartController : Controller
{
    private readonly IDeviceService _deviceService;
    private readonly Cart _cart;

    public CartController(IDeviceService deviceService, Cart cart)
    {
        _deviceService = deviceService;
        _cart = cart;
    }

    [Authorize]
    public ActionResult Index()
    {
        return View(_cart);
    }

    [Authorize]
    [Route("[controller]/add/{id:int}")]
    public async Task<ActionResult> Add(int id, string returnUrl)
    {
        var data = await _deviceService.GetDeviceByIdAsync(id);
        if (data.Successfull)
        {
            _cart.AddToCart(data.Data);
        }
        return Redirect(returnUrl);
    }

    [Authorize]
    [Route("[controller]/delete/{id:int}")]
    public ActionResult Delete(int id)
    {
        _cart.RemoveItems(id);
        return RedirectToAction("Index", "Cart");
    }
}