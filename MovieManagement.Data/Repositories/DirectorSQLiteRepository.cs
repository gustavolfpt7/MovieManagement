using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Data.Repositories
{
    public class DirectorSqliteRepository : IDirectorRepository
    {
        private string _connectionString = "Data Source = moviemanagement.db";

        public DirectorSqliteRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Directors (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Pais TEXT NOT NULL);";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        public void Adicionar(Director diretor)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"INSERT INTO Directors (Nome, Pais) VALUES (@n, @p)";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", diretor.Nome);
            cmd.Parameters.AddWithValue("@p", diretor.Pais);
            cmd.ExecuteNonQuery();
        }

        public List<Director> ListarTodos()
        {
            List<Director> lista = new();
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Nome, Pais FROM Directors";
            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Director { Id = reader.GetInt32(0), Nome = reader.GetString(1), Pais = reader.GetString(2) });
            }
            return lista;
        }

        public Director? ProcurarPorNome(string nome)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Nome, Pais FROM Directors WHERE Nome = @n";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@n", nome);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Director { Id = reader.GetInt32(0), Nome = reader.GetString(1), Pais = reader.GetString(2) };
            }
            return null;
        }

        public bool Remover(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"DELETE FROM Directors WHERE Id = @id";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}