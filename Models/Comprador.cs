 public class Comprador{
	public string Usuario;
	public string Nombre;
    public string Apellido;

    public string Contraseña;
    public string Telefono;
    public string FotoDePerfil;
    public string Mail;
    public int Genero;
    public double MedidaTorso;
    public int MedioDePago;

    public bool esVendedor;

     public void crearComprador(string usuario,string contraseña,string nombre, string apellido,string telefono,string mail,int genero, bool EsVendedor){
        Usuario=usuario;
        Nombre=nombre;
        Apellido=apellido;
        Contraseña=contraseña;
        Telefono=telefono;
        Mail=mail;
        Genero=genero;
        esVendedor=EsVendedor;
    }
 }