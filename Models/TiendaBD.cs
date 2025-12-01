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
      static public void cambiarDatos(int IdTienda, string Nombre,string Ubicacion, string Mail, string Telefono,string Descripcion, string Contacto){
           using(SqlConnection connection = new SqlConnection(connectionString)){
        string query = "UPDATE Tienda SET Nombre=@nombre, Ubicacion=@pUbicacion, Mail=@pMail, Telefono=@telefono,Descripcion=@pDescripcion, Contacto=@contacto WHERE IdTienda=@IdTienda";
        connection.Execute(query,new{@nombre=Nombre, @pUbicacion=Ubicacion, @pMail=Mail,@telefono=Telefono,@pDescripcion=Descripcion,@contacto=Contacto, @IdTienda=IdTienda});
            
        }
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
        string query="SELECT * FROM Prenda WHERE IdTienda=@idTienda AND mostrar=1";
        productos= connection.Query<Prenda>(query,new{@idTienda=idTienda}).ToList();

    }
    return productos;
    }
    //Crear tienda, levantar administradores, agregar producto, borrar producto,editar producto,levamtar producto

}