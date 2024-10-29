using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_253505_PAVLOVICH.Domain.Cart;
using WEB_253505_PAVLOVICH.UI.Extensions;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

namespace WEB_253505_PAVLOVICH.UI.Controllers;

public class CartController : Controller
{
    private readonly IDeviceService _deviceService;

    public CartController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [Authorize]
    [Route("[controller]/add/{id:int}")]
    public async Task<ActionResult> Add(int id, string returnUrl)
    {
        Cart cart = HttpContext.Session.Get<Cart>("cart") ?? new();
        var data = await _deviceService.GetDeviceByIdAsync(id);
        if (data.Successfull)
        {
            cart.AddToCart(data.Data);
            HttpContext.Session.Set<Cart>("cart", cart);
        }
        return Redirect(returnUrl);
    }
}
