 public class Comprador{
    public int IdComprador;
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

     public void crearComprador(string usuario,string nombre, string apellido, string contraseña,string telefono,string mail,int genero){
        Usuario=usuario;
        Nombre=nombre;
        Apellido=apellido;
        Contraseña=contraseña;
        Telefono=telefono;
        Mail=mail;
        Genero=genero;
    }
 }