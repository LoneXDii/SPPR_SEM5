using System.Text.Json.Serialization;
using WEB_253505_PAVLOVICH.Domain.Cart;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.UI.Extensions;

namespace WEB_253505_PAVLOVICH.UI.Services.CartService;

public class SessionCart : Cart
{
    [JsonIgnore]
    public ISession? Session { get; set; }

    public static Cart GetCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
        SessionCart cart = session.Get<SessionCart>("cart") ?? new SessionCart();
        cart.Session = session;
        return cart;
    }

    public override void AddToCart(Device device)
    {
        base.AddToCart(device);
        Session?.Set<SessionCart>("cart", this);
    }
    public override void RemoveItems(int id)
    {
        base.RemoveItems(id);
        Session?.Set<SessionCart>("cart", this);
    }
    public override void ClearAll()
    {
        base.ClearAll();
        Session?.Remove("cart");
    }
}
