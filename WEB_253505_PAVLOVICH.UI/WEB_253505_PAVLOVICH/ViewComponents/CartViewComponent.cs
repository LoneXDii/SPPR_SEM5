using Microsoft.AspNetCore.Mvc;

namespace WEB_253505_PAVLOVICH.ViewComponents;

public class CartViewComponent : ViewComponent
{
    public CartViewComponent()
    {
    }

    public Task<IViewComponentResult> InvokeAsync()
    {
        return Task.FromResult<IViewComponentResult>(View());
    }
}
