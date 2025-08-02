using ClienteWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ClienteWeb.Controllers
{
    public class ProductosController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductosController(IHttpClientFactory httpFactory)
        {
            _httpClient = httpFactory.CreateClient("ApiProductos");
        }
        public async Task<IActionResult> Index()
        {
            var respuesta = await _httpClient.GetAsync("");
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var productos = JsonSerializer.Deserialize<IEnumerable<Producto>>(contenido,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            return View(productos);
        }
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var json = JsonSerializer.Serialize(dto);
            var respuesta = await _httpClient.PostAsync("",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al crear producto.");
                return View(dto);
            }
            TempData["MensajeExito"] = "Producto creado correctamente.";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Editar(int id)
        {
            var respuesta = await _httpClient.GetAsync($"{id}");
            if (!respuesta.IsSuccessStatusCode)
                return NotFound();

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var producto = JsonSerializer.Deserialize<Producto>(contenido,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
           

            return View(producto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Producto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dto);

            var json = JsonSerializer.Serialize(dto);
            var respuesta = await _httpClient.PutAsync($"{id}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar producto.");
                return View(dto);
            }
            TempData["MensajeExito"] = "Producto editado correctamente.";
            
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Eliminar(int id)
        {
            var respuesta = await _httpClient.GetAsync($"{id}");
            if (!respuesta.IsSuccessStatusCode)
                return NotFound();

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var producto = JsonSerializer.Deserialize<Producto>(contenido,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            return View(producto);
        }
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"{id}");
            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al eliminar producto.");
                // Volver a obtener datos para mostrar en la vista
                return await Eliminar(id);
            }
            TempData["MensajeExito"] = "Producto eliminado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
