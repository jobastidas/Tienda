using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq; // Agrega esta directiva para usar el método Any
using System.Threading.Tasks;
using Tienda.Models;
namespace Tienda.Pages.Productos
{
    public class EditModel : PageModel
    {
        private readonly BaseDbContext _contexto;

        [BindProperty]
        public Models.Productos Producto { get; set; }

        public EditModel(BaseDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producto = await _contexto.Productos.FindAsync(id);

            if (Producto == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _contexto.Attach(Producto).State = EntityState.Modified;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(Producto.ProductoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }

        private bool ProductoExists(int id)
        {
            return _contexto.Productos.Any(e => e.ProductoId == id);
        }
    }
}