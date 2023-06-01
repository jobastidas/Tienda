using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Tienda.Models;

namespace Tienda.Pages.Venta
{
    public class CreateModel : PageModel
    {
        private readonly BaseDbContext _contexto;

        public CreateModel(BaseDbContext contexto)
        {
            _contexto = contexto;
        }                     

        [BindProperty]
        public Models.Venta Venta { get; set; }

        public List<Models.Productos> Productos { get; set; }

        public void OnGet()
        {
            Productos = _contexto.Productos.ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Productos = _contexto.Productos.ToList();
                return Page();
            }

            _contexto.Ventas.Add(Venta);
            _contexto.SaveChanges();

            return RedirectToPage("/Venta/Index");
        }
    }
}
