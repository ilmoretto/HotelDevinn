// Program.cs
using System;
using System.Collections.Generic;
using System.Globalization;
// Importe todas as suas camadas!
using HotelDevInn.Models;
using HotelDevInn.Repositories;
using HotelDevInn.Services;
using HotelDevInn.Views;
using HotelDevInn.Controllers;

public class Program
{
    public static void Main(string[] args)
    {
        // --- 1. POPULANDO DADOS INICIAIS ---
        // (Nosso "Banco de Dados" em memória)
        var quartos = new List<Quarto>
        {
            new Quarto { Id = 1, Numero = 101, Tipo = "Solteiro", PrecoPorNoite = 150.00m },
            new Quarto { Id = 2, Numero = 102, Tipo = "Casal", PrecoPorNoite = 250.00m },
            new Quarto { Id = 3, Numero = 201, Tipo = "Suíte", PrecoPorNoite = 450.00m },
            new Quarto { Id = 4, Numero = 202, Tipo = "Solteiro", PrecoPorNoite = 160.00m }
        };
        var hospedes = new List<Hospede>
        {
            new Hospede { Id = 1, Nome = "Ana Carolina", CPF = "111.111.111-11" },
            new Hospede { Id = 2, Nome = "Bruno Marques", CPF = "222.222.222-22" },
        };
        var funcionarios = new List<Funcionario>
        {
            new Funcionario { Id = 1, Nome = "Carlos" },
            new Funcionario { Id = 2, Nome = "Mariana" },
        };
        var reservas = new List<Reserva>
        {
            new Reserva { Id = 1, QuartoReservado = quartos[1], Hospede = hospedes[0], Funcionario = funcionarios[0], DataCheckIn = new DateTime(2025, 10, 12), DataCheckOut = new DateTime(2025, 10, 16) }, // Reserva da Ana (Quarto 102)
            new Reserva { Id = 2, QuartoReservado = quartos[2], Hospede = hospedes[1], Funcionario = funcionarios[1], DataCheckIn = new DateTime(2025, 10, 18), DataCheckOut = new DateTime(2025, 10, 21) }, // Reserva do Bruno (Quarto 201)
            new Reserva { Id = 3, QuartoReservado = quartos[0], Hospede = hospedes[1], Funcionario = funcionarios[0], DataCheckIn = new DateTime(2025, 11, 01), DataCheckOut = new DateTime(2025, 11, 05) }  // Outra reserva do Bruno (Quarto 101)
        };

        // --- 2. INJEÇÃO DE DEPENDÊNCIA MANUAL ---
        // (Conectando todas as peças)

        // Repositories (passando os dados)
        var quartoRepo = new GenericRepository<Quarto>(quartos);
        var hospedeRepo = new GenericRepository<Hospede>(hospedes);
        var funcionarioRepo = new GenericRepository<Funcionario>(funcionarios);
        var reservaRepo = new GenericRepository<Reserva>(reservas);

        // Services (passando os repositórios)
        var csvService = new CsvService();
        var hospedeService = new HospedeService(hospedeRepo, csvService);
        var quartoService = new QuartoService(quartoRepo, csvService);
        var reservaService = new ReservaService(reservaRepo, quartoRepo);

        // View & Controller (passando os serviços e a view)
        var hotelView = new HotelView();
        var hotelController = new HotelController(reservaService, hospedeService, quartoService, hotelView);

        // --- 3. EXECUÇÃO DOS REQUISITOS FUNCIONAIS ---
        Console.WriteLine("Bem-vindo ao Sistema de Gestão do Hotel Dev-Inn!");

        // Requisito 1: Listar Todas as Reservas
        hotelController.ListarTodasAsReservas();

        // Requisito 2: Calcular Custo da Reserva
        hotelController.MostrarCustoDeReserva(reservas[0]); // Custo da reserva da Ana

        // Requisito 3: Encontrar Reserva por Hóspede
        hotelController.BuscarReservasPorHospede("Bruno");

        // Requisito 4: Listar Quartos Disponíveis (para um período que conflita com a Ana)
        // A reserva da Ana é de 12/10 a 16/10. Vamos procurar de 14/10 a 17/10.
        // O quarto 102 (Casal) deve sumir da lista.
        hotelController.MostrarQuartosDisponiveis(new DateTime(2025, 10, 14), new DateTime(2025, 10, 17));

        // Requisito 5: Exportar Dados
        hotelController.ExecutarExportacao();

        Console.WriteLine("\nExecução concluída. Pressione qualquer tecla para sair.");
        Console.ReadKey();
    }
}