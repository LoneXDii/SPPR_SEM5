using WEB_253505_PAVLOVICH.Domain.Entities;

namespace WEB_253505_PAVLOVICH.Domain.Cart;

public class Cart
{
    public Dictionary<int, CartItem> CartItems { get; set; } = new();

    public virtual void AddToCart(Device device)
    {
        var exists = CartItems.ContainsKey(device.Id);
        if (exists)
        {
            CartItems[device.Id].Count++;
        }
        else
        {
            CartItems.Add(device.Id, new CartItem { Count = 1, Device = device });
        }
    }

    public virtual void RemoveItems(int id)
    {
        CartItems.Remove(id);
    }

    public virtual void ClearAll()
    {
        CartItems.Clear();
    }

    public int Count { get => CartItems.Sum(item => item.Value.Count);  }

    public double TotalCost { get => CartItems.Sum(item 
        => item.Value.Device.Price * item.Value.Count); }
}
