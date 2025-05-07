// AddContactModel.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class AddContactModel : PageModel
{
    private readonly AppDbContext _context;

    public AddContactModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Contact Contact { get; set; }

    public string Message { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Contact.Name) ||
            string.IsNullOrWhiteSpace(Contact.Phone) ||
            string.IsNullOrWhiteSpace(Contact.Category))
        {
            Message = "Ошибка! Все поля должны быть заполнены.";
            return Page();
        }
        Contact.OwnerUsername = TempData["Username"]?.ToString() ?? "";

        _context.Contacts.Add(Contact);
        _context.SaveChanges();
        Message = "Абонент успешно добавлен!";
        return RedirectToPage("/Welcome");
    }
}