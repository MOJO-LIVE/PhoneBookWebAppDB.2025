using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

public class WelcomeModel : PageModel
{
    private readonly AppDbContext _context;

    public WelcomeModel(AppDbContext context)
    {
        _context = context;
    }

    public string Username { get; set; } = "";
    public string Role { get; set; } = "";

    public List<Contact> Contacts { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string Search { get; set; } = "";

    public void OnGet()
    {
        Username = TempData["Username"]?.ToString() ?? "Гость";
        Role = TempData["Role"]?.ToString() ?? "User";

        // Базовый запрос
        var query = _context.Contacts.AsQueryable();

        // Если не админ — фильтруем по владельцу
        if (Role != "Admin")
        {
            query = query.Where(c => c.OwnerUsername == Username);
        }

        // Поиск по строке
        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(c =>
                c.Name.Contains(Search) ||
                c.Phone.Contains(Search) ||
                c.Category.Contains(Search));
        }

        Contacts = query.ToList();

        // Чтобы TempData не пропала
        TempData["Username"] = Username;
        TempData["Role"] = Role;
    }

}
