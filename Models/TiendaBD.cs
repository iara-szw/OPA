using Microsoft.Data.SqlClient;
using Dapper;
static class TiendaBD{
 
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";

    static public Tienda levantarTienda(int idTienda){
        Tienda tiendita=null;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Tienda WHERE IdTienda=@pidTienda";
        tiendita= connection.QueryFirstOrDefault<Tienda>(query,new{pidTienda=idTienda});
            
        }
        return tiendita;
    }

    static public int crearTienda(string Nombre,string Ubicacion,string Mail,string Telefono,string Descripcion,string FotoDePerfil,string Contacto){
        int id=-1;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "EXEC crearTienda @nombre, @ubicacion,@mail,@telefono,@descripcion,@fotoDePerfil,@Contacto";
        id= connection.QueryFirstOrDefault<int>(query,new{nombre=Nombre,ubicacion=Ubicacion,mail=Mail,telefono=Telefono,descripcion=Descripcion,FotoDePerfil=FotoDePerfil,contacto=Contacto});
            
        }
        return id;
    }

    static public List<Prenda> levantarProductos(int idTienda){ 
        List<Prenda> productos= new List<Prenda>();
        using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Prenda INNER JOIN PrendaTienda AS PT ON PT.IdPrenda=Prenda.IdPrenda WHERE PT.idTienda=@idTienda";
        productos= connection.Query<Prenda>(query,new{@idTienda=idTienda}).ToList();

    }
    return productos;
    }
    //Crear tienda, levantar administradores, agregar producto, borrar producto,editar producto,levamtar producto

}