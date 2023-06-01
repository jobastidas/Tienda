using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Tienda.Models;

namespace Tienda.Pages.Venta
{
    public class EditModel : PageModel
    {
        private readonly BaseDbContext _contexto;

        public EditModel(BaseDbContext contexto)
        {
            _contexto = contexto;
        }

        [BindProperty]
        public Models.Venta Venta { get; set; }

        public List<Models.Productos> Productos { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Venta = _contexto.Ventas.Find(id);
            if (Venta == null)
            {
                return NotFound();
            }
            Productos = _contexto.Productos.ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Productos = _contexto.Productos.ToList();
                return Page();
            }

            _contexto.Attach(Venta).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _contexto.SaveChanges();

            return RedirectToPage("/Venta/Index");
        }
    }
}
