using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
        }

        public void Inserir(Usuario user)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
               bc.Usuarios.Add(user);
               bc.SaveChanges();
            }
        }

        public Usuario BuscaId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }

        public void Editar(Usuario EditU)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario antigoU = bc.Usuarios.Find(EditU.Id);

                antigoU.Nome = EditU.Nome;
                antigoU.Login = EditU.Login;
                antigoU.Senha = EditU.Senha;
                antigoU.Tipo = EditU.Tipo;

                bc.SaveChanges();
            }
        }

        public void Excluir(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                 bc.Usuarios.Remove(bc.Usuarios.Find(id));
                 bc.SaveChanges();
            }
        }
    }
}