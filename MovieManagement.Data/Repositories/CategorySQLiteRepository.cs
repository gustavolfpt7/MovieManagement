using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Data.Repositories
{
    public class CategorySqliteRepository : ICategoryRepository
    {
        private string _connectionString = "Data Source = moviemanagement.db";

        public CategorySqliteRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Categories (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL);";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        public void Adicionar(Category categoria)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"INSERT INTO Categories (Nome) VALUES (@n)";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", categoria.Nome);
            cmd.ExecuteNonQuery();
        }

        public List<Category> ListarTodos()
        {
            List<Category> lista = new();
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Nome FROM Categories";
            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Category { Id = reader.GetInt32(0), Nome = reader.GetString(1) });
            }
            return lista;
        }

        public Category? ProcurarPorNome(string nome)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Nome FROM Categories WHERE Nome = @n";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", nome);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Category { Id = reader.GetInt32(0), Nome = reader.GetString(1) };
            }
            return null;
        }

        public bool Remover(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"DELETE FROM Categories WHERE Id = @id";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}