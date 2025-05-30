

using CaixaEletronico.Models;

var conta = new Conta();

Console.WriteLine("Bem-vindo ao Caixa Eletrônico");
Console.WriteLine("\nDigite o número da conta: 1234");
conta.NumeroConta = int.Parse(Console.ReadLine());
Console.WriteLine("\nDigite a senha: ****");
conta.Senha = int.Parse(Console.ReadLine());


