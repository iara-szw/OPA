using Microsoft.Data.SqlClient;
using Dapper;
static class ArmarioBD{
 
    static public  string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
    static public List<Prenda> levantarPoseidos(string Idcomprador){
    List<Prenda> prendas = new List<Prenda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Prenda as PRE INNER JOIN Poseido as P ON PRE.IdPrenda=P.IdPrenda WHERE P.Usuario=@pIdComprador";
        prendas= connection.Query<Prenda>(query, new{@pIdComprador=Idcomprador}).ToList();

    }
    return prendas;
    }
    static public List<Prenda> levantarDeseados(string Idcomprador){
    List<Prenda> prendas = new List<Prenda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Prenda as PRE INNER JOIN Deseado as D ON PRE.IdPrenda=D.IdPrenda WHERE D.Usuario=@pIdComprador";
        prendas= connection.Query<Prenda>(query,new{@pIdComprador=Idcomprador}).ToList();

    }
    return prendas;
    }
        static public void agregarEstilos(string Usuario,List<int>estilos){
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "DELETE EstiloxComprador WHERE usuario=@usuario";
        connection.Execute(query,new{@usuario=Usuario});
            
        }
        foreach(int est in estilos){
            using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "INSERT INTO EstiloxComprador Values(@usuario,@idEstilo)";
        connection.Execute(query,new{@usuario=Usuario,@idEstilo=est});
            
        }
        }
        }
         static public void agregarColores(string Usuario,List<int>colores){
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "DELETE ColorxComprador WHERE usuario=@usuario";
        connection.Execute(query,new{@usuario=Usuario});
            
        }
        foreach(int col in colores){
            using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "INSERT INTO ColorxComprador Values(@idColor,@usuario)";
        connection.Execute(query,new{@usuario=Usuario,@idColor=col});
            
        }
        }
        }
    static public void eliminarPoseido(string usuario, int idPrenda){

using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "DELETE FROM Poseido where Usuario=@pusuario AND idPrenda=@idPrenda";
        connection.Execute(query,new{@pusuario=usuario,@idPrenda=idPrenda});
            
        }
    }
}