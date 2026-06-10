using System;
using MovieManagement.Business.Services;
using MovieManagement.Data.Repositories;
using MovieManagement.Domain.Entities;

namespace MovieManagementUi
{
    class Program
    {
        // Instanciação única dos repositórios em memória nesta fase 
        private static readonly MovieSqliteRepository _movieRepo = new MovieSqliteRepository();
        private static readonly CategorySqliteRepository _categoryRepo = new CategorySqliteRepository();
        private static readonly DirectorSqliteRepository _directorRepo = new DirectorSqliteRepository();

        // Inicialização dos serviços injetando os repositórios
        private static readonly MovieService _movieService = new MovieService(_movieRepo, _categoryRepo, _directorRepo);
        private static readonly CategoryService _categoryService = new CategoryService(_categoryRepo);
        private static readonly DirectorService _directorService = new DirectorService(_directorRepo);

        static void Main(string[] args)
        {
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE GESTÃO DE FILMES ===");
                Console.WriteLine("1. Gerir Filmes");
                Console.WriteLine("2. Gerir Categorias");
                Console.WriteLine("3. Gerir Realizadores");
                Console.WriteLine("4. Sair");
                Console.Write("Escolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1: MenuFilmes(); break;
                        case 2: MenuCategorias(); break;
                        case 3: MenuRealizadores(); break;
                        case 4: Console.WriteLine("A encerrar o programa..."); break;
                        default: Console.WriteLine("Opção inválida!"); AguardarTecla(); break;
                    }
                }
            } while (opcao != 4);
        }

        #region MENUS DE GESTÃO

        static void MenuFilmes()
        {
            Console.Clear();
            Console.WriteLine("--- GESTÃO DE FILMES ---");
            Console.WriteLine("1. Adicionar Filme");
            Console.WriteLine("2. Listar Filmes");
            Console.WriteLine("3. Procurar Filme por Título");
            Console.WriteLine("4. Remover Filme");
            Console.Write("Opção: ");

            string op = Console.ReadLine();
            try
            {
                switch (op)
                {
                    case "1":
                        Console.Write("Título: "); string t = Console.ReadLine();
                        Console.Write("Ano: "); int ano = int.Parse(Console.ReadLine());
                        Console.Write("Língua: "); string l = Console.ReadLine();
                        Console.Write("Classificação (0-5): "); double c = double.Parse(Console.ReadLine());
                        // Pergunta pelos IDs que servirão de relação
                        Console.Write("ID da Categoria existente: "); int catId = int.Parse(Console.ReadLine());
                        Console.Write("ID do Realizador existente: "); int dirId = int.Parse(Console.ReadLine());
                        _movieService.AdicionarFilme(t, ano, l, c, catId, dirId);
                        Console.WriteLine("Filme adicionado com sucesso!");
                        break;
                    case "2":
                        var filmes = _movieService.ListarTodos();
                        if (filmes.Count == 0) Console.WriteLine("Nenhum filme registado.");
                        foreach (var f in filmes) Console.WriteLine($"[{f.Id}] {f.Titulo} ({f.Ano}) - {f.Classificacao}/5");
                        break;
                    case "3":
                        Console.Write("Digite o título: "); string proc = Console.ReadLine();
                        var encontrado = _movieService.ProcurarFilme(proc);
                        if (encontrado != null) Console.WriteLine($"Encontrado: {encontrado.Id} | {encontrado.Titulo} | Língua: {encontrado.Lingua} | Ano: {encontrado.Ano} | Classificação: {encontrado.Classificacao} |");
                        else Console.WriteLine("Filme não encontrado.");
                        break;
                    case "4":
                        Console.Write("ID do filme a remover: "); int idRem = int.Parse(Console.ReadLine());
                        _movieService.RemoverFilme(idRem);
                        Console.WriteLine("Filme removido!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}"); // Captura as exceções de validação do Service!
            }
            AguardarTecla();
        }

        static void MenuCategorias()
        {
            Console.Clear();
            Console.WriteLine("--- GESTÃO DE CATEGORIAS ---");
            Console.WriteLine("1. Adicionar Categoria");
            Console.WriteLine("2. Listar Categorias");
            Console.WriteLine("3. Procurar Categoria");
            Console.WriteLine("4. Remover Categoria");
            Console.Write("Opção: ");

            string op = Console.ReadLine();
            try
            {
                switch (op)
                {
                    case "1":
                        Console.Write("Nome da Categoria: "); string nome = Console.ReadLine();
                        _categoryService.AdicionarCategoria(nome);
                        Console.WriteLine("Categoria guardada!");
                        break;
                    case "2":
                        var listas = _categoryService.ListarTodos();
                        if (listas.Count == 0) Console.WriteLine("Nenhuma categoria registada.");
                        foreach (var cat in listas) Console.WriteLine($"[{cat.Id}] {cat.Nome}");
                        break;
                    case "3":
                        Console.Write("Nome a procurar: "); string pCat = Console.ReadLine();
                        var c = _categoryService.ProcurarCategoria(pCat);
                        if (c != null) Console.WriteLine($"Categoria Existente: [{c.Id}] {c.Nome}");
                        else Console.WriteLine("Categoria não encontrada.");
                        break;
                    case "4":
                        Console.Write("ID da categoria a remover: "); int id = int.Parse(Console.ReadLine());
                        _categoryService.RemoverCategoria(id);
                        Console.WriteLine("Categoria eliminada!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            AguardarTecla();
        }

        static void MenuRealizadores()
        {
            Console.Clear();
            Console.WriteLine("--- GESTÃO DE REALIZADORES ---");
            Console.WriteLine("1. Adicionar Realizador");
            Console.WriteLine("2. Listar Realizadores");
            Console.WriteLine("3. Procurar Realizador");
            Console.WriteLine("4. Remover Realizador");
            Console.Write("Opção: ");

            string op = Console.ReadLine();
            try
            {
                switch (op)
                {
                    case "1":
                        Console.Write("Nome: "); string n = Console.ReadLine();
                        Console.Write("País: "); string p = Console.ReadLine();
                        _directorService.AdicionarDirector(n, p);
                        Console.WriteLine("Realizador registado!");
                        break;
                    case "2":
                        var listaDir = _directorService.ListarTodos();
                        if (listaDir.Count == 0) Console.WriteLine("Nenhum realizador registado.");
                        foreach (var d in listaDir) Console.WriteLine($"[{d.Id}] {d.Nome} ({d.Pais})");
                        break;
                    case "3":
                        Console.Write("Nome a procurar: "); string pDir = Console.ReadLine();
                        var dir = _directorService.ProcurarDiretor(pDir);
                        if (dir != null) Console.WriteLine($"Encontrado: [{dir.Id}] {dir.Nome} - {dir.Pais}");
                        else Console.WriteLine("Realizador não encontrado.");
                        break;
                    case "4":
                        Console.Write("ID do realizador a remover: "); int id = int.Parse(Console.ReadLine());
                        _directorService.RemoverDiretor(id);
                        Console.WriteLine("Realizador removido!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            AguardarTecla();
        }

        #endregion

        static void AguardarTecla()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}