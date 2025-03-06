using System;
using System.Text;

Console.Write("Digite uma string para ser invertida: ");
string entrada = Console.ReadLine();

string invertida = InverterString(entrada);

Console.WriteLine($"String original: {entrada}");
Console.WriteLine($"String invertida: {invertida}");

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
