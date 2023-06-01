using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tienda.Models;

namespace Tienda.Pages.Productos
{
    public class CreateModel : PageModel
    {
        private readonly BaseDbContext _contexto;

        [BindProperty]
        public Models.Productos Producto { get; set; }

        public CreateModel(BaseDbContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _contexto.Productos.Add(Producto);
            _contexto.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}