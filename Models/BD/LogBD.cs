using Microsoft.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;

public static class LogBD
{
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

    /// <summary>
    /// Obtiene el IdUsuario numérico desde el Usuario (string).
    /// Como la tabla LogAcciones espera INT pero Comprador usa Usuario (string) como PK,
    /// necesitamos mapear. Si Usuario es numérico, lo convertimos; sino usamos un hash.
    /// </summary>
    private static int? ObtenerIdUsuario(string usuario)
    {
        if (string.IsNullOrEmpty(usuario))
            return null;

        // Intentar convertir directamente si es numérico
        if (int.TryParse(usuario, out int idNumerico))
        {
            return idNumerico;
        }

        // Si no es numérico, generar un hash consistente
        // Usamos el valor absoluto del hash para evitar negativos
        return Math.Abs(usuario.GetHashCode());
    }

    /// <summary>
    /// Registra una acción del usuario en la tabla LogAcciones.
    /// Si idUsuario es null o no hay sesión, no registra nada.
    /// </summary>
    public static void RegistrarAccion(string usuario, string tipoAccion, object detalle = null)
    {
        if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(tipoAccion))
        {
            return; // No registrar si no hay usuario o tipo de acción
        }

        int? idUsuario = ObtenerIdUsuario(usuario);
        if (!idUsuario.HasValue)
        {
            return; // No registrar si no se puede obtener un ID
        }

        try
        {
            string detalleJson = null;
            if (detalle != null)
            {
                // Serializar el detalle y luego agregar el usuario
                string detalleOriginal = JsonConvert.SerializeObject(detalle);
                var detalleObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(detalleOriginal);
                if (detalleObj == null)
                {
                    detalleObj = new Dictionary<string, object>();
                }
                detalleObj["Usuario"] = usuario;
                detalleJson = JsonConvert.SerializeObject(detalleObj);
            }
            else
            {
                // Incluso si no hay detalle específico, guardar el usuario
                detalleJson = JsonConvert.SerializeObject(new { Usuario = usuario });
            }

            string query = @"
                INSERT INTO LogAcciones (IdUsuario, TipoAccion, Detalle, Fecha)
                VALUES (@IdUsuario, @TipoAccion, @Detalle, GETDATE())";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new
                {
                    IdUsuario = idUsuario.Value,
                    TipoAccion = tipoAccion,
                    Detalle = detalleJson
                });
            }
        }
        catch (Exception)
        {
            // Silenciosamente fallar para no romper el flujo existente
            // En producción, podrías loggear el error
        }
    }

    /// <summary>
    /// Obtiene las últimas acciones de un usuario.
    /// </summary>
    public static List<LogAccion> ObtenerAccionesPorUsuario(string usuario, int limit = 100)
    {
        if (string.IsNullOrEmpty(usuario))
        {
            return new List<LogAccion>();
        }

        int? idUsuario = ObtenerIdUsuario(usuario);
        if (!idUsuario.HasValue)
        {
            return new List<LogAccion>();
        }

        try
        {
            string query = @"
                SELECT TOP (@Limit) 
                    IdLog, 
                    IdUsuario, 
                    Fecha, 
                    TipoAccion, 
                    Detalle
                FROM LogAcciones
                WHERE IdUsuario = @IdUsuario
                ORDER BY Fecha DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var logs = connection.Query<LogAccion>(query, new
                {
                    IdUsuario = idUsuario.Value,
                    Limit = limit
                }).ToList();

                return logs;
            }
        }
        catch (Exception)
        {
            return new List<LogAccion>();
        }
    }
}

/// <summary>
/// Modelo para representar un log de acción.
/// </summary>
public class LogAccion
{
    public int IdLog { get; set; }
    public int IdUsuario { get; set; }
    public DateTime Fecha { get; set; }
    public string TipoAccion { get; set; }
    public string Detalle { get; set; }
}

