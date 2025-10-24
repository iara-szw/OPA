using Microsoft.Data.SqlClient;
using Dapper;
static class BD{
 
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

     static public List<Prenda> levantarRecomendados(int Idcomprador){
    List<Prenda> prendas = new List<Prenda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="EXEC levantarRecomendados @idComprador";
        prendas= connection.Query<Prenda>(query,new{@pIdCompradores=Idcomprador}).ToList();

    }
    return patentes;
    }


}