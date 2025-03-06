using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Collections.Generic;

const string nomeArquivo = "dados.json";

try
{
    if (!File.Exists(nomeArquivo))
    {
        Console.WriteLine($"Erro: O arquivo {nomeArquivo} não foi encontrado.");
        return;
    }

    string jsonString = File.ReadAllText(nomeArquivo);

    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new DecimalJsonConverter() }
    };

    List<Faturamento> faturamentoArray = JsonSerializer.Deserialize<List<Faturamento>>(jsonString, options);
    Console.WriteLine($"Número de registros lidos: {faturamentoArray.Count}");

    var faturamentoDiario = faturamentoArray
        .Where(f => f.Valor > 0)
        .Select(f => f.Valor)
        .ToList();

    Console.WriteLine($"Número de dias com faturamento válido: {faturamentoDiario.Count}");
    if (faturamentoDiario.Count == 0)
    {
        Console.WriteLine("Não há dados de faturamento válidos (maiores que zero).");
        return;
    }

    decimal menorFaturamento = faturamentoDiario.Min();
    Console.WriteLine($"Menor faturamento: {FormatarMoeda(menorFaturamento)}");

    decimal maiorFaturamento = faturamentoDiario.Max();
    Console.WriteLine($"Maior faturamento: {FormatarMoeda(maiorFaturamento)}");

    decimal mediaMensal = faturamentoDiario.Average();
    Console.WriteLine($"Média mensal: {FormatarMoeda(mediaMensal)}");

    int diasAcimaDaMedia = faturamentoDiario.Count(valor => valor > mediaMensal);
    Console.WriteLine($"Número de dias com faturamento acima da média: {diasAcimaDaMedia}");
}
catch (JsonException ex)
{
    Console.WriteLine($"Erro ao deserializar o JSON: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
}

string FormatarMoeda(decimal valor)
{
    return valor.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));
}

public class Faturamento
{
    public int Dia { get; set; }
    public decimal Valor { get; set; }
}

public class DecimalJsonConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (decimal.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
            {
                return value;
            }
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetDecimal();
        }

        throw new JsonException("Valor inválido para o tipo decimal.");
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}
