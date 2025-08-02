// Controllers/ProductosController.cs
using ApiProductos.Interfaces;
using ApiProductos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiProductos.Controllers
{
    [ApiController]                             // 1. Marca este controlador como API
    [Route("api/[controller]")]                 // 2. Define la ruta raíz: /api/productos
    public class ProductosController : ControllerBase  // 3. Hereda ControllerBase
    {
        private readonly IProductoServicio _servicio;

        public ProductosController(IProductoServicio servicio)
        {
            _servicio = servicio;
        }

        // GET api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> ObtenerTodos()
        {
            var productos = await _servicio.ObtenerTodosAsync();
            return Ok(productos);
        }

        // GET api/productos/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> ObtenerPorId(int id)
        {
            var producto = await _servicio.ObtenerPorIdAsync(id);
            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            return Ok(producto);
        }

        // POST api/productos
        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<Producto>> Crear([FromBody] CrearProducto dto)
        {
            try
            {
                var entidad = new Producto
                {
                    Nombre = dto.Nombre,
                    Precio = dto.Precio
                };

                var idGenerado = await _servicio.CrearAsync(entidad);

                entidad.Id = idGenerado;

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = idGenerado },
                    entidad
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/productos/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Producto productoActualizado)
        {
            try
            {
                var exito = await _servicio.ActualizarAsync(id, productoActualizado);
                if (!exito)
                    return NotFound(new { mensaje = "No existe el producto." });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        // DELETE api/productos/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var exito = await _servicio.EliminarAsync(id);
                if (!exito)
                    return NotFound(new { mensaje = "No existe el producto." });

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }
    }
}
