using Microsoft.Data.SqlClient;
using Dapper;
static class CompradorBD{
 
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
    public static void agregarComprador(Comprador usu){

        string query = "INSERT INTO Comprador (Usuario, Nombre, Apellido, Contraseña,Telefono,Mail,Genero) VALUES (@pUsuario, @pNombre, @pApellido, @pContraseña, @pTelefono, @pMail, @pGenero)";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pUsuario=usu.Usuario, pNombre=usu.Nombre,pApellido=usu.Apellido, pContraseña=usu.Contraseña, pTelefono=usu.Telefono, pMail=usu.Mail, pGenero=usu.Genero});
        }
    }
    public static Comprador levantarComprador(string nombreUsuario, string password){
        Comprador usu=null;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Comprador WHERE Usuario=@pnombreUsuario AND Contraseña=@Contraseña";
        usu= connection.QueryFirstOrDefault<Comprador>(query,new{pnombreUsuario=nombreUsuario,Contraseña=password});
            
        }
        return usu;
   }
    public static bool yaExiste(string NombreUsuario){

        Comprador usu=null;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Comprador WHERE Usuario=@Usuario";
        usu= connection.QueryFirstOrDefault<Comprador>(query,new{Usuario=NombreUsuario});
            
        }
        return usu!=null;
   }
      public List<Estilo> levantarEstilos(int Idcomprador){
    List<Estilo> estilos = new List<Estilo>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Estilo INNER JOIN EstiloxComprador AS EC ON EC.IdEstilo=Estilo.IdEstilo WHERE EC.idComprador=@pIdcomprador";
        estilos= connection.Query<Estilo>(query,new{@pIdComprador=Idcomprador}).ToList();

    }
    return estilos;
    }
}