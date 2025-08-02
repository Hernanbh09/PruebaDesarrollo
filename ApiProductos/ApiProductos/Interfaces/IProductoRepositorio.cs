using ApiProductos.Models;

namespace ApiProductos.Interfaces
{
    public interface IProductoRepositorio
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Producto entidad);
        Task<bool> ActualizarAsync(Producto entidad);
        Task<bool> EliminarAsync(int id);
    }
}
