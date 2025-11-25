using Microsoft.Data.SqlClient;
using Dapper;
public class PrendaBD{
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

  static public void agregarPrenda(int idTienda, string tipo, string modelo, int idTalle,string Descripcion,double precio, List<int> Estilos,List<int> color, int temporada, string Foto){
    if (Estilos.Count != 3)
        {
            Estilos.Add(Estilos[0]);
            if (Estilos.Count != 3)
            {
                Estilos.Add(Estilos[0]);
            }
        }
        foreach (int colorcito in color)
        {
  string query = "EXEC agregarPrenda @IdTienda, @Tipo, @Modelo, @IdTalle,@descripcion,@Precio, @estilo1,@estilo2,@estilo3,@pcolor, @Temporada, @foto";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {IdTienda=idTienda, Tipo=tipo,IdTalle=idTalle, Modelo=modelo, descripcion=Descripcion, Precio=precio, estilo1=Estilos[0],estilo2=Estilos[1],estilo3=Estilos[2],pcolor = colorcito
,Temporada=temporada, foto=Foto});
        }
        }
        
    }

     static public Color levantarColor(int idColor)
    {
        Color color = new Color();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            string query = "SELECT * FROM Color WHERE idColor=@pidColor";
            color = connection.QueryFirstOrDefault<Color>(query, new {pidColor = idColor });

        }
        return color;
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
        Prenda prendita = new Prenda();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            string query = "SELECT * FROM Prenda WHERE IdPrenda=@pIdPrenda";
            prendita = connection.QueryFirstOrDefault<Prenda>(query, new { pIdPrenda = IdPrenda });

        }
        return prendita;
    }

    static public List<Prenda> LevantarSimilar(int IdPrenda)
    {
        List<Prenda> prendita = new List<Prenda>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            string query = "SELECT * FROM Prenda WHERE Modelo=(SELECT Modelo from Prenda where IdPrenda=@pIdPrenda) AND IdTienda=(SELECT IdTienda from Prenda where IdPrenda=@pIdPrenda)";
            prendita = connection.Query<Prenda>(query, new { pIdPrenda = IdPrenda }).ToList();

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
        string query = "DELETE FROM Prenda WHERE IdPrenda=@pIdPrenda";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pIdPrenda=IdPrenda});
        }
    }
    static public void restarStock(int IdPrenda){
                string query = "UPDATE Prenda SET stock=stock-1 WHERE IdPrenda=@IdPrenda";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pIdPrenda=IdPrenda});
        }

    }
}