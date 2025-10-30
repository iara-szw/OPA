using Microsoft.Data.SqlClient;
using Dapper;
static class ArmarioBD{
 
    static public  string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
    static public List<Prenda> levantarPoseidos(string Idcomprador){
    List<Prenda> prendas = new List<Prenda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Prenda as PRE INNER JOIN Poseido as P ON PRE.IdPrenda=P.IdPrenda WHERE P.IdComprador=@pIdComprador";
        prendas= connection.Query<Prenda>(query, new{@pIdComprador=Idcomprador}).ToList();

    }
    return prendas;
    }
    static public List<Prenda> levantarDeseados(string Idcomprador){
    List<Prenda> prendas = new List<Prenda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Prenda as PRE INNER JOIN Deseado as D ON PRE.IdPrenda=D.IdPrenda WHERE D.IdComprador=@pIdComprador";
        prendas= connection.Query<Prenda>(query,new{@pIdComprador=Idcomprador}).ToList();

    }
    return prendas;
    }
}