using System;
using System.Collections.Generic;
using System.Linq;
using MovieManagement.Domain.Interfaces;
using MovieManagement.Domain.Entities;

namespace MovieManagement.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private List<Movie> _movies; //convenção para quando o atributo é privado (iniciar com underscore)
        private int _proximoId;

        public MovieRepository()
        {
            _movies = new List<Movie>();
            _proximoId = 1;
        }

        public void Adicionar(Movie movie) //passa o novo objeto movie como parametro para add na lista.
        {
            movie.Id = _proximoId++; // Atribui o ID atual e incrementa para o próximo
            _movies.Add(movie);   // Adiciona o filme à lista
        }

        
        public List<Movie> ListarTodos()
        {
            return _movies; // Retorna a lista com todos os filmes 
        }

        public Movie ProcurarPorTitulo(string titulo)
        {
            foreach (Movie m in _movies) //Percorre a lista senso m o filme da vez 
            {
                if (m.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase)) //Compara o file da vez com o parametro e adiciona
                                                                                 //facilitações para a correspondencia, ignorando caps
                {
                    return m; // Se corresponde retorna o file da vez como resultado
                }
            }
            return null; // se não há correspondência retona null

        }

        // Metodo que responde com T/F, recebe ium parametro int
        public bool Remover(int id)
        {
            Movie? movie = null; // var temporária Movie que aceita ser null "?" 

            foreach (Movie m in _movies) //loop que percorre a lista sendo m o filme da vez
            {
                if (m.Id == id) // Testa cada ID contra o parâmetro 
                {
                    movie = m; // Se igual a var temporária movie guarda o filme da vez no loop
                    break; // Interrompe pois o filme foi encontrado
                }
            }

            if (movie != null) // Aqui se testa se a var temporária deixou de ser nula, ou seja,
                               // se encontrou a correspondencia, sendo true remove o filme e
                               // retorna true, se não houve correspondencia retorna false.
            {
                _movies.Remove(movie);
                return true;
            }
            return false;
        }
    }
}
