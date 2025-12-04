using Microsoft.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;

public static class RecomendadorBD
{
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

    /// <summary>
    /// Obtiene el IdUsuario numérico desde el Usuario (string).
    /// Debe ser igual al método en LogBD para consistencia.
    /// </summary>
    private static int? ObtenerIdUsuario(string usuario)
    {
        if (string.IsNullOrEmpty(usuario))
            return null;

        if (int.TryParse(usuario, out int idNumerico))
        {
            return idNumerico;
        }

        return Math.Abs(usuario.GetHashCode());
    }

    /// <summary>
    /// Obtiene recomendaciones personalizadas basadas en el historial de acciones del usuario.
    /// Algoritmo: Asigna pesos por tipo de acción y calcula ranking de prendas.
    /// </summary>
    public static List<Prenda> ObtenerRecomendaciones(string usuario)
    {
        if (string.IsNullOrEmpty(usuario))
        {
            return new List<Prenda>();
        }

        int? idUsuario = ObtenerIdUsuario(usuario);
        if (!idUsuario.HasValue)
        {
            return new List<Prenda>();
        }

        try
        {
            // Obtener los logs del usuario
            string queryLogs = @"
                SELECT TipoAccion, Detalle
                FROM LogAcciones
                WHERE IdUsuario = @IdUsuario
                AND TipoAccion IN ('VisitaPrenda', 'AgregaDeseado', 'AgregaPoseido')
                ORDER BY Fecha DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Primero obtener las prendas que el usuario ya posee para excluirlas
                string queryPoseidos = @"SELECT IdPrenda FROM Poseido WHERE Usuario = @Usuario";
                var prendasPoseidas = connection.Query<int>(queryPoseidos, new { Usuario = usuario }).ToHashSet();

                var logs = connection.Query<dynamic>(queryLogs, new { IdUsuario = idUsuario.Value }).ToList();

                if (!logs.Any())
                {
                    return new List<Prenda>(); // Sin historial, no hay recomendaciones
                }

                // Diccionario para almacenar los pesos por IdPrenda
                Dictionary<int, int> pesosPrendas = new Dictionary<int, int>();

                // Pesos por tipo de acción
                const int pesoVisitaPrenda = 1;
                const int pesoAgregaDeseado = 3;
                const int pesoAgregaPoseido = 5;

                // Procesar cada log
                foreach (var log in logs)
                {
                    int peso = 0;
                    string tipoAccion = log.TipoAccion;

                    // Asignar peso según el tipo de acción
                    switch (tipoAccion)
                    {
                        case "VisitaPrenda":
                            peso = pesoVisitaPrenda;
                            break;
                        case "AgregaDeseado":
                            peso = pesoAgregaDeseado;
                            break;
                        case "AgregaPoseido":
                            peso = pesoAgregaPoseido;
                            break;
                        default:
                            continue; // Ignorar otros tipos
                    }

                    // Intentar extraer IdPrenda del detalle JSON
                    if (log.Detalle != null)
                    {
                        try
                        {
                            var detalle = JsonConvert.DeserializeObject<dynamic>(log.Detalle);
                            if (detalle != null && detalle.IdPrenda != null)
                            {
                                int idPrenda = (int)detalle.IdPrenda;
                                
                                // Si la prenda ya está poseída, no agregarla o poner peso 0
                                if (prendasPoseidas.Contains(idPrenda))
                                {
                                    // Si ya existe en el diccionario, poner peso 0
                                    if (pesosPrendas.ContainsKey(idPrenda))
                                    {
                                        pesosPrendas[idPrenda] = 0;
                                    }
                                    // Si no existe, no agregarla en absoluto
                                    continue;
                                }
                                
                                if (pesosPrendas.ContainsKey(idPrenda))
                                {
                                    pesosPrendas[idPrenda] += peso;
                                }
                                else
                                {
                                    pesosPrendas[idPrenda] = peso;
                                }
                            }
                        }
                        catch
                        {
                            // Si falla el parseo, continuar con el siguiente log
                            continue;
                        }
                    }
                }

                // Filtrar prendas con peso 0 (poseídas) y ordenar por peso (mayor a menor)
                var prendasOrdenadas = pesosPrendas
                    .Where(p => p.Value > 0) // Excluir prendas con peso 0 (poseídas)
                    .OrderByDescending(p => p.Value)
                    .Take(10)
                    .ToList();

                if (!prendasOrdenadas.Any())
                {
                    return new List<Prenda>();
                }
                var idsPrendas = prendasOrdenadas.Select(p => p.Key).ToList();

                if (!idsPrendas.Any())
                {
                    return new List<Prenda>();
                }

                // Crear diccionario para mantener el orden
                var ordenPorId = new Dictionary<int, int>();
                for (int i = 0; i < idsPrendas.Count; i++)
                {
                    ordenPorId[idsPrendas[i]] = i;
                }

                // Obtener las prendas desde la base de datos
                // Excluir prendas ya poseídas
                string idsString = string.Join(",", idsPrendas);
                string idsPoseidosString = prendasPoseidas.Any() ? string.Join(",", prendasPoseidas) : "0";
                
                string queryPrendas = $@"
                    SELECT DISTINCT p.*
                    FROM Prenda p
                    WHERE p.IdPrenda IN ({idsString})
                    AND p.IdPrenda NOT IN ({idsPoseidosString})
                    AND p.mostrar = 1";

                var prendas = connection.Query<Prenda>(queryPrendas).ToList();

                // Ordenar según el orden original del ranking
                prendas = prendas.OrderBy(p => ordenPorId.ContainsKey(p.IdPrenda) ? ordenPorId[p.IdPrenda] : int.MaxValue).Take(10).ToList();

                // Si hay menos de 10, completar con prendas similares basadas en las categorías más vistas
                if (prendas.Count < 10 && prendas.Any())
                {
                    // Obtener estilos más frecuentes de las prendas vistas
                    string idsStringParaBuscar = string.Join(",", idsPrendas);
                    string idsStringYaRecomendadas = string.Join(",", prendas.Select(p => p.IdPrenda));
                    
                    int cantidadFaltante = 10 - prendas.Count;
                    string idsPoseidosStringComplemento = prendasPoseidas.Any() ? string.Join(",", prendasPoseidas) : "0";
                    
                    string queryComplemento = $@"
                        SELECT TOP {cantidadFaltante} p.*
                        FROM Prenda p
                        INNER JOIN EstiloXPrenda ep ON ep.IdPrenda = p.IdPrenda
                        WHERE p.IdPrenda NOT IN ({idsStringYaRecomendadas})
                        AND p.IdPrenda NOT IN ({idsPoseidosStringComplemento})
                        AND p.mostrar = 1
                        AND ep.IdEstilo IN (
                            SELECT TOP 3 ep2.IdEstilo
                            FROM Prenda p2
                            INNER JOIN EstiloXPrenda ep2 ON ep2.IdPrenda = p2.IdPrenda
                            WHERE p2.IdPrenda IN ({idsStringParaBuscar})
                            GROUP BY ep2.IdEstilo
                            ORDER BY COUNT(*) DESC
                        )
                        ORDER BY NEWID()";

                    var prendasComplemento = connection.Query<Prenda>(queryComplemento).ToList();
                    prendas.AddRange(prendasComplemento);
                }

                return prendas.Take(10).ToList();
            }
        }
        catch (Exception)
        {
            // En caso de error, retornar lista vacía para no romper el flujo
            return new List<Prenda>();
        }
    }
}

