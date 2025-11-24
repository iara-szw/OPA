using System.Security.Cryptography;
using System.Text;
public class encriptar {
    
public static string HashearPassword(string password)
{
    using (SHA256 sha256 = SHA256.Create())
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256.ComputeHash(bytes);
        StringBuilder sb = new StringBuilder();
        foreach (byte b in hash)
            sb.Append(b.ToString("x2")); // Formato hexadecimal
        return sb.ToString();
    }
}
}