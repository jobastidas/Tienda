using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tienda.Models; 

namespace Tienda.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly BaseDbContext _contexto;

        public List<Models.Productos> Productos { get; set; }

        public IndexModel(BaseDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> OnGet()
        {
            Productos = await _contexto.Productos.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var producto = await _contexto.Productos.FindAsync(id);

            if (producto != null)
            {
                _contexto.Productos.Remove(producto);
                await _contexto.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
