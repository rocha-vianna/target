using System;
using System.Text;

// Solicita ao usuário que insira uma string
Console.Write("Digite uma string para ser invertida: ");
string entrada = Console.ReadLine();

// Chama a função para inverter a string
string invertida = InverterString(entrada);

// Exibe o resultado
Console.WriteLine($"String original: {entrada}");
Console.WriteLine($"String invertida: {invertida}");

// Função para inverter a string
static string InverterString(string str)
{
    if (string.IsNullOrEmpty(str))
    {
        return str;
    }

    StringBuilder resultado = new StringBuilder(str.Length);

    for (int i = str.Length - 1; i >= 0; i--)
    {
        resultado.Append(str[i]);
    }

    return resultado.ToString();
}
