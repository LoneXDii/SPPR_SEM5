﻿using System.ComponentModel.DataAnnotations;

namespace WEB_253505_PAVLOVICH.Blazor.SSR.Models;

public class CounterModel
{
    [Range(1, 10)]
    public int Value { get; set; }
}