using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico.Services
{
    internal class RegistraConta
    {
        public void CriaConta(string nomeTitular, string senha)
        {
            if (string.IsNullOrWhiteSpace(nomeTitular) || senha.Length != 4) 
            {
                Console.WriteLine("O titular não  pode ser nulo e a senha deve ter exatamente 4 dígitos.");
                return;
            }

            var id = Guid.NewGuid().ToString();
            var random = new Random();
            var numeroConta = random.Next(10);
            var saldo = 0.0;

            using var connection = new SqliteConnection("Data Source=caixaEletronico.db");
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Contas (
                    id TEXT PRIMARY KEY,
                    numeroConta INTEGER,
                    nomeTitular TEXT,
                    senha TEXT,
                    saldo REAL
                );

                INSERT INTO Contas (id, numeroConta, nomeTitular, senha, saldo)
                VALUES ($id, $numeroConta, $nomeTitular, $senha, $saldo);
            ";


            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("numeroConta", numeroConta);
            command.Parameters.AddWithValue("nomeTitular", nomeTitular);
            command.Parameters.AddWithValue("senha", senha);
            command.Parameters.AddWithValue("saldo", saldo);

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Conta registrada com sucesso!");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Erro ao registrar conta: {ex.Message}");
            }
        }
    }
}
