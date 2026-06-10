using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Data.Repositories
{
    public class MovieSqliteRepository : IMovieRepository
    {
        private string _connectionString = "Data Source = moviemanagement.db";

        // Criação da tabela
        public MovieSqliteRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            // Tabela criada com todos os campos do seu modelo, incluindo os IDs de relação
            string sql = @"CREATE TABLE IF NOT EXISTS Movies (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Titulo TEXT NOT NULL,
                Ano INTEGER NOT NULL,
                Lingua TEXT NOT NULL,
                Classificacao REAL NOT NULL,
                CategoryId INTEGER NOT NULL,
                DirectorId INTEGER NOT NULL);";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        // Implementação dos métodos da interface IMovieRepository

        public void Adicionar(Movie movie)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"INSERT INTO Movies (Titulo, Ano, Lingua, Classificacao, CategoryId, DirectorId) 
                           VALUES (@t, @a, @l, @c, @catId, @dirId)";

            using var cmd = new SqliteCommand(sql, con);

            cmd.Parameters.AddWithValue("@t", movie.Titulo);
            cmd.Parameters.AddWithValue("@a", movie.Ano);
            cmd.Parameters.AddWithValue("@l", movie.Lingua);
            cmd.Parameters.AddWithValue("@c", movie.Classificacao);
            cmd.Parameters.AddWithValue("@catId", movie.CategoryId);
            cmd.Parameters.AddWithValue("@dirId", movie.DirectorId);

            cmd.ExecuteNonQuery();
        }

        public List<Movie> ListarTodos()
        {
            List<Movie> lista = new();

            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Titulo, Ano, Lingua, Classificacao, CategoryId, DirectorId FROM Movies";

            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Movie
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2),
                    Lingua = reader.GetString(3),
                    Classificacao = reader.GetDouble(4),
                    CategoryId = reader.GetInt32(5),
                    DirectorId = reader.GetInt32(6)
                });
            }
            return lista;
        }

        public Movie? ProcurarPorTitulo(string titulo)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Titulo, Ano, Lingua, Classificacao, CategoryId, DirectorId 
                           FROM Movies WHERE Titulo = @t";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@t", titulo);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Movie
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2),
                    Lingua = reader.GetString(3),
                    Classificacao = reader.GetDouble(4),
                    CategoryId = reader.GetInt32(5),
                    DirectorId = reader.GetInt32(6)
                };
            }
            return null;
        }

        public bool Remover(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"DELETE FROM Movies WHERE Id = @id";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            int linhas = cmd.ExecuteNonQuery();
            return linhas > 0;
        }
    }
}