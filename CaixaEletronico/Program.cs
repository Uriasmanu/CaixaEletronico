

using CaixaEletronico.Models;
using System.Linq.Expressions;

var conta = new LoginContaDTO();
int opcao;

Console.WriteLine("Bem-vindo ao Caixa Eletrônico");
Console.WriteLine("\nSelecione uma opção para prosseguir");
Console.WriteLine("\n*******************************");
Console.WriteLine("\n1 - Para se Cadastrar");
Console.WriteLine("\n2 - Para Consultar Saldo");
Console.WriteLine("\n3 - Para Sacar");
Console.WriteLine("\n1 - Para Tranferir");
Console.WriteLine("\n*******************************");
opcao = int.Parse(Console.ReadLine());



switch (opcao)
{
    case 1:
        Console.WriteLine($"\nVocê escolheu a opção {opcao} e será direcionado à tela de Cadastro");
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



