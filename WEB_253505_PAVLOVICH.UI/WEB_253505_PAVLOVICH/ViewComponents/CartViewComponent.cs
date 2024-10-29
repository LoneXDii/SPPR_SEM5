using Microsoft.AspNetCore.Mvc;
using WEB_253505_PAVLOVICH.Domain.Cart;
using WEB_253505_PAVLOVICH.UI.Extensions;

namespace WEB_253505_PAVLOVICH.ViewComponents;

public class CartViewComponent : ViewComponent
{
    public CartViewComponent()
    {
    }

    public Task<IViewComponentResult> InvokeAsync()
    {
        Cart cart = HttpContext.Session.Get<Cart>("cart") ?? new();
        return Task.FromResult<IViewComponentResult>(View(cart));
    }
}
