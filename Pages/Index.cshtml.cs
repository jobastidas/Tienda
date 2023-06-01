using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tienda.Models;

namespace Tienda.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BaseDbContext _dbContext;
        public Models.Productos ProductoMasVendido { get; set; }
        public Models.Productos ProductoMenosVendido { get; set; }
        public int CantidadTotalVendida { get; set; }
        public IndexModel(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            ProductoMasVendido = _dbContext.Ventas
               .GroupBy(v => v.ProductoId)
               .Select(g => new { ProductoId = g.Key, CantidadVendida = g.Sum(v => v.Cantidad) })
               .OrderByDescending(g => g.CantidadVendida)
               .Join(_dbContext.Productos,
                   venta => venta.ProductoId,
                   producto => producto.ProductoId,
                   (venta, producto) => new { Producto = producto, venta.CantidadVendida })
               .AsEnumerable() // Evaluar la consulta en el lado del cliente
               .FirstOrDefault()?.Producto;

            ProductoMenosVendido = _dbContext.Ventas
                 .GroupBy(v => v.ProductoId)
                 .Select(g => new { ProductoId = g.Key, CantidadVendida = g.Sum(v => v.Cantidad) })
                 .OrderBy(g => g.CantidadVendida)
                 .Join(_dbContext.Productos,
                     venta => venta.ProductoId,
                     producto => producto.ProductoId,
                     (venta, producto) => new { Producto = producto, venta.CantidadVendida })
                 .AsEnumerable() // Evaluar la consulta en el lado del cliente
                 .FirstOrDefault()?.Producto;
            CantidadTotalVendida = (int)_dbContext.Ventas.Sum(v => v.PrecioTotal);

        }
    }
}
