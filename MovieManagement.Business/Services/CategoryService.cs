using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repositorio;

        public CategoryService(ICategoryRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void AdicionarCategoria(string nome)
        {
            // Regra de negócio: nome obrigatório
            if (string.IsNullOrEmpty(nome))
                throw new Exception("O nome da categoria não pode ser vazio.");

            // Regra de negócio: não permitir categorias duplicadas
            var existente = _repositorio.ProcurarPorNome(nome);
            if (existente != null)
                throw new Exception("Esta categoria já se encontra registada.");

            Category novaCategoria = new Category();
            novaCategoria.Nome = nome;

            _repositorio.Adicionar(novaCategoria);
        }

        // ESTES MÉTODOS ESTAVAM EM FALTA E CAUSAVAM OS ERROS NA UI:
        public List<Category> ListarTodos()
        {
            return _repositorio.ListarTodos();
        }

        public Category? ProcurarCategoria(string nome)
        {
            return _repositorio.ProcurarPorNome(nome);
        }

        public void RemoverCategoria(int id)
        {
            bool removido = _repositorio.Remover(id);
            if (!removido)
                throw new Exception("Não foi possível encontrar a categoria para remoção.");
        }
    }
}