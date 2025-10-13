using Microsoft.Data.SqlClient;
using Dapper;
static class ArmarioBD{
 
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
        public List<Prenda> levantarPoseidos(int Idcomprador){
    List<Prenda> prendas = new List<Prenda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Prendas as PRE INNER JOIN Poseido as P ON PRE.IdPrenda=P.IdPrenda WHERE P.IdComprador=@pIdCompradores";
        prendas= connection.Query<Prenda>(query, new{@pIdComprador=Idcomprador}).ToList();

    }
    return patentes;
    }
         public List<Prenda> levantarDeseados(int Idcomprador){
    List<Prenda> prendas = new List<Prenda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Prendas as PRE INNER JOIN Deseado as D ON PRE.IdPrenda=D.IdPrenda WHERE D.IdComprador=@pIdCompradores";
        prendas= connection.Query<Prenda>(query,new{@pIdComprador=Idcomprador}).ToList();

    }
    return patentes;
    }
}