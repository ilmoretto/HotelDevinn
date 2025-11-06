using HotelDevInn.Models;
using HotelDevInn.Services;
using HotelDevInn.Views;

namespace HotelDevInn.Controllers;

// Controllers/HotelController.cs
using System;

public class HotelController
{
    // O Controller conhece os Serviços e a View
    private readonly ReservaService _reservaService;
    private readonly HospedeService _hospedeService;
    private readonly QuartoService _quartoService;
    private readonly HotelView _view;

    // Recebe todos eles via construtor
    public HotelController(ReservaService reservaService, HospedeService hospedeService, QuartoService quartoService, HotelView view)
    {
        _reservaService = reservaService;
        _hospedeService = hospedeService;
        _quartoService = quartoService;
        _view = view;
    }

    // --- Métodos de Orquestração ---
    // Note como eles são simples: pegam dados do serviço e entregam para a view.

    public void ListarTodasAsReservas()
    {
        _view.ExibirTitulo("Listagem de Todas as Reservas");
        var reservas = _reservaService.ListarTodas();
        _view.ExibirLista(reservas);
    }

    public void MostrarCustoDeReserva(Reserva reserva)
    {
        _view.ExibirTitulo($"Cálculo de Custo da Reserva ID {reserva.Id}");
        var custo = _reservaService.CalcularCustoReserva(reserva);
        _view.ExibirCusto(reserva, custo);
    }

    public void BuscarReservasPorHospede(string nome)
    {
        _view.ExibirTitulo($"Busca de Reservas por Hóspede: '{nome}'");
        var reservas = _reservaService.EncontrarPorNomeHospede(nome);
        _view.ExibirLista(reservas);
    }

    public void MostrarQuartosDisponiveis(DateTime checkIn, DateTime checkOut)
    {
        _view.ExibirTitulo($"Quartos Disponíveis entre {checkIn:dd/MM/yyyy} e {checkOut:dd/MM/yyyy}");
        var quartos = _reservaService.ListarQuartosDisponiveis(checkIn, checkOut);
        _view.ExibirLista(quartos);
    }

    public void ExecutarExportacao()
    {
        _view.ExibirTitulo("Exportação de Dados para CSV");
        try
        {
            _hospedeService.ExportarParaCsv("hospedes.csv");
            _view.ExibirMensagem("Arquivo hospedes.csv exportado com sucesso!");
            _quartoService.ExportarParaCsv("quartos.csv");
            _view.ExibirMensagem("Arquivo quartos.csv exportado com sucesso!");
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Ocorreu um erro ao exportar: {ex.Message}");
        }
    }
}