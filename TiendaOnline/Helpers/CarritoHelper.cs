using System.Text.Json;
using TiendaOnline.Models;

namespace TiendaOnline.Helpers
{
    public class CarritoHelper
    {
        private const string SessionKey = "Carrito";

        // Obtener carrito desde sesión
        public static List<CarritoItem> ObtenerCarrito(ISession session)
        {
            var carritoJson = session.GetString(SessionKey);
            return string.IsNullOrEmpty(carritoJson)
                ? new List<CarritoItem>()
                : JsonSerializer.Deserialize<List<CarritoItem>>(carritoJson);
        }

        // Guardar carrito en sesión
        public static void GuardarCarrito(ISession session, List<CarritoItem> carrito)
        {
            session.SetString(SessionKey, JsonSerializer.Serialize(carrito));
        }

        // Vaciar carrito
        public static void VaciarCarrito(ISession session)
        {
            session.Remove(SessionKey);
        }
    }
}
