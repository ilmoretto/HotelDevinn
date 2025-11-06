using HotelDevInn.Models;
using HotelDevInn.Repositories;

namespace HotelDevInn.Services;

// Services/HospedeService.cs
public class HospedeService
{
    private readonly GenericRepository<Hospede> _repository;
    private readonly CsvService _csvService;

    // Recebemos as dependências (Repos e outro Serviço) no construtor
    public HospedeService(GenericRepository<Hospede> repository, CsvService csvService)
    {
        _repository = repository;
        _csvService = csvService;
    }

    public void ExportarParaCsv(string caminho)
    {
        var hospedes = _repository.GetAll();
        _csvService.Exportar(caminho, hospedes,
            () => "Id;Nome;CPF", // Lambda para o cabeçalho
            h => $"{h.Id};{h.Nome};{h.CPF}"); // Lambda para a linha
    }
}