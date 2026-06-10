using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Interfaces
{
    public interface IDirectorRepository
    {
        void Adicionar(Director diretor);          // Adiciona um realizador
        List<Director> ListarTodos();                 // Retorna todos os realizadores
        Director ProcurarPorNome(string nome);        // Procura realizador por nome
        bool Remover(int id);
    }
}
