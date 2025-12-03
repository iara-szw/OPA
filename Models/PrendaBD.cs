using Microsoft.Data.SqlClient;
using Dapper;
using System.Text.Json;
public class PrendaBD{
    public static string connectionString = @"Server=localhost\SQLEXPRESS01; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

    static public void agregarPrenda(int idTienda, int tipoInt, string modelo, string Descripcion, double precio, List<int> Estilos, List<int> colores, List<int> talles, int temporada, string Foto, string variantStockJson){
        while (Estilos.Count < 3) {
           Estilos.Add(-1);
        }
        Dictionary<int, Dictionary<int,int>> stockMap = new Dictionary<int, Dictionary<int,int>>();
        if (!string.IsNullOrEmpty(variantStockJson)){
            try{
                using(var doc = JsonDocument.Parse(variantStockJson)){
                    foreach(var prop in doc.RootElement.EnumerateObject()){
                        if (!int.TryParse(prop.Name, out var colorId)) continue;
                        var inner = new Dictionary<int,int>();
                        foreach(var item in prop.Value.EnumerateArray()){
                            int talleId = item.GetProperty("talleId").GetInt32();
                            int stock = item.GetProperty("stock").GetInt32();
                            inner[talleId] = stock;
                        }
                        stockMap[colorId] = inner;
                    }
                }
            }catch{
                stockMap = new Dictionary<int, Dictionary<int,int>>();
            }
        }

        // Call stored procedure per color+talle, passing stock from stockMap when available
        using(SqlConnection connection = new SqlConnection(connectionString)){
            connection.Open();

            foreach(var colorcito in colores){
                var tallesToUse = (talles != null && talles.Count>0) ? talles : new List<int>{1};
                foreach(var talleId in tallesToUse){
                    int stock = 0;
                    if (stockMap.ContainsKey(colorcito) && stockMap[colorcito].ContainsKey(talleId)){
                        stock = stockMap[colorcito][talleId];
                    }

                    string exec = "EXEC agregarPrenda @IdTienda, @Tipo, @Modelo, @IdTalle, @pDescripcion, @Precio, @Estilo1, @Estilo2, @Estilo3, @Color, @Temporada, @Foto, @Stock";
                    connection.Execute(exec, new {IdTienda = idTienda, Tipo = tipoInt, Modelo = modelo, IdTalle = talleId, pDescripcion = Descripcion, Precio = precio, Estilo1 = Estilos[0], Estilo2 = Estilos[1], Estilo3 = Estilos[2], Color = colorcito, Temporada = temporada, Foto = Foto, Stock = stock});
                }
            }
        }
    }

