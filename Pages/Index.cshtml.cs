using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string RegisterUsername { get; set; } = "";

    [BindProperty]
    public string RegisterPassword { get; set; } = "";

    [BindProperty]
    public string LoginUsername { get; set; } = "";

    [BindProperty]
    public string LoginPassword { get; set; } = "";

    [TempData]
    public string Message { get; set; } = "";

    public void OnGet() { }

    public IActionResult OnPostRegister()
    {
        if (string.IsNullOrWhiteSpace(RegisterUsername) || string.IsNullOrWhiteSpace(RegisterPassword))
        {
            Message = "Введите имя пользователя и пароль для регистрации.";
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

        Message = "Регистрация успешно завершена! Теперь вы можете войти.";
        return Page();
    }

    public IActionResult OnPostLogin()
    {
        if (string.IsNullOrWhiteSpace(LoginUsername) || string.IsNullOrWhiteSpace(LoginPassword))
        {
            Message = "Введите имя пользователя и пароль.";
            return Page();
        }

        var user = _context.Users.FirstOrDefault(u => u.Username == LoginUsername && u.Password == LoginPassword);
        if (user == null)
        {
            Message = "Неверное имя пользователя или пароль.";
            return Page();
        }

        TempData["Username"] = user.Username;
        TempData["Role"] = user.Role;
        return RedirectToPage("/Welcome");
    }
}