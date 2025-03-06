const int INDICE = 13; 
int soma = 0;          
int k = 0;             

while (k < INDICE)
{
    k = k + 1;
    soma = soma + k; 
}

Console.WriteLine($"A soma dos números de 1 a {INDICE} é: {soma}");
