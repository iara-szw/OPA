using Microsoft.Data.SqlClient;
using Dapper;
static class ArmarioBD{
 
<<<<<<< HEAD
    static public  string connectionString = @"Server=localhost\SQLEXPRESS01; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
=======
    static public  string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
>>>>>>> e74b5af953a42157a6a9be6fbbc2a0395bae5d2f
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
}