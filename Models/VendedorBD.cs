using Microsoft.Data.SqlClient;
using Dapper;
static class VendedorBD{
 
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
    //HACER QUE LA BASE DE DATOS CATALOGUE A LOS VENDEDORES CON UN BIT DE SI SON VENDEDORES, NO TENER OTRA PESTAÑA PARA ESO
    public static void agregarVendedor(Vendedor usu){

        string query = "INSERT INTO Vendedor (Usuario, Nombre, Apellido, Contraseña,Telefono,Mail) VALUES (@pUsuario,@pNombre, @pApellido, @pContraseña, @pTelefono, @pMail)";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pUsuariop=usu.Usuario, Nombre=usu.Nombre,pApellido=usu.Apellido, pContraseña=usu.Contraseña, pTelefono=usu.Telefono, pMail=usu.Mail});
        }
    }
    public static Vendedor levantarVendedor(string Usuario, string password){
        Vendedor usu=null;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Vendedor WHERE Usuario=@pUsuario AND Contraseña=@Contraseña";
        usu= connection.QueryFirstOrDefault<Comprador>(query,new{pUsuario=nombreUsuario,Contraseña=password});
            
        }
        return usu;
   }
    public static bool yaExiste(string NombreUsuario){

        Comprador usu=null;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Integrantes WHERE Usuario=@Usuario";
        usu= connection.QueryFirstOrDefault<Comprador>(query,new{Usuario=NombreUsuario});
            
        }
        return usu!=null;
   }
}