namespace WEB_253505_PAVLOVICH.Domain.Entities;

public class Device
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Category? Category { get; set; }
    public int price { get; set; }
    public string? Image {  get; set; }
}
