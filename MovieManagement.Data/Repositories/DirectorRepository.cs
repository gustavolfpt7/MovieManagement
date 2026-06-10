using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MovieManagement.Data.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        // Persistência em memória para Realizadores, mesmo padrão dos outros repositórios
        private List<Director> _diretores;
        private int _proximoId;

        public DirectorRepository()
        {
            _diretores = new List<Director>();
            _proximoId = 1;
        }

        public void Adicionar(Director realizador)
        {
            realizador.Id = _proximoId++;
            _diretores.Add(realizador);
        }

        public List<Director> ListarTodos()
        {
            return _diretores;
        }

        public Director ProcurarPorNome(string nome)
        {
            foreach (Director r in _diretores)
            {
                if (r.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return r;
                }
            }
            return null;
        }

        public bool Remover(int id)
        {
            Director? diretor = null;

            foreach (Director d in _diretores)
            {
                if (d.Id == id)
                {
                    diretor = d;
                    break;
                }
            }

            if (diretor != null)
            {
                _diretores.Remove(diretor);
                return true;
            }
            return false;
        }
    }
}
