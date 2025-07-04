using CaixaEletronico.Models;
using CaixaEletronico.Services;
using SQLitePCL;


Batteries.Init();


var conta = new LoginContaDTO();
var dbPath = "caixaEletronico.db";
var registraConta = new RegistraConta(dbPath);
int opcao;

Console.WriteLine("Bem-vindo ao Caixa Eletrônico");
Console.WriteLine("\nSelecione uma opção para prosseguir");
Console.WriteLine("\n*******************************");
Console.WriteLine("\n1 - Para se Cadastrar");
Console.WriteLine("\n2 - Para Consultar Saldo");
Console.WriteLine("\n3 - Para Sacar");
Console.WriteLine("\n4 - Para Tranferir");
Console.WriteLine("\n*******************************");
opcao = int.Parse(Console.ReadLine());



switch (opcao)
{
    case 1:
        Console.Clear();
        Console.WriteLine("\nTela de Cadastro de Conta");

        Console.WriteLine("Digite o nome do titula: ");
        string nomeTitular = Console.ReadLine();

        Console.WriteLine("Digite a senha, ela deve ter 4 digitos: ");
        string senha = Console.ReadLine();

        registraConta.CriaConta(nomeTitular, senha);

        break;

    case 2:
        Console.WriteLine($"\nVocê escolheu a opção {opcao} e seu saldo é: ");
        break;

    case 3:
        Console.WriteLine($"\nVocê escolheu a opção {opcao} e será direcionado à tela de Saques");
        break;

    case 4:
        Console.WriteLine($"\nVocê escolheu a opção {opcao} e será direcionado para a tela de transferencia.");
        break;

    default:
        Console.WriteLine("Opção inválida, escolha novamente.");
        break;
}