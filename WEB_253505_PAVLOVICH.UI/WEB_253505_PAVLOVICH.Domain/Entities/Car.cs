﻿namespace WEB_253505_PAVLOVICH.Domain.Entities;

internal class Car
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Category? Category { get; set; }
    public int Price { get; set; }
    public string? ImageUrl { get; set; }
}
