using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

public class RegisterModel : PageModel
{
    private readonly AppDbContext _context;
    public RegisterModel(AppDbContext context) => _context = context;

    [BindProperty]
    public string RegisterUsername { get; set; } = "";

    [BindProperty]
    public string RegisterPassword { get; set; } = "";

    public string Message { get; set; } = "";

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(RegisterUsername) || string.IsNullOrWhiteSpace(RegisterPassword))
        {
            Message = "Введите имя пользователя и пароль.";
            return Page();
        }

        if (_context.Users.Any(u => u.Username == RegisterUsername))
        {
            Message = "Пользователь с таким именем уже существует.";
            return Page();
        }

        _context.Users.Add(new User
        {
            Username = RegisterUsername,
            Password = RegisterPassword,
            Role = "User"
        });
        _context.SaveChanges();

        Message = "Регистрация успешна! Теперь вы можете войти.";
        return RedirectToPage("/Index");
    }
}
