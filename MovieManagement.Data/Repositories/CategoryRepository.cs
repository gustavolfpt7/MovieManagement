using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MovieManagement.Data.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private List<Category> _categorias;
        private int _proximoId;

        public CategoryRepository()
        {
            _categorias = new List<Category>();
            _proximoId = 1;
        }

        public void Adicionar(Category categoria)
        {
            categoria.Id = _proximoId++; // Atribui ID e incrementa para o próximo
            _categorias.Add(categoria);
        }

        public List<Category> ListarTodos()
        {
            return _categorias; // Devolve a lista completa
        }

        public Category ProcurarPorNome(string nome)
        {
            foreach (Category c in _categorias)
            {
                // Ignora maiúsculas/minúsculas, igual ao MovieRepository
                if (c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return c;
                }
            }
            return null; // Não encontrado
        }

        public bool Remover(int id)
        {
            Category? categoria = null;

            foreach (Category c in _categorias)
            {
                if (c.Id == id)
                {
                    categoria = c;
                    break; // Encontrou, interrompe o loop
                }
            }

            if (categoria != null)
            {
                _categorias.Remove(categoria);
                return true; // Removido com sucesso
            }
            return false; // Não encontrado
        }
    }
}
