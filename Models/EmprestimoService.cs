using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class EmprestimoService
    {
        public void Inserir(Emprestimo e)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;
                emprestimo.Devolvido = e.Devolvido;
                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro = null)
        {
            using (BibliotecaContext context = new BibliotecaContext())
            {
                IQueryable<Emprestimo> pesquisa;
                if (filtro != null)
                {
                    switch (filtro.TipoFiltro)
                    {
                        case "Usuario":
                            pesquisa = context.Emprestimos.Where(e => e.NomeUsuario.Contains(filtro.Filtro)).Include(e => e.Livro);
                            break;
                        case "Livro":
                            pesquisa = context.Emprestimos.Include(e => e.Livro).Where(e => e.Livro.Titulo.Contains(filtro.Filtro));
                            break;
                        default:
                            pesquisa = context.Emprestimos.Include(e => e.Livro).Where(e => e.Livro.Autor.Contains(filtro.Filtro));
                            break;
                    }
                }
                else
                {
                    pesquisa = context.Emprestimos.Include(e => e.Livro);
                }
                return pesquisa.OrderByDescending(p => p.DataDevolucao).ToList();
            }
        }

        public Emprestimo ObterPorId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }
    }
}