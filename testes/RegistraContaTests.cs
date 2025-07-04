using CaixaEletronico.Services;
using Microsoft.Data.Sqlite;
using Xunit;
using System;
using System.IO;

namespace testes
{
    public class RegistraContaTests : IDisposable
    {
        private readonly string _testdbPath = "test_caixaEletronico.db";
        private readonly SqliteConnection _connection;
        private readonly RegistraConta _services;

        public RegistraContaTests()
        {
            // Abre conexão com o mesmo banco usado pelo serviço
            _connection = new SqliteConnection($"Data Source={_testdbPath}");
            _connection.Open();

            // Garante que a tabela existe
            CreateTestDatabase();

            _services = new RegistraConta(_testdbPath);
        }

        private void CreateTestDatabase()
        {
            using var command = _connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Contas (
                    id TEXT PRIMARY KEY,
                    numeroConta INTEGER UNIQUE,
                    nomeTitular TEXT UNIQUE,
                    senha TEXT,
                    saldo REAL
                )";
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            // Limpa a tabela pra não impactar outros testes
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Contas";
            cmd.ExecuteNonQuery();

            _connection.Close();
        }

        [Fact]
        public void CriaConta_ComDadosValidos_DeveCriarConta()
        {
            // Arrange
            var nome = "Teste";
            var senha = "1234";

            // Act
            _services.CriaConta(nome, senha);

            // Assert
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Contas WHERE nomeTitular = $nome";
            cmd.Parameters.AddWithValue("$nome", nome);
            var count = (long)cmd.ExecuteScalar();
            Assert.Equal(1, count);
        }

        [Fact]
        public void CriaConta_ComNomeVazio_NaoDeveCriarConta()
        {

            // arrange
            var nome = "";
            var senha = "1234";

            // act
            _services.CriaConta(nome, senha);

            // assert
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Contas WHERE senha = $senha";
            cmd.Parameters.AddWithValue("$senha", senha);
            var count = (long)cmd.ExecuteScalar(); 
            Assert.Equal(0, count);

        }

        [Fact]
        public void CriaConta_ComSenhaMenorQue4_NaoDeveCriarConta()
        {
            // arrange
            var nome = "Teste";
            var senha = "12";

            // act
            _services.CriaConta(nome, senha);

            // assert
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Contas WHERE nomeTitular = $nome";
            cmd.Parameters.AddWithValue("$nome", nome);
            var count = (long)cmd.ExecuteScalar();
            Assert.Equal(0, count);

        }



    }
}
