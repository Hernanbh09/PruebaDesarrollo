using ApiProductos.Interfaces;
using ApiProductos.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiProductos.Repositories
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly string _cadenaConexion;
        public ProductoRepositorio(IConfiguration config)
        {
            _cadenaConexion = config.GetConnectionString("Conexion")!;
        }
        private IDbConnection ObtenerConexion() =>
            new SqlConnection(_cadenaConexion);

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            const string sql = "SELECT Id, Nombre, Precio FROM Productos;";
            using var conexion = ObtenerConexion();
            return await conexion.QueryAsync<Producto>(sql);
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            const string sql =
                "SELECT Id, Nombre, Precio FROM Productos WHERE Id = @Id;";
            using var conexion = ObtenerConexion();
            return await conexion.QueryFirstOrDefaultAsync<Producto>(sql, new { Id = id });
        }

        public async Task<int> CrearAsync(Producto entidad)
        {
            const string sql = @"
                INSERT INTO Productos (Nombre, Precio) 
                VALUES (@Nombre, @Precio);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using var conexion = ObtenerConexion();
            return await conexion.ExecuteScalarAsync<int>(sql, entidad);
        }

        public async Task<bool> ActualizarAsync(Producto entidad)
        {
            const string sql = @"
                UPDATE Productos 
                SET Nombre = @Nombre, Precio = @Precio 
                WHERE Id = @Id;";
            using var conexion = ObtenerConexion();
            var filas = await conexion.ExecuteAsync(sql, entidad);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string sql = "DELETE FROM Productos WHERE Id = @Id;";
            using var conexion = ObtenerConexion();
            var filas = await conexion.ExecuteAsync(sql, new { Id = id });
            return filas > 0;
        }
    }
}
