

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautyParlourManagementAPI.Data;

public class AboutUsController : Controller
{
    private readonly BeautyParlourManagementContext _context;

    public AboutUsController(BeautyParlourManagementContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var services = await _context.Services.ToListAsync();
        var products = await _context.Products.ToListAsync();

        var model = new AboutUsViewModel
        {
            Services = services,
            Products = products
        };

        return View(model);
    }

}
