using System;
using System.Collections.Generic;

// Dicionário para armazenar os valores de faturamento por estado
var faturamentoPorEstado = new Dictionary<string, double>
{
    {"SP", 67836.43},
    {"RJ", 36678.66},
    {"MG", 29229.88},
    {"ES", 27165.48},
    {"Outros", 19849.53}
};

// Calcula o faturamento total
double faturamentoTotal = 0;
foreach (var valor in faturamentoPorEstado.Values)
{
    faturamentoTotal += valor;
}

Console.WriteLine("Percentual de representação por estado:");

// Calcula e exibe o percentual para cada estado
foreach (var estado in faturamentoPorEstado)
{
    double percentual = (estado.Value / faturamentoTotal) * 100;
    Console.WriteLine($"{estado.Key}: {percentual:F2}%");
}

// Função para formatar o valor como moeda (não utilizada no código acima, mas disponível se necessário)
static string FormatarComoMoeda(double valor)
{
    return valor.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"));
}
