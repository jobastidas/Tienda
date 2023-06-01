using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tienda.Models;

namespace Tienda.Pages.Pedidos
{
    public class CreateModel : PageModel
    {
        private readonly BaseDbContext _context;

        public CreateModel(BaseDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Pedido Pedido { get; set; }
        [BindProperty]

        public List<Models.Productos> Productos { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Productos = await _context.Productos.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var venta = new Models.Venta();
            venta.ProductoId = Pedido.ProductoId;
            venta.PrecioTotal = Pedido.Total;
            venta.FechaVenta = Pedido.FechaPedido;
            venta.Cantidad = Pedido.Cantidad;
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            Pedido.VentaId = venta.VentaId;
            _context.Pedidos.Add(Pedido);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
