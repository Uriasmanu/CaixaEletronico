using Microsoft.Data.Sqlite;
using System;
using System.Linq;

namespace CaixaEletronico.Services
{
    public class RegistraConta
    {
        public void CriaConta(string nomeTitular, string senha)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(nomeTitular))
            {
                Console.WriteLine("O nome do titular não pode estar vazio.");
                return;
            }

            if (senha.Length != 4 || !senha.All(char.IsDigit))
            {
                Console.WriteLine("A senha deve ter exatamente 4 dígitos numéricos.");
                return;
            }

            var id = Guid.NewGuid().ToString();
            var numeroConta = new Random().Next(100000, 999999);
            var saldo = 0.0;

            using (var connection = new SqliteConnection("Data Source=caixaEletronico.db"))
            {
                connection.Open();

                // Primeiro cria a tabela se não existir
                CriarTabelaSeNaoExistir(connection);

                try
                {
                    // Verifica se titular já existe
                    if (TitularExiste(connection, nomeTitular))
                    {
                        Console.WriteLine("Já existe uma conta com este nome de titular.");
                        return;
                    }

                    // Insere nova conta
                    InserirConta(connection, id, numeroConta, nomeTitular, senha, saldo);

                    Console.WriteLine("\nConta registrada com sucesso!");
                    Console.WriteLine($"Número da conta: {numeroConta}");
                    Console.WriteLine($"Titular: {nomeTitular}");
                }
                catch (SqliteException ex)
                {
                    Console.WriteLine($"Erro ao registrar conta: {ex.Message}");
                }
            }
        }

        private void CriarTabelaSeNaoExistir(SqliteConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
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
        }

        private bool TitularExiste(SqliteConnection connection, string nomeTitular)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM Contas WHERE nomeTitular = $nomeTitular";
                command.Parameters.AddWithValue("$nomeTitular", nomeTitular);
                return (long)command.ExecuteScalar() > 0;
            }
        }

        private void InserirConta(SqliteConnection connection, string id, int numeroConta,
                                string nomeTitular, string senha, double saldo)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    INSERT INTO Contas (id, numeroConta, nomeTitular, senha, saldo)
                    VALUES ($id, $numeroConta, $nomeTitular, $senha, $saldo)";

                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$numeroConta", numeroConta);
                command.Parameters.AddWithValue("$nomeTitular", nomeTitular);
                command.Parameters.AddWithValue("$senha", senha);
                command.Parameters.AddWithValue("$saldo", saldo);

                command.ExecuteNonQuery();
            }
        }
    }
}