using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Entities
{
    public class Director
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Pais { get; set; } = null!;

        // Construtor vazio para uso pela BD
        public Director() { }

        // Construtor com parâmetros por boa prática arquitetural
        public Director(string nome, string pais)
        {
            Nome = nome;
            Pais = pais;
        }
    }
}
