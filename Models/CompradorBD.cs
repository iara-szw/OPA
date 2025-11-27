using Microsoft.Data.SqlClient;
using Dapper;
static class CompradorBD{
 
    public static string connectionString = @"Server=localhost\SQLEXPRESS01; DataBase=OPA; Integrated Security=True; TrustServerCertificate=True;";
    static public  void agregarComprador(Comprador usu){

        string query = "INSERT INTO Comprador (Usuario, Nombre, Apellido, Contraseña,Telefono,Mail,Genero,esVendedor) VALUES (@pUsuario, @pNombre, @pApellido, @pContraseña, @pTelefono, @pMail, @pGenero,@pVendedor)";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pUsuario=usu.Usuario, pNombre=usu.Nombre,pApellido=usu.Apellido, pContraseña=usu.Contraseña, pTelefono=usu.Telefono, pMail=usu.Mail, pGenero=usu.Genero, pVendedor=usu.esVendedor});
        }
    }

    static public void agregarDeseado(int idPrenda, string Usuario){
        string query = "INSERT INTO Deseado VALUES (@pidPrenda, @pUsuario)";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pidPrenda=idPrenda, pUsuario=Usuario});
        }
    }
        static public void comprarPrenda(int idPrenda, string Usuario){
        string query = "INSERT INTO Poseido VALUES (@pUsuario,@pidPrenda)";
        using(SqlConnection connection = new SqlConnection(connectionString)){
        connection.Execute(query, new {pidPrenda=idPrenda, pUsuario=Usuario});
        }
    }


    static public  bool yaExiste(string NombreUsuario){

        Comprador usu=null;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Comprador WHERE Usuario=@Usuario";
        usu= connection.QueryFirstOrDefault<Comprador>(query,new{Usuario=NombreUsuario});
            
        }
        return usu!=null;
   }
      static public Comprador levantarComprador(string nombreUsuario, string password){
        Comprador usu=null;
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "SELECT * FROM Comprador WHERE Usuario=@pnombreUsuario AND Contraseña=@Contraseña";
        usu= connection.QueryFirstOrDefault<Comprador>(query,new{pnombreUsuario=nombreUsuario,Contraseña=password});
            
        }
        return usu;
   }
    static public List<Estilo> levantarEstilos(string Idcomprador){
    List<Estilo> estilos = new List<Estilo>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Estilo INNER JOIN EstiloxComprador AS EC ON EC.IdEstilo=Estilo.IdEstilo WHERE EC.Usuario=@pIdcomprador";
        estilos= connection.Query<Estilo>(query,new{@pIdComprador=Idcomprador}).ToList();

    }
    return estilos;
    }
    static public List<Color> levantarColores(string Idcomprador){
    List<Color> color = new List<Color>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Color INNER JOIN ColorxComprador AS CC ON CC.IdColor=Color.IdColor WHERE CC.Usuario=@pIdcomprador";
        color= connection.Query<Color>(query,new{@pIdComprador=Idcomprador}).ToList();

    }
    return color;
    }
    static public List<string> levantarPrendas(string Idcomprador){
    List<string> tipos = new List<string>();
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM Tipos INNER JOIN TiposXComprador AS TC ON TC.IdTipo=Tipos.IdTipo WHERE TC.Usuario=@pIdcomprador";
        tipos= connection.Query<string>(query,new{@pIdComprador=Idcomprador}).ToList();

    }
    return tipos;
    }

    static  public void cargarMedidas(string usuario,double MedidaTorso, double MedidaCintura, double MedidaPierna, double MedidaHombros, double MedidaBrazos, double MedidaCadera){
       Console.WriteLine("aca");
       Console.WriteLine(MedidaTorso);
        using(SqlConnection connection = new SqlConnection(connectionString)){
 
        string query = "EXEC cargarMedidas @idUsuario,@MedidaTorso,@MedidaCintura,@MedidaPierna,@MedidaHombros,@MedidaBrazos,@MedidaCadera";
        connection.Execute(query,new{@idUsuario=usuario,@MedidaTorso=MedidaTorso,@MedidaCintura=MedidaCintura,@MedidaPierna=MedidaPierna,@MedidaHombros=MedidaHombros,@MedidaBrazos=MedidaBrazos,@MedidaCadera=MedidaCadera});
            
        }
    }
     static  public TalleUsu levantarMedidas(string usuario){
        TalleUsu tallesito;
         using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT * FROM TallesUsu WHERE IdTalle=(SELECT talles FROM Comprador where Usuario=@pusuario)";
        tallesito= connection.QueryFirstOrDefault<TalleUsu>(query,new{@pusuario=usuario});

    }
    return tallesito;
    }
    static public void editarComprador(string usuario, string nombre,string apellido,string mail,string telefono,string passwordHasheada){
         using(SqlConnection connection = new SqlConnection(connectionString)){
        string query = "UPDATE Comprador SET Nombre=@nombre, Apellido=@Apellido, Contraseña=@password, Telefono=@telefono,Mail=@mail";
        connection.Execute(query,new{@nombre=nombre, @Apellido=apellido, @password=passwordHasheada,@telefono=telefono,@mail=mail});
            
        }
    }

    static public bool verSiPoseido(int idPrenda, string usuario){
          bool esta = false;
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT 1 FROM Poseido WHERE Usuario=@pusuario AND IdPrenda=@pidPrenda";
        esta= connection.QueryFirstOrDefault<bool>(query,new{@pusuario=usuario,pidPrenda=idPrenda});

    }
    return esta;
    }

      static public bool verSiDeseado(int idPrenda, string usuario){
          bool esta = false;
    using(SqlConnection connection=new SqlConnection(connectionString)){
        string query="SELECT 1 FROM Deseado WHERE Usuario=@pusuario AND IdPrenda=@pidPrenda";
        esta= connection.QueryFirstOrDefault<bool>(query,new{@pusuario=usuario,pidPrenda=idPrenda});

    }
    return esta;
    }
}