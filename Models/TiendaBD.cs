using Microsoft.Data.SqlClient;
using Dapper;
static class TiendaBD{
 
<<<<<<< HEAD
    public static string connectionString = @"Server=localhost\SQLEXPRESS01; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
=======
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
>>>>>>> e74b5af953a42157a6a9be6fbbc2a0395bae5d2f

    static public Tienda levantarTienda(int idTienda){
        Tienda tiendita=null;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Tienda WHERE IdTienda=@pidTienda";
        tiendita= connection.QueryFirstOrDefault<Tienda>(query,new{pidTienda=idTienda});
            
        }
        return tiendita;
    }

    static public int crearTienda(string Nombre,string Ubicacion,string Mail,string Telefono,string Descripcion,string FotoDePerfil,string Contacto, string usuario){
        int id=-1;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "EXEC crearTienda @nombre, @ubicacion,@mail,@telefono,@descripcion,@fotoDePerfil,@Contacto, @Usuario";
        id= connection.QueryFirstOrDefault<int>(query,new{nombre=Nombre,ubicacion=Ubicacion,mail=Mail,telefono=Telefono,descripcion=Descripcion,FotoDePerfil=FotoDePerfil,contacto=Contacto, Usuario=usuario});
            
        }
        return id;
    }

    static public List<Prenda> levantarProductos(int idTienda){ 
        List<Prenda> productos= new List<Prenda>();
        using(SqlConnection connection=new SqlConnection(connectionString)){
<<<<<<< HEAD
        string query="SELECT * FROM Prenda WHERE IdTienda=@idTienda AND mostrar=1";
=======
        string query="SELECT * FROM Prenda INNER JOIN PrendaTienda AS PT ON PT.IdPrenda=Prenda.IdPrenda WHERE PT.idTienda=@idTienda";
>>>>>>> e74b5af953a42157a6a9be6fbbc2a0395bae5d2f
        productos= connection.Query<Prenda>(query,new{@idTienda=idTienda}).ToList();

    }
    return productos;
    }
    //Crear tienda, levantar administradores, agregar producto, borrar producto,editar producto,levamtar producto

}