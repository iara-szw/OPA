using Microsoft.Data.SqlClient;
using Dapper;
static class BD{
 
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

     static public List<Prenda> levantarRecomendados(string Idcomprador){
    List<Prenda> prendas = new List<Prenda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="EXEC levantarRecomendados @idComprador";
        prendas= connection.Query<Prenda>(query,new{@pIdCompradores=Idcomprador}).ToList();

    }
    return prendas;
    }
     static public List<Tienda> levantarRecomendadosTienda(string Idcomprador){
    List<Tienda> tiendas = new List<Tienda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="EXEC levantarRecomendadosTienda @idComprador";
        tiendas= connection.Query<Tienda>(query,new{@pIdCompradores=Idcomprador}).ToList();

    }
    return tiendas;
    }
 static public Prenda levantarPrenda(int IdPrenda){
    Prenda ropa = new Prenda();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Prenda where IdPrenda=@pIdPrenda";
        ropa= connection.QueryFirstOrDefault<Prenda>(query,new{pIdPrenda=IdPrenda});

    }
    return ropa;
    }

}