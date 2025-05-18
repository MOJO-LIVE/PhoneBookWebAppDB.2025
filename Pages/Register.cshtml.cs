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

    // AJAX: проверка логина
    public JsonResult OnGetCheckUsername(string username)
    {
        bool exists = _context.Users.Any(u => u.Username == username);
        return new JsonResult(new { exists });
    }

    // AJAX: регистрация
    public JsonResult OnPostRegisterAjax([FromBody] RegisterDto dto)
    {
        if (dto == null ||
            string.IsNullOrWhiteSpace(dto.Username) ||
            string.IsNullOrWhiteSpace(dto.Password))
        {
            return new JsonResult(new { success = false, message = "Введите логин и пароль." });
        }

        if (_context.Users.Any(u => u.Username == dto.Username))
        {
            return new JsonResult(new { success = false, message = "Пользователь с таким именем уже существует." });
        }

        _context.Users.Add(new User
        {
            Username = dto.Username,
            Password = dto.Password,
            Role = "User"
        });
        _context.SaveChanges();

        return new JsonResult(new { success = true, message = "Регистрация успешна!" });
    }

    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
