namespace HotelDevInn.Services;

// Services/CsvService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Serviço genérico de baixo nível para exportação
public class CsvService
{
    public void Exportar<T>(string caminho, List<T> itens, Func<string> cabecalhoFunc, Func<T, string> linhaFunc)
    {
        var sb = new StringBuilder();
        sb.AppendLine(cabecalhoFunc());
        foreach (var item in itens)
        {
            sb.AppendLine(linhaFunc(item));
        }
        File.WriteAllText(caminho, sb.ToString());
    }
}