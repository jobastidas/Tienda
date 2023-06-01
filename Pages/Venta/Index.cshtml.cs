using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tienda.Models;
namespace Tienda.Pages.Venta
{
    public class IndexModel : PageModel
    {
        private readonly BaseDbContext _contexto;

        public IndexModel(BaseDbContext contexto)
        {
            _contexto = contexto;
        }

        public List<Models.Venta> Ventas { get; set; }

        public void OnGet()
        {  
            Ventas = _contexto.Ventas.Include(v => v.Producto).ToList(); 
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var pedidos = await _contexto.Pedidos
                .Where(p => p.VentaId == id)
                .ToListAsync();

            if (pedidos == null || !pedidos.Any())
            {
                var Ventas = await _contexto.Ventas.FindAsync(id);

                if (Ventas != null)
                {
                    _contexto.Ventas.Remove(Ventas);
                    await _contexto.SaveChangesAsync();
                }
            }
            else
            {
                _contexto.Pedidos.RemoveRange(pedidos);
                await _contexto.SaveChangesAsync();

                var Ventas = await _contexto.Ventas.FindAsync(id);

                if (Ventas != null)
                {
                    _contexto.Ventas.Remove(Ventas);
                    await _contexto.SaveChangesAsync();
                }
            }

           


           

            return RedirectToPage();
        }
    }
}