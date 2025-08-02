using ApiProductos.Models;

namespace ApiProductos.Interfaces
{
    public interface IProductoServicio
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Producto nuevo);
        Task<bool> ActualizarAsync(int id, Producto actualizado);
        Task<bool> EliminarAsync(int id);
    }
}
