using HotelDevInn.Models;
using HotelDevInn.Repositories;

namespace HotelDevInn.Services;

// Services/QuartoService.cs
using System.Globalization; // Para CultureInfo.InvariantCulture

public class QuartoService
{
    private readonly GenericRepository<Quarto> _repository;
    private readonly CsvService _csvService;

    public QuartoService(GenericRepository<Quarto> repository, CsvService csvService)
    {
        _repository = repository;
        _csvService = csvService;
    }

    public void ExportarParaCsv(string caminho)
    {
        var quartos = _repository.GetAll();
        _csvService.Exportar(caminho, quartos,
            () => "Id;Numero;Tipo;PrecoPorNoite",
            q => $"{q.Id};{q.Numero};{q.Tipo};{q.PrecoPorNoite.ToString(CultureInfo.InvariantCulture)}");
    }
}