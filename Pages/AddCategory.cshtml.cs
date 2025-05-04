using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class AddCategoryModel : PageModel
{
    private readonly AppDbContext _context;

    public AddCategoryModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Category Category { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Add(Category);
            _context.SaveChanges();
            return RedirectToPage("/Welcome");
        }
        return Page();
    }
}
