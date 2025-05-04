using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
[ResponseCache(Duration=0,Location=ResponseCacheLocation.None,NoStore=true)]
public class ErrorModel : PageModel
{
    public string RequestId => Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    public void OnGet() {}
}