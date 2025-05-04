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
        if (ModelState.IsValid)
        {
            _context.Contacts.Add(Contact);
            _context.SaveChanges();
            Message = "Абонент успешно добавлен!";
            return RedirectToPage("/Welcome");
        }
        else
        {
            Message = "Ошибка! Пожалуйста, заполните все поля.";
            return Page();
        }
    }
}
