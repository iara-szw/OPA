using Microsoft.Data.SqlClient;
using Dapper;
using System.Text.Json;
// Simplified implementation will call stored procedure per color+talle
public class PrendaBD{
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

    static public void agregarPrenda(int idTienda, int tipoInt, string modelo, string Descripcion, double precio, List<int> Estilos, List<int> colores, List<int> talles, int temporada, string Foto, string variantStockJson){
        while (Estilos.Count < 3) {
           Estilos.Add(0);
        }

        // Parse variantStockJson -> mapping colorId -> mapping talleId->stock
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

                    string exec = "EXEC agregarPrenda @IdTienda, @Tipo, @Modelo, @IdTalle, @Descripcion, @Precio, @Estilo1, @Estilo2, @Estilo3, @Color, @Temporada, @Foto, @Stock";
                    connection.Execute(exec, new {IdTienda = idTienda, Tipo = tipoInt, Modelo = modelo, IdTalle = talleId, Descripcion = Descripcion, Precio = precio, Estilo1 = Estilos[0], Estilo2 = Estilos[1], Estilo3 = Estilos[2], Color = colorcito, Temporada = temporada, Foto = Foto, Stock = stock});
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
    static public Prenda LevantarPrenda(int IdPrenda)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Prenda WHERE IdPrenda=@pIdPrenda";
            var prendita = connection.QueryFirstOrDefault<Prenda>(query, new { pIdPrenda = IdPrenda });
            return prendita ?? new Prenda();
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
    }static public List<Estilo> LevantarPrendaxEstilo(int IdPrenda)
    {
        List<Estilo> estilitos = new List<Estilo>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            string query = "SELECT * FROM Estilo INNER JOIN estiloXPrenda AS ep on Ep.IdEstilo=Estilo.IdEstilo WHERE EP.IdPrenda=@IdPrenda";
            estilitos = connection.Query<Estilo>(query, new { @IdPrenda = IdPrenda }).ToList();

        }
        return estilitos;
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
}