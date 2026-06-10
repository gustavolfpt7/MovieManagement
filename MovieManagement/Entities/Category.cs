using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        // Construtor vazio para uso pela BD
        public Category() { }

        // Construtor com parâmetros por boa prática arquitetural
        public Category(string nome)
        {
            Nome = nome;
            // ID ignorado pois será gerado por auto increment
        }
    }
}
