using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        void Adicionar(Category categoria);            // Adiciona uma categoria
        List<Category> ListarTodos();                  // Retorna todas as categorias
        Category ProcurarPorNome(string nome);         // Procura categoria por nome
        bool Remover(int id);
    }
}
