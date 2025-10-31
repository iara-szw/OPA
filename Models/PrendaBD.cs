using Microsoft.Data.SqlClient;
using Dapper;
public class PrendaBD{
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

  static public void agregarPrenda(int idTienda, string tipo, string modelo, int idTalle,string Descripcion,double precio, List<int> Estilos,int color, int temporada, string Foto){
        if(Estilos.Count != 3){
            Estilos.Add(Estilos[0]);
            if(Estilos.Count != 3){
              Estilos.Add(Estilos[0]);
            }
        }
       string query = "EXEC agregarPrenda @IdTienda, @Tipo, @Modelo, @IdTalle,@descripcion,@Precio, @estilo1,@estilo2,@estilo3,@color, @Temporada, @foto";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {IdTienda=idTienda, Tipo=tipo,IdTalle=idTalle, Modelo=modelo, descripcion=Descripcion, Precio=precio, estilo1=Estilos[0],estilo2=Estilos[1],estilo3=Estilos[2],@color=color,Temporada=temporada, @foto=Foto});
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