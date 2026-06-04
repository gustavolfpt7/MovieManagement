using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace MovieManagement.Domain.Interfaces
{
    public interface IMovieRepository
    {
        // Insterface que pertence ao domain para firmar o "contrato", designar
        // as assinaturas dõs métodos que deverão ser implementados pela classe
        // data seguindo as regras de negócio da classe business

        void Adicionar(Movie movie);             // Void pois não retorna objeto, apenas opera alteração ao
                                                 // passar parâmetro da classe Movie
        List<Movie> ListarTodos();               // Retorna tipo Lista de objetos do tipo Movie
        Movie ProcurarPorTitulo(string titulo);  // Retorna um objeto do tipo Movie através de parametro do tipo string
        bool Remover(int id);                    // Bool pois retorna T/F, opera alteração ao
                                                 // passar parâmetro da classe int
    }
}
