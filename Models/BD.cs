using Microsoft.Data.SqlClient;
using Dapper;
static class BD{
 
    public static string connectionString = @"Server=localhost\SQLEXPRESS01; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

     static public List<Prenda> levantarRecomendados(string Idcomprador){
    List<Prenda> prendas = new List<Prenda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="EXEC levantarRecomendados @idComprador";
        prendas= connection.Query<Prenda>(query,new{@idComprador=Idcomprador}).ToList();

    }
    if (Idcomprador!="default" && prendas.Count<20)
    {
            using(SqlConnection connection=new SqlConnection(connectionString)){

        string query="SELECT TOP 20 * FROM Prenda WHERE mostrar=1";
        prendas= connection.Query<Prenda>(query,new{}).ToList();
            }
    }
    return prendas;
    }


     static public List<Tienda> levantarRecomendadosTienda(){
    List<Tienda> tiendas = new List<Tienda>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT TOP 20 * FROM Tienda";
        tiendas= connection.Query<Tienda>(query,new{}).ToList();

    }
    return tiendas;
    }
 static public Prenda levantarPrenda(int IdPrenda){
    Prenda ropa = new Prenda();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Prenda where IdPrenda=@pIdPrenda";
        ropa= connection.QueryFirstOrDefault<Prenda>(query,new{pIdPrenda=IdPrenda});

    }
    if(ropa==null){
        ropa=null;
    }
    return ropa;
    }
     static public List<Estilo> levantarEstilos(){
    List<Estilo> estilos = new List<Estilo>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Estilo ";
        estilos= connection.Query<Estilo>(query).ToList();

    }
    return estilos;
    }
    static public List<Color> levantarColor(){
    List<Color> colores = new List<Color>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Color ";
        colores= connection.Query<Color>(query).ToList();

    }
    return colores;
    }
        static public List<Temporada> levantarTemporada(){
    List<Temporada> temporada = new List<Temporada>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Temporada ";
        temporada= connection.Query<Temporada>(query).ToList();

    }
    return temporada;
    }

        static public List<Talle> levantarTalles(){
    List<Talle> talles = new List<Talle>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Talle ";
        talles= connection.Query<Talle>(query).ToList();

    }
    return talles;
    }

        static public List<Tipo> levantarTipos(){
            List<Tipo> tipos = new List<Tipo>();
            using(SqlConnection connection=new SqlConnection(connectionString)){
                string query = "SELECT * FROM Tipos";
                tipos = connection.Query<Tipo>(query).ToList();
            }
            return tipos;
        }
}