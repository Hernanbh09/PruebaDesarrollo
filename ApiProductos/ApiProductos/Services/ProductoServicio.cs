// Services/ProductoServicio.cs
using ApiProductos.Interfaces;
using ApiProductos.Models;

namespace ApiProductos.Services
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IProductoRepositorio _repositorio;

        public ProductoServicio(IProductoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<IEnumerable<Producto>> ObtenerTodosAsync()
            => _repositorio.ObtenerTodosAsync();

        public Task<Producto?> ObtenerPorIdAsync(int id)
            => _repositorio.ObtenerPorIdAsync(id);

        public async Task<int> CrearAsync(Producto nuevo)
        {
            if (string.IsNullOrWhiteSpace(nuevo.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (nuevo.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            return await _repositorio.CrearAsync(nuevo);
        }

        public async Task<bool> ActualizarAsync(int id, Producto actualizado)
        {
            var existente = await _repositorio.ObtenerPorIdAsync(id)
                ?? throw new KeyNotFoundException("Producto no encontrado.");

            if (string.IsNullOrWhiteSpace(actualizado.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (actualizado.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            actualizado.Id = id;
            return await _repositorio.ActualizarAsync(actualizado);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existe = await _repositorio.ObtenerPorIdAsync(id);
            if (existe is null)
                throw new KeyNotFoundException("Producto no encontrado.");

            return await _repositorio.EliminarAsync(id);
        }
    }
}
