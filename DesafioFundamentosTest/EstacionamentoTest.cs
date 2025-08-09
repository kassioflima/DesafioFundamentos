using DesafioFundamentos.Models;

namespace DesafioFundamentosTest
{
    public class EstacionamentoTests
    {
        private Estacionamento CreateEstacionamento(decimal precoInicial = 5, decimal precoPorHora = 2)
        {
            return new Estacionamento(precoInicial, precoPorHora);
        }

        [Fact]
        public void AdicionarVeiculo_ValidPlaca_AddsVeiculo()
        {
            var estacionamento = CreateEstacionamento();
            var input = new StringReader("ABC1234\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            estacionamento.AdicionarVeiculo();

            var result = output.ToString();
            Assert.Contains("Ve�culo ABC1234 estacionado com sucesso.", result);
        }

        [Fact]
        public void AdicionarVeiculo_InvalidPlaca_ShowsError()
        {
            var estacionamento = CreateEstacionamento();
            var input = new StringReader("123\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            estacionamento.AdicionarVeiculo();

            var result = output.ToString();
            Assert.Contains("Placa em formato inv�lido.", result);
        }

        [Fact]
        public void AdicionarVeiculo_EmptyPlaca_ShowsError()
        {
            var estacionamento = CreateEstacionamento();
            var input = new StringReader("\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            estacionamento.AdicionarVeiculo();

            var result = output.ToString();
            Assert.Contains("Placa n�o informada.", result);
        }

        [Fact]
        public void AdicionarVeiculo_DuplicatePlaca_ShowsError()
        {
            var estacionamento = CreateEstacionamento();
            var input = new StringReader("ABC1234\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            estacionamento.AdicionarVeiculo();

            // Try to add the same plate again
            input = new StringReader("ABC1234\n");
            Console.SetIn(input);

            estacionamento.AdicionarVeiculo();

            var result = output.ToString();
            Assert.Contains("Esse ve�culo j� est� estacionado.", result);
        }

        [Fact]
        public void RemoverVeiculo_ValidPlaca_RemovesVeiculoAndCalculatesPrice()
        {
            var estacionamento = CreateEstacionamento();

            // Add vehicle first
            var inputAdicionar = new StringReader("ABC1234\n");
            var outputAdicionar = new StringWriter();
            Console.SetIn(inputAdicionar);
            Console.SetOut(outputAdicionar);
            estacionamento.AdicionarVeiculo();

            // Remove vehicle: provide both plate and hours in sequence
            var inputRemover = new StringReader("ABC1234\n2\n");
            var outputRemover = new StringWriter();
            Console.SetIn(inputRemover);
            Console.SetOut(outputRemover);

            estacionamento.RemoverVeiculo();

            var result = outputRemover.ToString();
            var testes = result.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Assert.Contains("O ve�culo ABC1234 foi removido e o pre�o total foi de: R$ 9,00", testes[2]);
        }


        [Fact]
        public void RemoverVeiculo_InvalidPlaca_ShowsError()
        {
            var estacionamento = CreateEstacionamento();
            var input = new StringReader("XYZ9876\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            estacionamento.RemoverVeiculo();

            var result = output.ToString();
            Assert.Contains("Desculpe, esse ve�culo n�o est� estacionado aqui", result);
        }

        [Fact]
        public void RemoverVeiculo_InvalidHoras_ShowsError()
        {
            var estacionamento = CreateEstacionamento();
            // Add vehicle first
            var input = new StringReader("ABC1234\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);
            estacionamento.AdicionarVeiculo();

            // Remove vehicle with invalid hours
            input = new StringReader("ABC1234\nabc\n");
            Console.SetIn(input);
            output = new StringWriter();
            Console.SetOut(output);

            estacionamento.RemoverVeiculo();

            var result = output.ToString();
            Assert.Contains("Quantidade de horas inv�lida.", result);
        }

        [Fact]
        public void ListarVeiculos_WithVehicles_ListsAll()
        {
            var estacionamento = CreateEstacionamento();
            // Add vehicles
            var input = new StringReader("ABC1234\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);
            estacionamento.AdicionarVeiculo();

            input = new StringReader("XYZ9876\n");
            Console.SetIn(input);
            estacionamento.AdicionarVeiculo();

            output = new StringWriter();
            Console.SetOut(output);

            estacionamento.ListarVeiculos();

            var result = output.ToString();
            Assert.Contains("Os ve�culos estacionados s�o:", result);
            Assert.Contains("- ABC1234", result);
            Assert.Contains("- XYZ9876", result);
        }

        [Fact]
        public void ListarVeiculos_NoVehicles_ShowsEmptyMessage()
        {
            var estacionamento = CreateEstacionamento();
            var output = new StringWriter();
            Console.SetOut(output);

            estacionamento.ListarVeiculos();

            var result = output.ToString();
            Assert.Contains("N�o h� ve�culos estacionados.", result);
        }
    }
}