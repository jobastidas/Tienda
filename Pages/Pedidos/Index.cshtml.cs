using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tienda.Models;
using System.Collections.Generic;
namespace Tienda.Pages.Pedido
{
    public class IndexModel : PageModel
    {
        private readonly BaseDbContext _context;

        public IndexModel(BaseDbContext context)
        {
            _context = context;
        }

        public IList<Models.Pedido> Pedidos { get; set; }

        public async Task OnGetAsync()
        {
            Pedidos = await _context.Pedidos
                .Include(p => p.Producto)
                .Include(p => p.Venta)
                .ToListAsync();
        }

        [BindProperty]
        public Models.Pedido Pedido { get; set; }

        public async Task<IActionResult> OnGetDeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pedido = await _context.Pedidos
                .Include(p => p.Producto)
                .Include(p => p.Venta)
                .FirstOrDefaultAsync(m => m.PedidoId == id);

            if (Pedido == null)
            {
                return NotFound();
            }

            return Page();
        }

     
        public async Task<IActionResult> OnPostDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pedido = await _context.Pedidos.FindAsync(id);

            if (Pedido != null)
            {
                // Eliminar la venta asociada al pedido
                var venta = await _context.Ventas.FindAsync(Pedido.VentaId);
                if (venta != null)
                {
                    _context.Ventas.Remove(venta);
                }

                // Eliminar el pedido
                _context.Pedidos.Remove(Pedido);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        }
}
