

using System;

namespace AutomatoExpressoesMatematicas
{
    class ErroExpressao
    {
        public void exibirMensagemDeErro(int erro, int posicao, string Expressao)
        {
            int indice = 0;
            int tamanho = Expressao.Length;

            // Exibe Mensagem de Erro na Tela
            switch(erro)
            {                
                case ConstantesDeErros.ERRO_PARENTESES_ABERTOS:
                    Console.WriteLine("\n\n Existem mais Parênteses Abertos do que Fechados :\n");
                break;
                
                case ConstantesDeErros.ERRO_PARENTESES_FECHADOS:
                    Console.WriteLine("\n\n Um \"Fechar Parênteses\" foi inserido em uma posição inválida :\n");
                break;
                
                case ConstantesDeErros.ERRO_OPERADOR_EM_POSICAO_INVALIDA:
                    Console.WriteLine("\n\n Um dos Operadores da Expressão está em uma posição Inválida :\n");
                break;

                case ConstantesDeErros.ERRO_OPERADOR_AUSENTE:
                    Console.WriteLine("\n\n Está faltando um Operador neste posição da Expressão :\n");
                break;

                case ConstantesDeErros.ERRO_OPERADOR_INVALIDO:
                    Console.WriteLine("\n\n O Operador é Inválido :\n");
                break;

                case ConstantesDeErros.ERRO_NUMERO_INVALIDO:
                    Console.WriteLine("\n\n Um dos Números da Expressão é Inválido :\n");
                break;
                
                case ConstantesDeErros.ERRO_CARACTERE_INVALIDO:
                    Console.WriteLine("\n\n Um dos Caracteres da Expressão é Inválido :\n");
                break;
                
                case ConstantesDeErros.ERRO_DIVISAO_POR_ZERO:
                    Console.WriteLine("\n\n Não é possível Dividir por Zero :\n");
                break;

                default:
                    Console.WriteLine("\n\n Erro Desconhecido \n");
                break;
            }

            

            Console.Write(" ");
            // Altera Cores de Fundo e da Fonte
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            
            // Exibe a Expressão na Tela e indica a Posição onde se detectou o Erro
            for(indice = 0; indice < tamanho; indice ++)
            {
                // Verifica se a posição atual é onde se detectou o erro
                if(indice == posicao)
                {
                    // Altera as Cores de Fundo e da Fonte
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;

                    // Escreve Caractere na Tela
                    Console.Write("{0}", Expressao[indice]);

                    // Altera as Cores de Fundo e da Fonte
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;

                    continue;
                }
                
                Console.Write("{0}", Expressao[indice]);
            }

            // Altera as Cores de Fundo e da Fonte
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

        }

    }
}