using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        //Funções adicionais---------------------------------------------------------------------
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        //Cadastro-------------------------------------------------------------------------------
        public IActionResult CadastroUser()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaTipoUser(this);

            return View();
        }

        [HttpPost]
        public IActionResult CadastroUser(Usuario u)
        {
            u.Senha = Criptografo.TextoCriptografado(u.Senha);
            new UsuarioService().Inserir(u);
            return RedirectToAction("CadastroRealizado");
        }

        public IActionResult CadastroRealizado()
        {
            return View();
        }

        //Listagem---------------------------------------------------------------------------------
        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaTipoUser(this);

            return View(new UsuarioService().Listar());
        }

        //Edição-----------------------------------------------------------------------------------
        public IActionResult EdicaoUser(int id)
        {
            Usuario u = new UsuarioService().BuscaId(id);
            return View(u);
        }

        [HttpPost]
        public IActionResult EdicaoUser(Usuario editU)
        {
            new UsuarioService().Editar(editU);
            return RedirectToAction("ListaDeUsuarios");
        }

        //Exclusão--------------------------------------------------------------------------------
        public IActionResult ExcluirUser(int id)
        {
            return View(new UsuarioService().BuscaId(id));
        }
        
        [HttpPost]
        public IActionResult ExcluirUser(string decisao, int id)
        {
            if (decisao == "EXCLUIR")
            {
                ViewData["Mensagem"] = "Exclusão do usuário " + new UsuarioService().BuscaId(id).Nome + " realizada com sucesso";
                new UsuarioService().Excluir(id);
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
            else
            {
                ViewData["Mensagem"] = "Exclusão cancelada";
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
        }
    }
}