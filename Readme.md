
#Autômato Expressões Matemáticas

Este programa tem o objetivo de Calcular Expressões Matemáticas.
Dada uma Expressão, o programa a validará e realizará o cálculo da mesma.

OBS.: Por padrão, o Visual Studio Code utiliza o terminal integrado.
Portanto, é necessário configurá-lo para utilizar o terminal do sistema.
Para isto é necessário alterar o arquivo launch.json.

Basta substituir a linha:
            "console": "internalConsole",

Pela linha:
            "console": "externalTerminal",

Para mais detalhes, acesse o manual:

https://github.com/OmniSharp/omnisharp-vscode/blob/master/debugger-launchjson.md#console-terminal-window


==========================================================================================================================================

Simulação de Execução do código.


Cada Andar (posição) da Pilha é uma lista que contém todas as operções pendentes antes da abertura de um parêntese (ou parênteses aninhados).
Ou seja, se tivermos nenhum parêntese, todas as operações serão colocadas nem uma lista no andar (posição) 0 (Zero) da pilha.
Se tivermos a abertura de 1 parêntese, todos os elementos dentro do par de parênteses ficarão no andar (posição) 1 da pilha. E assim que este par de parêntese for resolvido, o resultado do cálculo será colocado no andar zero  e a posição 1 será excluída da pilha. Assim, caso um parêntese seja aberto novamente {Ex: 1+(2+3)+4+(5+6) } o andar 1 está livre para armazenar as informações deste novo par de parêntese.
Se tivermos a abertura de parênteses aninhados, cada novo par de parênteses aberto ocupará um andar na pilha.

OBS.: Vejam o exemplo abaixo em um editor de texto (VSCode, Bloco de Notas etc.), pois no Bitbucket ele fica desconfigurado.


Exemplo:
----------------

Expressão --> 1+2x3x(4x5+6x7x8+9x(0+1x2)+3+4x5)+6+7x8


Andar 0
 1+2x3x(4x5+6x7x8+9x(0+1x2)+3+4x5)+6+7x8
C1 2x3x(4x5+6x7x8+9x(0+1x2)+3+4x5)+6+7x8
C1 6x  (4x5+6x7x8+9x(0+1x2)+3+4x5)+6+7x8
C1 C2  (4x5+6x7x8+9x(0+1x2)+3+4x5)+6+7x8


Uma abertura de parêntese foi encontrada. Logo C1 e C2 ficam armazenadas no Andar 0 da pilha e as operações subsequentes serão armazenadas no Andar 1.

Andar 0   Andar 1
C1 C2  |  (4x5+6x7x8+9x(0+1x2)+3+4x5)+6+7x8
C1 C2  |  (20+ 6x7x8+9x(0+1x2)+3+4x5)+6+7x8
C1 C2  |  (C3  6x7x8+9x(0+1x2)+3+4x5)+6+7x8
C1 C2  |  (C3  42x8 +9x(0+1x2)+3+4x5)+6+7x8
C1 C2  |  (C3  336+  9x(0+1x2)+3+4x5)+6+7x8
C1 C2  |  (C3  C4   9x (0+1x2)+3+4x5)+6+7x8
C1 C2  |  (C3  C4   C5 (0+1x2)+3+4x5)+6+7x8

Uma abertura de parêntese foi encontrada. Logo C3, C4 e C2 ficam armazenadas no Andar 1 da pilha e as operações subsequentes serão armazenadas no Andar 2.

Andar 0   Andar 1       Andar 2
C1 C2  |  (C3 C4 C5  |  (0+ 1x2)+3+4x5)+6+7x8
C1 C2  |  (C3 C4 C5  |  (C6 1x2)+3+4x5)+6+7x8
C1 C2  |  (C3 C4 C5  |  (C6   2)+3+4x5)+6+7x8

Ao fechar o parêntese, resolve-se todo mundo que está pendente neste Andar (posição) da pilha

Andar 0   Andar 1       Andar 2
C1 C2  |  (C3 C4 C5  |  (C6 2)+3+4x5)+6+7x8
C1 C2  |  (C3 C4 C5  |  (0+ 2)+3+4x5)+6+7x8
C1 C2  |  (C3 C4 C5  |  (2)   +3+4x5)+6+7x8

Com o Andar (posição) atual resolvido, elemina-se este Andar (posição) da pilha e continua-se lendo a expressão
Andar 2 = Apagado.

Andar 0   Andar 1
C1 C2  |  (C3 C4 C5 2 +3+4x5)+6+7x8

Com a resposta do cálculo do Andar 2, verificamos se existe uma Operação de Alta Prioridade aguardando neste andar para ser executada com o resultado do Andar 2. Caso haja, executa o calculo e após executar o calculo, continua-se lendo a expressão. Caso não haja, simplesmente continua-se lendo a expressão.

Andar 0   Andar 1
C1 C2  |  (C3 C4 9x 2 +3+4x5)+6+7x8
C1 C2  |  (C3 C4 18    +3+4x5)+6+7x8
C1 C2  |  (C3 C4 21+      4x5)+6+7x8
C1 C2  |  (C3 C4 C6 4x5)     +6+7x8
C1 C2  |  (C3 C4 C6 20)      +6+7x8

Ao fechar o parêntese, resolve-se todo mundo que está pendente neste Andar (posição) da pilha

Andar 0   Andar 1
            20+336+18+3+20
C1 C2  |  (C3 C4 C6 20) +6+7x8
C1 C2  |  (C3 C4 21+ 20)  +6+7x8
C1 C2  |  (C3 C4 41)    +6+7x8
C1 C2  |  (C3 336+ 41)  +6+7x8
C1 C2  |  (C3 377)      +6+7x8
C1 C2  |  (20+ 377      +6+7x8
C1 C2  |  (397)         +6+7x8


Com o Andar (posição) atual resolvido, elemina-se este Andar (posição) da pilha e continua-se lendo a expressão
Andar 1 = Apagado.

Andar 0
C1 C2 397 +6+7x8

Com a resposta do cálculo do Andar 1, verificamos se existe uma Operação de Alta Prioridade aguardando neste andar para ser executada com o resultado do Andar 1. Caso haja, executa o calculo e após executar o calculo, continua-se lendo a expressão. Caso não haja, simplesmente continua-se lendo a expressão.

C1 C2 397 +6+7x8
C1 6x 397 +6+7x8
C1 2382   +6+7x8
C1 2382+6   +7x8
C1 2388+     7x8
C1 C3 7x8
C1 C3 56
C1 2388+ 56
C1 2444
1+ 2444
2445









