using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Entities
{
    public class Movie
    {

        // Camada Domain, define o que o sistema contám, no caso os atributos:

        public int Id { get; set; }
        public string Titulo { get; set; } = null!; //Operador null forgiving, inicia nulo porém vai ser preenchido
        public int Ano { get; set; }
        public string Lingua { get; set; }
        public double Classificacao { get; set; }

        //Construtor vazio para ser usado para BD "criar" objetos posteriormente
        public Movie() { }

        //Criação de construtor mesmo não sendo necessário para o funcionamento
        //por ser boa prática arquitetural

        public Movie (string titulo, int ano, string lingua, double classificacao)
        {
            Titulo = titulo;
            Ano = ano;
            Lingua = lingua;
            Classificacao = classificacao;

            // ID é ignorado pois será gerado por auto increment
        }

    }
}
