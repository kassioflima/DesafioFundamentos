using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            var entrada = Console.ReadLine();

            var placa = (entrada ?? string.Empty).Trim().ToUpperInvariant();

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine("Placa não informada.");
                return;
            }

            // Validação simples de placa (BR antiga AAA-0A00 ou Mercosul ABC1D23)
            // Opcional: remova se não quiser validar formato
            var padraoPlaca = new Regex(@"^[A-Z]{3}\-?[0-9][A-Z0-9][0-9]{2}$");
            if (!padraoPlaca.IsMatch(placa.Replace("-", "")))
            {
                Console.WriteLine("Placa em formato inválido.");
                return;
            }

            // Normaliza removendo hífen
            placa = placa.Replace("-", "");

            if (veiculos.Any(v => v.Equals(placa, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Esse veículo já está estacionado.");
                return;
            }

            veiculos.Add(placa);
            Console.WriteLine($"Veículo {placa} estacionado com sucesso.");
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            var entrada = Console.ReadLine();
            var placa = (entrada ?? string.Empty).Trim().ToUpperInvariant().Replace("-", "");

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine("Placa não informada.");
                return;
            }

            // Verifica se o veículo existe
            var index = veiculos.FindIndex(x => x.Equals(placa, StringComparison.OrdinalIgnoreCase));

            if (index >= 0)
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                var horasStr = Console.ReadLine();
                if (!int.TryParse(horasStr, out int horas) || horas < 0)
                {
                    Console.WriteLine("Quantidade de horas inválida.");
                    return;
                }

                decimal valorTotal = precoInicial + (precoPorHora * horas);

                // Remove a placa da lista de veículos
                veiculos.RemoveAt(index);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal:0.00}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                // Lista cada veículo
                foreach (var v in veiculos)
                {
                    Console.WriteLine($"- {v}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}