using ProjetoAtivos.Models;

namespace ProjetoEstagio.Controls
{
    public class LoginControl
    {
        public Usuario Login(string Login, string Senha)
        {
            Usuario User;
            if ((Login != "" && Login.Length > 6) && (Senha != "" && Senha.Length >= 6))
            {
                User = new Usuario().BuscarUsuario(Login, Senha);

                if (User != null)
                {
                    return User;
                }
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
    }

}
