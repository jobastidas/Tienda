using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda.Models
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }

        [Required]
        public decimal Precio { get; set; }
    }

    public class Venta
    {
        [Key]
        public int VentaId { get; set; }
        [ForeignKey("ProductoId")]
        public int ProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioTotal { get; set; }

        [Required]
        public DateTime FechaVenta { get; set; }
        public virtual Productos Producto { get; set; }
    }

    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        [ForeignKey("ProductoId")]

        public int ProductoId { get; set; }
        [ForeignKey("VentaId")]

        public int VentaId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public DateTime FechaPedido { get; set; }

        [Required]
        public string UsuarioPedido { get; set; }
        [Required]
        public string NumeroTelefono { get; set; }
        [Required]
        public decimal Total { get; set; }
        public virtual Productos Producto { get; set; }
        public virtual Venta Venta { get; set; }
    }
}
