using DesafioFundamentos.Models;
using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Seja bem-vindo ao sistema de estacionamento!");
        Console.WriteLine("Digite o preço inicial:");
        decimal precoInicial = LerDecimal();

        Console.WriteLine("Digite o preço por hora:");
        decimal precoPorHora = LerDecimal();

        var estacionamento = new Estacionamento(precoInicial, precoPorHora);

        string opcao;
        bool exibirMenu = true;

        while (exibirMenu)
        {
            Console.Clear();
            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1 - Cadastrar veículo");
            Console.WriteLine("2 - Remover veículo");
            Console.WriteLine("3 - Listar veículos");
            Console.WriteLine("4 - Encerrar");

            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    estacionamento.AdicionarVeiculo();
                    break;

                case "2":
                    estacionamento.RemoverVeiculo();
                    break;

                case "3":
                    estacionamento.ListarVeiculos();
                    break;

                case "4":
                    exibirMenu = false;
                    Console.WriteLine("Encerrando o sistema...");
                    break;

                default:
                    Console.WriteLine("Opção inválida, tente novamente.");
                    break;
            }

            if (exibirMenu)
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    static decimal LerDecimal()
    {
        while (true)
        {
            string entrada = Console.ReadLine();
            if (decimal.TryParse(entrada, out decimal valor) && valor >= 0)
                return valor;

            Console.WriteLine("Valor inválido, tente novamente:");
        }
    }
}