     static public Color levantarColor(int idColor)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Color WHERE idColor=@pidColor";
            var color = connection.QueryFirstOrDefault<Color>(query, new {pidColor = idColor });
            return color ?? new Color();
        }
    }

    static public Talle levantarTalle(int idTalle)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Talle WHERE IdTalle=@pIdTalle";
            var talle = connection.QueryFirstOrDefault<Talle>(query, new { pIdTalle = idTalle });
            return talle ?? new Talle();
        }
    }
     static public int levantarStock(int IdPrenda)
    {
        int stock;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            string query = "SELECT stock FROM Prenda WHERE IdPrenda=@pIdPrenda";
            stock = connection.QueryFirstOrDefault<int>(query, new {pIdPrenda = IdPrenda });

        }
        return stock;
    }
    static public void UpdateStock(int IdPrenda, int nuevoStock)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Prenda SET stock = @pStock WHERE IdPrenda = @pIdPrenda";
            connection.Execute(query, new { pStock = nuevoStock, pIdPrenda = IdPrenda });
        }
    }
    static public Prenda LevantarPrenda(int IdPrenda)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Prenda WHERE IdPrenda=@pIdPrenda";
            var prendita = connection.QueryFirstOrDefault<Prenda>(query, new { pIdPrenda = IdPrenda });
            return prendita ?? new Prenda();
        }
    }

    static public List<Prenda> LevantarVariantes(int IdPrenda)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Prenda WHERE Modelo = (SELECT Modelo FROM Prenda WHERE IdPrenda = @pIdPrenda) AND Color = (SELECT Color FROM Prenda WHERE IdPrenda = @pIdPrenda) AND IdTienda = (SELECT IdTienda FROM Prenda WHERE IdPrenda = @pIdPrenda) ORDER BY IdTalle";
            var variantes = connection.Query<Prenda>(query, new { pIdPrenda = IdPrenda }).ToList();
            return variantes;
        }
    }

    static public List<Prenda> FiltrarPrendas(double? minPrecio, double? maxPrecio, int? colorId, int? estiloId, string q)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            var where = new List<string>();
            var parameters = new DynamicParameters();
            string baseQuery = "SELECT DISTINCT p.* FROM Prenda p ";
            if (estiloId.HasValue)
            {
                baseQuery += " INNER JOIN EstiloXPrenda ep ON ep.IdPrenda = p.IdPrenda ";
                where.Add("ep.IdEstilo = @pEstilo");
                parameters.Add("pEstilo", estiloId.Value);
            }
            if (colorId.HasValue)
            {
                where.Add("p.Color = @pColor");
                parameters.Add("pColor", colorId.Value);
            }
            if (minPrecio.HasValue)
            {
                where.Add("p.Precio >= @pMin");
                parameters.Add("pMin", minPrecio.Value);
            }
            if (maxPrecio.HasValue)
            {
                where.Add("p.Precio <= @pMax");
                parameters.Add("pMax", maxPrecio.Value);
            }
            if (!string.IsNullOrWhiteSpace(q))
            {
                where.Add("(p.Modelo LIKE @q OR p.Descripcion LIKE @q)");
                parameters.Add("q", "%" + q + "%");
            }

            string finalQuery = baseQuery;
            if (where.Count > 0)
            {
                finalQuery += " WHERE " + string.Join(" AND ", where) + " AND p.mostrar=1";
            }
            else
            {
                finalQuery += " WHERE p.mostrar=1";
            }

            finalQuery += " ORDER BY p.Precio ASC";

            var prendas = connection.Query<Prenda>(finalQuery, parameters).ToList();
            return prendas;
        }
    }

    static public List<Prenda> LevantarSimilar(int IdPrenda)

    {
        int cantColor=0;
          using (SqlConnection connection = new SqlConnection(connectionString))
        {

string query = "SELECT COUNT(DISTINCT COLOR) from prenda where Modelo=(SELECT Modelo from Prenda where IdPrenda=@pIdPrenda) AND IdTienda=(SELECT IdTienda from Prenda where IdPrenda=@pIdPrenda) AND Color!=(SELECT Color from Prenda where IdPrenda=@pIdPrenda)";
            cantColor = connection.Query<int>(query, new { pIdPrenda = IdPrenda }).FirstOrDefault();

        }
        List<Prenda> prendita = new List<Prenda>();
        for(int i=0;i<cantColor;i++){
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

string query = "SELECT TOP (@num) * FROM (SELECT *, ROW_NUMBER() OVER(PARTITION BY Color ORDER BY IdPrenda) AS rn FROM Prenda WHERE Modelo = (SELECT Modelo FROM Prenda WHERE IdPrenda = @pIdPrenda) AND IdTienda = (SELECT IdTienda FROM Prenda WHERE IdPrenda = @pIdPrenda) AND Color != (SELECT Color FROM Prenda WHERE IdPrenda = @pIdPrenda)) AS PrendasFiltradas WHERE rn = 1;";
            prendita = connection.Query<Prenda>(query, new { pIdPrenda = IdPrenda, num=cantColor }).ToList();

        }
        }
        return prendita;
    }
    static public List<Estilo> LevantarPrendaxEstilo(int IdPrenda)
    {
        List<Estilo> estilitos = new List<Estilo>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            string query = "SELECT * FROM Estilo INNER JOIN estiloXPrenda AS ep on Ep.IdEstilo=Estilo.IdEstilo WHERE EP.IdPrenda=@IdPrenda";
            estilitos = connection.Query<Estilo>(query, new { @IdPrenda = IdPrenda }).ToList();

        }
        return estilitos;
    }
    static public List<Temporada> LevantarTemporadas(int IdPrenda){
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = @"SELECT T.* 
                            FROM TemporadaXPrenda TP 
                            INNER JOIN Temporada T ON T.idTemporada = TP.IdTemporada 
                            WHERE TP.IdPrenda=@pIdPrenda";
            return connection.Query<Temporada>(query, new { pIdPrenda = IdPrenda }).ToList();
        }
    }
    static public void eliminarPrenda(int IdPrenda){
        string query = "EXEC EliminarPrenda @pIdPrenda";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pIdPrenda=IdPrenda});
        }
    }
    static public void restarStock(int IdPrenda){
                string query = "UPDATE Prenda SET stock=stock-1 WHERE IdPrenda=@pIdPrenda";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pIdPrenda=IdPrenda});
        }

    }
    
    static public void editarPrenda(int idPrenda, int tipoInt, string modelo, string Descripcion, double precio, List<int> Estilos, List<int> colores, List<int> talles, int temporada, string Foto, string variantStockJson){
        while (Estilos.Count < 3) {
           Estilos.Add(0);
        }

        Dictionary<int, Dictionary<int,int>> stockMap = new Dictionary<int, Dictionary<int,int>>();
        if (!string.IsNullOrEmpty(variantStockJson)){
            try{
                using(var doc = JsonDocument.Parse(variantStockJson)){
                    foreach(var prop in doc.RootElement.EnumerateObject()){
                        if (!int.TryParse(prop.Name, out var colorId)) continue;
                        var inner = new Dictionary<int,int>();
                        foreach(var item in prop.Value.EnumerateArray()){
                            int talleId = item.GetProperty("talleId").GetInt32();
                            int stock = item.GetProperty("stock").GetInt32();
                            inner[talleId] = stock;
                        }
                        stockMap[colorId] = inner;
                    }
                }
            }catch{
                stockMap = new Dictionary<int, Dictionary<int,int>>();
            }
        }

        // Obtener la prenda original para mantener IdTienda y Modelo
        var prendaOriginal = LevantarPrenda(idPrenda);
        if(prendaOriginal == null || prendaOriginal.IdPrenda == 0) return;

        using(SqlConnection connection = new SqlConnection(connectionString)){
            connection.Open();

            // Eliminar relaciones existentes
            connection.Execute("DELETE FROM EstiloXPrenda WHERE IdPrenda=@IdPrenda", new {IdPrenda = idPrenda});
            connection.Execute("DELETE FROM TemporadaXPrenda WHERE IdPrenda=@IdPrenda", new {IdPrenda = idPrenda});

            // Actualizar o crear variantes por color y talle
            foreach(var colorcito in colores){
                var tallesToUse = (talles != null && talles.Count>0) ? talles : new List<int>{1};
                foreach(var talleId in tallesToUse){
                    int stock = 0;
                    if (stockMap.ContainsKey(colorcito) && stockMap[colorcito].ContainsKey(talleId)){
                        stock = stockMap[colorcito][talleId];
                    }

                    // Buscar si existe una variante con este color y talle
                    string checkQuery = "SELECT IdPrenda FROM Prenda WHERE Modelo=@Modelo AND IdTienda=@IdTienda AND Color=@Color AND IdTalle=@IdTalle";
                    var existingId = connection.QueryFirstOrDefault<int?>(checkQuery, new {
                        Modelo = modelo,
                        IdTienda = prendaOriginal.IdTienda,
                        Color = colorcito,
                        IdTalle = talleId
                    });

                    if(existingId.HasValue && existingId.Value == idPrenda){
                        // Actualizar la prenda actual
                        string updateQuery = @"UPDATE Prenda SET Tipo=@Tipo, Descripcion=@Descripcion, Precio=@Precio, Foto=@Foto, stock=@Stock 
                                             WHERE IdPrenda=@IdPrenda";
                        connection.Execute(updateQuery, new {
                            Tipo = tipoInt,
                            Descripcion = Descripcion,
                            Precio = precio,
                            Foto = Foto,
                            Stock = stock,
                            IdPrenda = idPrenda
                        });
                    } else if(existingId.HasValue){
                        // Actualizar otra variante existente
                        string updateQuery = @"UPDATE Prenda SET Tipo=@Tipo, Descripcion=@Descripcion, Precio=@Precio, Foto=@Foto, stock=@Stock 
                                             WHERE IdPrenda=@IdPrenda";
                        connection.Execute(updateQuery, new {
                            Tipo = tipoInt,
                            Descripcion = Descripcion,
                            Precio = precio,
                            Foto = Foto,
                            Stock = stock,
                            IdPrenda = existingId.Value
                        });
                    } else {
                        // Crear nueva variante
                        string insertQuery = @"INSERT INTO Prenda (Tipo, Modelo, IdTalle, Descripcion, Precio, Foto, Color, IdTienda, mostrar, stock)
                                             VALUES (@Tipo, @Modelo, @IdTalle, @Descripcion, @Precio, @Foto, @Color, @IdTienda, 0, @Stock)";
                        var newId = connection.QueryFirstOrDefault<int>(insertQuery + "; SELECT CAST(SCOPE_IDENTITY() as int)", new {
                            Tipo = tipoInt,
                            Modelo = modelo,
                            IdTalle = talleId,
                            Descripcion = Descripcion,
                            Precio = precio,
                            Foto = Foto,
                            Color = colorcito,
                            IdTienda = prendaOriginal.IdTienda,
                            Stock = stock
                        });
                    }
                }
            }

            // Actualizar la prenda principal
            string mainUpdate = @"UPDATE Prenda SET Tipo=@Tipo, Modelo=@Modelo, Descripcion=@Descripcion, Precio=@Precio, Foto=@Foto 
                                WHERE IdPrenda=@IdPrenda";
            connection.Execute(mainUpdate, new {
                Tipo = tipoInt,
                Modelo = modelo,
                Descripcion = Descripcion,
                Precio = precio,
                Foto = Foto,
                IdPrenda = idPrenda
            });

            // Re-agregar relaciones de estilos
            foreach(var estiloId in Estilos){
                if(estiloId > 0){
                    connection.Execute("INSERT INTO EstiloXPrenda (IdEstilo, IdPrenda) VALUES (@IdEstilo, @IdPrenda)", 
                        new {IdEstilo = estiloId, IdPrenda = idPrenda});
                }
            }

            // Re-agregar relación de temporada
            connection.Execute("INSERT INTO TemporadaXPrenda (IdTemporada, IdPrenda) VALUES (@IdTemporada, @IdPrenda)", 
                new {IdTemporada = temporada, IdPrenda = idPrenda});
        }
    }
    
    static public List<Prenda> LevantarPrendasPorModelo(int idTienda, string modelo){
        using(SqlConnection connection = new SqlConnection(connectionString)){
            string query = "SELECT * FROM Prenda WHERE IdTienda=@IdTienda AND Modelo=@Modelo";
            return connection.Query<Prenda>(query, new {IdTienda = idTienda, Modelo = modelo}).ToList();
        }
    }
}