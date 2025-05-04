using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    public void OnGet(int id)
    {
        var contact = _context.Contacts.Find(id);
        if (contact != null)
        {
            Contact = contact;
        }
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            Message = "Проверьте правильность введённых данных.";
            return Page();
        }

        var existing = _context.Contacts.Find(Contact.Id);
        if (existing != null)
        {
            existing.Name = Contact.Name;
            existing.Phone = Contact.Phone;
            existing.Category = Contact.Category;
            _context.SaveChanges();
            Message = "Контакт успешно обновлён.";
        }

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
