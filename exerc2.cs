using System;

Console.Write("Informe um número para verificar se pertence à sequência de Fibonacci: ");
int numero = int.Parse(Console.ReadLine());

bool pertence = VerificarFibonacci(numero);

if (pertence)
{
    Console.WriteLine($"O número {numero} pertence à sequência de Fibonacci.");
}
else
{
    Console.WriteLine($"O número {numero} não pertence à sequência de Fibonacci.");
}

static bool VerificarFibonacci(int numero)
{
    if (numero < 0)
    {
        return false;
    }

    int anterior = 0;
    int atual = 1;

    while (atual <= numero)
    {
        if (atual == numero)
        {
            return true;
        }

        int proximo = anterior + atual;
        anterior = atual;
        atual = proximo;
    }

    return false;
}
