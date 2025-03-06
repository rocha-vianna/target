using System;
using System.Collections.Generic;

var faturamentoPorEstado = new Dictionary<string, double>
{
    {"SP", 67836.43},
    {"RJ", 36678.66},
    {"MG", 29229.88},
    {"ES", 27165.48},
    {"Outros", 19849.53}
};

double faturamentoTotal = 0;
foreach (var valor in faturamentoPorEstado.Values)
{
    faturamentoTotal += valor;
}

Console.WriteLine("Percentual de representação por estado:");

foreach (var estado in faturamentoPorEstado)
{
    double percentual = (estado.Value / faturamentoTotal) * 100;
    Console.WriteLine($"{estado.Key}: {percentual:F2}%");
}

static string FormatarComoMoeda(double valor)
{
    return valor.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"));
}
