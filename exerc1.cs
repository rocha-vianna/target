const int INDICE = 13; // O número até onde queremos somar
int soma = 0;          // Inicializa a soma como 0
int k = 0;             // Inicializa o contador K como 0

// Loop enquanto K for menor que INDICE
while (k < INDICE)
{
    k = k + 1;     // Incrementa K
    soma = soma + k; // Adiciona K à soma
}

// Imprime o resultado da soma
Console.WriteLine($"A soma dos números de 1 a {INDICE} é: {soma}");
