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
    public static Administrador levantarAdministrador(string usuario){
        Administrador usu=null;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Administrador WHERE Usuario=@pusuario";
        usu= connection.QueryFirstOrDefault<Administrador>(query,new{pusuario=usuario});
            
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
