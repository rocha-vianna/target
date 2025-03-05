using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Collections.Generic;

// Nome do arquivo JSON
const string nomeArquivo = "dados.json";

try
{
    // Verifica se o arquivo existe
    if (!File.Exists(nomeArquivo))
    {
        Console.WriteLine($"Erro: O arquivo {nomeArquivo} não foi encontrado.");
        return;
    }

    // Lê todo o conteúdo do arquivo
    string jsonString = File.ReadAllText(nomeArquivo);

    // Configura as opções do JsonSerializer
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new DecimalJsonConverter() }
    };

    // Desserializa o JSON para uma lista de Faturamento
    List<Faturamento> faturamentoArray = JsonSerializer.Deserialize<List<Faturamento>>(jsonString, options);
    Console.WriteLine($"Número de registros lidos: {faturamentoArray.Count}");

    // Filtra os dias com faturamento maior que zero
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

    // Calcula e exibe o menor faturamento
    decimal menorFaturamento = faturamentoDiario.Min();
    Console.WriteLine($"Menor faturamento: {FormatarMoeda(menorFaturamento)}");

    // Calcula e exibe o maior faturamento
    decimal maiorFaturamento = faturamentoDiario.Max();
    Console.WriteLine($"Maior faturamento: {FormatarMoeda(maiorFaturamento)}");

    // Calcula a média mensal (ignorando dias sem faturamento)
    decimal mediaMensal = faturamentoDiario.Average();
    Console.WriteLine($"Média mensal: {FormatarMoeda(mediaMensal)}");

    // Conta e exibe os dias com faturamento acima da média
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

// Método auxiliar para formatar valores monetários
string FormatarMoeda(decimal valor)
{
    return valor.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));
}

// Classe para armazenar os dados de faturamento de cada dia
public class Faturamento
{
    public int Dia { get; set; }
    public decimal Valor { get; set; }
}

// Conversor personalizado para decimal durante a desserialização do JSON
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
