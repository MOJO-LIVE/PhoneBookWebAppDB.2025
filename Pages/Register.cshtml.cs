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
            Message = "������� ��� ������������ � ������.";
            return Page();
        }

        if (_context.Users.Any(u => u.Username == RegisterUsername))
        {
            Message = "������������ � ����� ������ ��� ����������.";
            return Page();
        }

        _context.Users.Add(new User
        {
            Username = RegisterUsername,
            Password = RegisterPassword,
            Role = "User"
        });
        _context.SaveChanges();

        Message = "����������� �������! ������ �� ������ �����.";
        return RedirectToPage("/Index");
    }

    // AJAX: �������� ������
    public JsonResult OnGetCheckUsername(string username)
    {
        bool exists = _context.Users.Any(u => u.Username == username);
        return new JsonResult(new { exists });
    }

    // AJAX: �����������
    public JsonResult OnPostRegisterAjax([FromBody] RegisterDto dto)
    {
        if (dto == null ||
            string.IsNullOrWhiteSpace(dto.Username) ||
            string.IsNullOrWhiteSpace(dto.Password))
        {
            return new JsonResult(new { success = false, message = "������� ����� � ������." });
        }

        if (_context.Users.Any(u => u.Username == dto.Username))
        {
            return new JsonResult(new { success = false, message = "������������ � ����� ������ ��� ����������." });
        }

        _context.Users.Add(new User
        {
            Username = dto.Username,
            Password = dto.Password,
            Role = "User"
        });
        _context.SaveChanges();

        return new JsonResult(new { success = true, message = "����������� �������!" });
    }

    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
