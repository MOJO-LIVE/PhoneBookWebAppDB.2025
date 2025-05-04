using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

public class AssignRoleModel : PageModel
{
    private readonly AppDbContext _context;
    public AssignRoleModel(AppDbContext context) => _context = context;

    public List<User> Users { get; set; } = new();

    [BindProperty]
    public int SelectedUserId { get; set; }

    public string Message { get; set; } = "";

    public void OnGet()
    {
        Users = _context.Users.OrderBy(u => u.Id).ToList();
    }

    public IActionResult OnPost()
    {
        var user = _context.Users.Find(SelectedUserId);
        if (user != null && user.Role != "Admin")
        {
            user.Role = "Admin";
            _context.SaveChanges();
            Message = $"Пользователь {user.Username} теперь Администратор.";
        }

        Users = _context.Users.OrderBy(u => u.Id).ToList();
        return Page();
    }
}
