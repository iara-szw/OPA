using Microsoft.Data.SqlClient;
using Dapper;
public class PrendaBD{
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

  static public void agregarPrenda(int idTienda, string tipo,int idPrenda, string modelo, int idTalle,string Descripcion,double precio, List<Estilo> Estilos, int temporada){
       string query = "EXEC agregarPrenda @IdTienda, @Tipo, @Modelo, @IdTalle,@descripcion,@Precio, @estilos, @Temporada";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {IdTienda=idTienda, Tipo=tipo,IdTalle=idTalle, Modelo=modelo, descripcion=Descripcion, Precio=precio, estilos=Estilos,Temporada=temporada});
        }
    }

    static public Prenda LevantarPrenda(int IdPrenda){
        Prenda prendita=new Prenda();
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Prenda WHERE IdPrenda=@pIdPrenda";
        prendita= connection.QueryFirstOrDefault<Prenda>(query,new{pIdPrenda=IdPrenda});
            
        }
        return prendita;
    }

    static public void eliminarPrenda(int IdPrenda){
        string query = "DELETE FROM Prenda WHERE IdPrenda=@pIdPrenda";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pIdPrenda=IdPrenda});
        }
    }
}