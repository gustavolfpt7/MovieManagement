using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Business.Services
{
    public class MovieService
    {
        // Criar campo privado que guarda a referência para o repositório de filmes, ser 'readonly'
        // garante que essa referência não seja alterada depois de inicializada.

        private readonly IMovieRepository _repositorio;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IDirectorRepository _directorRepo;

        // O construtor injeta dependência a classe business não cria o repositório usando 'new',
        // ele recebe o repositório pronto através do construtor.
        public MovieService(IMovieRepository repositorio, ICategoryRepository categoryRepo, IDirectorRepository directorRepo)
        {
            _repositorio = repositorio;
            _categoryRepo = categoryRepo;
            _directorRepo = directorRepo;
        }

        // Aplicar regra de negócio para Adicionar um Filme
        public void AdicionarFilme(string titulo, int ano, string lingua, double classificacao, int categoryId, int directorId)
        {
            
            if (string.IsNullOrEmpty(titulo)) // testa se é nulo ou vazio para caso seja, não adicionar
                                              // e lançar a exceção
            {
                throw new Exception("O título do filme não pode ser vazio.");
            }

            if (string.IsNullOrEmpty(lingua))
            {
                throw new Exception("O idioma do filme deve ser informado.");
            }

            if (classificacao < 0 || classificacao > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(classificacao), "A classificação deve estar obrigatoriamente entre 0 e 5.");
            }

            var existente = _repositorio.ProcurarPorTitulo(titulo);
            
            if (existente != null)
            {
                    throw new Exception("Já existe um filme com esse título.");
            }

            // Bloqueia se o ID da categoria não existir na lista

            var categoriaExistente = _categoryRepo.ListarTodos().Find(c => c.Id == categoryId);
            if (categoriaExistente == null)
                throw new Exception("A categoria informada não existe no sistema.");

            // Bloqueia se o ID do diretor não existir na lista

            var directorExistente = _directorRepo.ListarTodos().Find(d => d.Id == directorId);
            if (directorExistente == null)
                throw new Exception("O realizador informado não existe no sistema.");

            // Se os dados passarem nas regras acima, criamos a entidade Filme
            Movie novoFilme = new Movie();
            novoFilme.Titulo = titulo;
            novoFilme.Ano = ano;
            novoFilme.Lingua = lingua;
            novoFilme.Classificacao= classificacao;

            novoFilme.CategoryId = categoryId;
            novoFilme.DirectorId = directorId;

            // A camada de negócio prepara o objeto e delega o salvamento para a camada de dados
            _repositorio.Adicionar(novoFilme);
        }

        // Aplicar regra para listar filmes
        public List<Movie> ListarTodos()
        {
            
            return _repositorio.ListarTodos(); // Ponte direta: pede para o repositório buscar todos e devolve para a UI
        }

        // Aplicar regra para procurar filme pelo nome
        public Movie? ProcurarFilme(string titulo) 
        {
            
            return _repositorio.ProcurarPorTitulo(titulo); // Retorna o filme encontrado ou 'null' (indicado pelo Movie?) caso não exista
        }

        // Aplicar regra para remover um Filme
        public void RemoverFilme(int id)
        {
            
            bool removido = _repositorio.Remover(id); // O serviço pede para a camada de dados remover e descobre se deu certo (true ou false)

          
            if (!removido) // Se não removido lança a exceção
            {
                throw new Exception("Não foi possível encontrar o filme para remoção.");
            }

        }
    }
}
