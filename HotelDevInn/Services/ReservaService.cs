using HotelDevInn.Models;
using HotelDevInn.Repositories;

namespace HotelDevInn.Services;

// Services/ReservaService.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class ReservaService
{
    // Note que este serviço precisa de DOIS repositórios!
    private readonly GenericRepository<Reserva> _reservaRepository;
    private readonly GenericRepository<Quarto> _quartoRepository;

    public ReservaService(GenericRepository<Reserva> reservaRepo, GenericRepository<Quarto> quartoRepo)
    {
        _reservaRepository = reservaRepo;
        _quartoRepository = quartoRepo;
    }

    public List<Reserva> ListarTodas() => _reservaRepository.GetAll();

    public List<Reserva> EncontrarPorNomeHospede(string nome)
    {
        // Usamos o Find() do repositório com uma Lambda
        return _reservaRepository.Find(r => r.Hospede.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
    }

    // *** ISTO É UMA REGRA DE NEGÓCIO ***
    public decimal CalcularCustoReserva(Reserva reserva)
    {
        var numeroNoites = (reserva.DataCheckOut - reserva.DataCheckIn).Days;
        // Regra: Mínimo de 1 diária
        return numeroNoites > 0 ? numeroNoites * reserva.QuartoReservado.PrecoPorNoite : reserva.QuartoReservado.PrecoPorNoite;
    }

    // *** ESTA É A REGRA DE NEGÓCIO MAIS COMPLEXA ***
    public List<Quarto> ListarQuartosDisponiveis(DateTime checkIn, DateTime checkOut)
    {
        var todasReservas = _reservaRepository.GetAll();
        var todosQuartos = _quartoRepository.GetAll();

        // 1. Encontra IDs de quartos cujas reservas conflitam com o período desejado
        var quartosOcupadosIds = todasReservas
            // A lógica de conflito de datas:
            .Where(r => (checkIn < r.DataCheckOut) && (r.DataCheckIn < checkOut))
            .Select(r => r.QuartoReservado.Id)
            .ToHashSet(); // ToHashSet() é ótimo para performance em buscas

        // 2. Retorna os quartos cujo ID NÃO ESTÁ (!) na lista de ocupados
        return todosQuartos.Where(q => !quartosOcupadosIds.Contains(q.Id)).ToList();
    }
}