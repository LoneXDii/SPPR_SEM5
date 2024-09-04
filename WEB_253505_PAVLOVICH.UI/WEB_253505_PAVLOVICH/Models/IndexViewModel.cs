using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_253505_PAVLOVICH.Models;

public class IndexViewModel
{
    public int SelectedId { get; set; }
    public SelectList? ListItems { get; set; }
}
