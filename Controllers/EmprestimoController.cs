using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace Biblioteca.Controllers
{
    
    public class EmprestimoController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            LivroService livroService = new LivroService();
            EmprestimoService emprestimoService = new EmprestimoService();

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarDisponiveis();
            return View(cadModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CadEmprestimoViewModel viewModel)
        {
            EmprestimoService emprestimoService = new EmprestimoService();
            
            if(viewModel.Emprestimo.Id == 0)
            {
                emprestimoService.Inserir(viewModel.Emprestimo);
            }
            else
            {
                emprestimoService.Atualizar(viewModel.Emprestimo);
            }
            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, string intensPorPagina, int numDaPagina, int paginaAtual)
        {
            Autenticacao.CheckLogin(this);
            FiltrosEmprestimos empFiltro = null;
            if (!string.IsNullOrEmpty(filtro))
            {
                empFiltro = new FiltrosEmprestimos();
                empFiltro.Filtro = filtro;
                empFiltro.TipoFiltro = tipoFiltro;
            }

                ViewData["emprestimosPorPagina"] = (string.IsNullOrEmpty(intensPorPagina) ? 10 : Int32.Parse(intensPorPagina));
                ViewData["paginaAtual"] = (paginaAtual != 0 ? paginaAtual : 1);

            EmprestimoService service = new EmprestimoService();
            return View(service.ListarTodos(empFiltro));
        }

        public IActionResult Edicao(int id)
        {
            LivroService livroService = new LivroService();
            EmprestimoService em = new EmprestimoService();
            Emprestimo e = em.ObterPorId(id);

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarDisponiveis();
            cadModel.Emprestimo = e;
            
            return View(cadModel);
        }
    }
}