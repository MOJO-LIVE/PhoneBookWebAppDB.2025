using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

public class EditContactModel : PageModel
{
    private readonly AppDbContext _context;

    public EditContactModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Contact Contact { get; set; } = new();

    public string Message { get; set; } = "";
    public string Username { get; set; } = "";
    public string Role { get; set; } = "User";

    public void OnGet(int id)
    {
        Username = TempData["Username"]?.ToString() ?? "";
        Role = TempData["Role"]?.ToString() ?? "User";

        var contact = _context.Contacts.Find(id);
        if (contact != null)
        {
            // Только если админ или владелец
            if (Role == "Admin" || contact.OwnerUsername == Username)
            {
                Contact = contact;
            }
        }

        TempData["Username"] = Username;
        TempData["Role"] = Role;
    }

    public IActionResult OnPost()
    {
        Username = TempData["Username"]?.ToString() ?? "";
        Role = TempData["Role"]?.ToString() ?? "User";

        var existing = _context.Contacts.Find(Contact.Id);
        if (existing == null) return RedirectToPage("/Welcome");

        // Только если админ или владелец
        if (Role != "Admin" && existing.OwnerUsername != Username)
        {
            return RedirectToPage("/Welcome");
        }

        existing.Name = Contact.Name;
        existing.Phone = Contact.Phone;
        existing.Category = Contact.Category;

        _context.SaveChanges();

        TempData["Username"] = Username;
        TempData["Role"] = Role;
        return RedirectToPage("/Welcome");
    }

    public IActionResult OnPostDelete()
    {
        var contact = _context.Contacts.Find(Contact.Id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }

        return RedirectToPage("/Welcome");
    }
}
