using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System.Collections.Generic;

namespace MovieManagement.Business.Services
{
    public class DirectorService
    {
        // Injeção de dependência — mesmo padrão dos outros services
        private readonly IDirectorRepository _repositorio;

        public DirectorService(IDirectorRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void AdicionarDirector(string nome, string pais)
        {
            // Regra de negócio: nome obrigatório
            if (string.IsNullOrEmpty(nome))
                throw new Exception("O nome do realizador não pode ser vazio.");

            // Regra de negócio: país obrigatório
            if (string.IsNullOrEmpty(pais))
                throw new Exception("O país do realizador não pode ser vazio.");

            Director novoDirector = new Director();
            novoDirector.Nome = nome;
            novoDirector.Pais = pais;

            _repositorio.Adicionar(novoDirector);
        }

        public List<Director> ListarTodos()
        {
            return _repositorio.ListarTodos();
        }

        public Director? ProcurarDiretor(string nome)
        {
            return _repositorio.ProcurarPorNome(nome);
        }

        public void RemoverDiretor(int id)
        {
            bool removido = _repositorio.Remover(id);

            if (!removido)
                throw new Exception("Não foi possível encontrar o diretor para remoção.");
        }
    }
}