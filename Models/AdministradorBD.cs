using Microsoft.Data.SqlClient;
using Dapper;
static class AdministradorBD{
 
    public static string connectionString = @"Server=localhost; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
    
     public static void agregarAdministrador(int idTienda,bool permisos, string usuario,string mail){

        string query = "INSERT INTO Administrador (IdTienda, Usuario, Permisos, mail) VALUES (@pTienda, @pPermisos, @pUsuario, @pMail)";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pTienda=idTienda, pPermisos=permisos,pUsuario=usuario, pMail=mail});
        }
    }
    public static List<Tienda> levantarAdministrador(string usuario){
        List<Tienda> usu=null;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Tienda INNER JOIN Administrador ON Administrador.idTienda=Tienda.idTienda WHERE Administrador.Usuario=@pusuario";
        usu= connection.Query<Tienda>(query,new{pusuario=usuario}).ToList();
            
        }
        return usu;
   }


public static bool verPermisos(int IdTienda,string usuario ){
        bool permisos;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT Permisos FROM Administrador WHERE Usuario=@pusuario AND IdTienda=@pIdTienda";
        permisos= connection.QueryFirstOrDefault<bool>(query,new{pusuario=usuario,pIdTienda=IdTienda});
            
        }
        return permisos;
   }


}
