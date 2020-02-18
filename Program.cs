using System;
using System.Collections;

namespace AutomatoExpressoesMatematicas
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variáveis
            int indice              = 0;
            int tamanhoDaExpressao  = 0;
            int contadorPeranteses  = 0;

            bool erro       = false;
            int codigoErro  = ConstantesDeErros.SEM_ERRO;

            char Operacao     = ' ';

            string DesejaContinuar = "s";
            string Expressao    = "";            
            string stringNumero = "";

        

            // Objetos
            Numero numero = new Numero();
            Operacoes operacoes = new Operacoes();
            NumerosLib numerosLib = new NumerosLib();
            ErroExpressao erroExpressao = new ErroExpressao();

            
            
            // É necessário primeiro se calcular as Multiplicações e Divisões para só então se calcular as Somas e Subtrações.
            Stack pilhaOperacoesPendentes = new Stack();

            // Toda vez que um Parêntese é Aberto, o cálculo precisa ser interrompido para se resolver o cálculo de dentro do Parêntese.
            Stack pilhaOperacoesPendentesAndar = new Stack();


            while(DesejaContinuar == "s")
            {
                // Lê a Expressão Digitada pelo Usuário
                Console.WriteLine("\n Digite uma Expressão:\n");
                Console.Write(" ");
                Expressao = Console.ReadLine();
                tamanhoDaExpressao = Expressao.Length;


                // Loop que Analisa todos os Caracteres da Expressão
                for(indice = 0; (indice < tamanhoDaExpressao) && !erro; indice++)
                {
                    // Verifica se o caractere lido é um número
                    if(numerosLib.isThisCharANumber(Expressao[indice]))
                    {
                        double auxiliar;
                        double numero_2;

                        // Verifica se a variável operação está vazia
                        if(Operacao == ' ')
                        {
                            // Extrai o Número da Expressão e Verifica se é um Número Válido
                            (erro, stringNumero) = numerosLib.lerNumeroDaString(Expressao, indice);                                                
                            if(erro)
                            {
                                erro = true;
                                erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_NUMERO_INVALIDO, indice, Expressao);
                                break;
                            }

                            // Tenta Converter o Número da forma de String para Double
                            (erro, auxiliar) = numerosLib.converterParaNumero(stringNumero);                                               
                            if(erro)
                            {
                                erro = true;
                                erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_NUMERO_INVALIDO, indice, Expressao);
                                break;
                            }

                            // Armazena o resultado do Cálculo
                            numero.setNumero(auxiliar);
                            indice += (stringNumero.Length - 1);
                        }
                        else
                        {
                            // Extrai o Número da Expressão e Verifica se é um Número Válido
                            (erro, stringNumero) = numerosLib.lerNumeroDaString(Expressao, indice);                                                
                            if(erro)
                            {
                                erro = true;
                                erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_NUMERO_INVALIDO, indice, Expressao);
                                break;
                            }

                            // Tenta Converter o Número da forma de String para Double
                            (erro, numero_2) = numerosLib.converterParaNumero(stringNumero);                                               
                            if(erro)
                            {
                                erro = true;
                                erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_NUMERO_INVALIDO, indice, Expressao);
                                break;
                            }


                            // Neste ponto do código nós temos um número "Numero_1", uma operação "Operation" e acabamos de validar
                            // um numero "Numero_2". Caso a Operação seja de Baixa Prioridade ( + ou -) e após o "Numero_2" exista
                            // uma operação de Alta Prioridade ( * , / ), significa que devemos calcular a Multiplicação / Divisão
                            // primeiro, ou seja, o numero "Numero_1" e a operação "Operation" precisam aguardar.
                            // Logo, ambos serão colocados na Pilha e aguardarão até que eles possam ser processados.
                            
                            // Verifica se o Operador atual é de uma Operção de Baixa Prioridade (+ , -)
                            if(!operacoes.isThisAPriorityOperator(Operacao))
                            {
                                // Verifica se este NÃO é o Último Número da Expressão Matemática
                                if((indice + stringNumero.Length) < Expressao.Length)
                                {
                                    // Verifica se depois deste Número (Numero_2) existe uma Operção                                
                                    if(operacoes.isThisCharAOperator(Expressao[indice + stringNumero.Length]))
                                    {
                                        // Verifica se depois deste Número (Numero_2) existe uma Operção de Alta Prioridade (x, *, /)
                                        if(operacoes.isThisAPriorityOperator(Expressao[indice + stringNumero.Length]))
                                        {
                                            OperacoesPendentes operacoesPendentes = new OperacoesPendentes();
                                
                                            operacoesPendentes.setOperacaoPendente(numero.getNumero, Operacao);

                                            pilhaOperacoesPendentes.Push(operacoesPendentes);

                                            // Como o primeiro número (Numero_1) foi para a pilha,
                                            // o último número lido (Numero_2) deve assumir a posição do primeiro.
                                            indice += (stringNumero.Length - 1);
                                            numero.setNumero(numero_2);

                                            // Limpa a Variável Operação, pois a Operação já foi realizada ou armazenada na pilha
                                            Operacao = ' ';

                                            continue;
                                        }
                                    }
                                }
                            }

                            // Tenta Realizar o cálculo entre os dois números.
                            // ATENÇÃO !!! A ordem dos números é importante !!!
                            (codigoErro, auxiliar) = operacoes.calculaOperacao(numero.getNumero, Operacao, numero_2);                        
                            if(codigoErro != ConstantesDeErros.SEM_ERRO)
                            {
                                erro = true;
                                erroExpressao.exibirMensagemDeErro(codigoErro, indice, Expressao);
                                break;
                            }

                            // Armazena o resultado do Cálculo
                            indice += (stringNumero.Length - 1);
                            numero.setNumero(auxiliar);

                            // Limpa a Variável Operação, pois a Operação já foi realizada ou armazenada na pilha
                            Operacao = ' ';
                        }
                    }

                    // Se Não, Verifica se o caractere é uma operação
                    else if(operacoes.isThisCharAOperator(Expressao[indice]))
                    { 
                        // Verifica se o primeiro caractere da Expressão é válido
                        if(indice == 0)
                        {
                            if(Expressao[0] == '*' || Expressao[0] == '/' || Expressao[0] == 'x' || Expressao[1] == '(')
                            {
                                erro = true;
                                erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_OPERADOR_EM_POSICAO_INVALIDA, indice, Expressao);
                                break;
                            }
                        }
                        // Verifica se existe outro Operador antes do atual, por exemplo "+/"
                        else if(operacoes.isThisCharAOperator(Expressao[indice - 1]))
                        {
                            erro = true;
                            erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_OPERADOR_EM_POSICAO_INVALIDA, indice, Expressao);
                            break;
                        }
                        // Caso o caractere anterior seja um Abre Parênteses, os únicos operadores permitidos são "-" e "+"                  
                        else if(
                                Expressao[indice - 1] == '(' 
                                &&
                                (Expressao[indice] == '*' || Expressao[indice] == '/' || Expressao[indice] == 'x')
                                )
                        {
                            erro = true;
                            erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_OPERADOR_EM_POSICAO_INVALIDA, indice, Expressao);
                            break;
                        }
                        // Verifica se este operador é o Último caractere da Expressão Matemática
                        else if(indice >= (Expressao.Length - 1))
                        {
                            erro = true;
                            erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_OPERADOR_EM_POSICAO_INVALIDA, indice, Expressao);
                            break;
                        }

                        Operacao = Expressao[indice];
                    }

                    // Se Não, Verifica se o caractere é um abre parênteses
                    else if(Expressao[indice] == '(')
                    {
                        // Uma Abertura de Parênteses só pode ser precidido por:
                        //    1) Outra Abertura de Parênteses;
                        //    2) Uma Operação;.
                        // O IF abaixo é equivalente a seguinte anhinhamento de IFs:
                        // if(indice > 0)
                        //   if(Expressao[indice - 1] != '(')
                        //      if(!operacoes.isThisCharAOperator(Expressao[indice - 1]))
                        if( 
                            indice > 0
                            &&
                            Expressao[indice - 1] != '('
                            &&
                            !operacoes.isThisCharAOperator(Expressao[indice - 1])
                            )
                        {
                            erro = true;
                            erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_OPERADOR_AUSENTE, indice, Expressao);
                            break;
                        }
                        // Ignora o Par de Parênteses Vazio
                        else if((indice + 1) < tamanhoDaExpressao)
                        {
                            if(Expressao[indice + 1] == ')')
                            {
                                continue;
                            }
                        }

                        contadorPeranteses++;

                        
                        
                        // Colocar Resultado na Pilha
                        if(!numero.estaVazio)
                        {                            
                            OperacoesPendentes operacoesPendentes = new OperacoesPendentes();

                            operacoesPendentes.setOperacaoPendente(numero.getNumero, Operacao);
                            pilhaOperacoesPendentes.Push(operacoesPendentes);

                            // Limpa Variáveis
                            Operacao = ' ';
                            numero.resetarNumero();

                            // Colocar a Pilha na Pilha =D
                            // Fechar o Andar Atual da Pilha de Operações Pendentes e abre um novo.
                            pilhaOperacoesPendentesAndar.Push(pilhaOperacoesPendentes.Clone());                            
                            // Aqui é necessário se chamar o método "Clone" pois, caso contrário, o
                            // "Push" receberá um ponteiro da pilhaOperacoesPendentes, ou seja, ao se
                            // resetar a pilha, logo abaixo, o conteúdo de pilhaOperacoesPendentesAndar
                            // também será apagado.

                            // Reseta a Pilha
                            pilhaOperacoesPendentes.Clear();
                        }
                    }

                    // Se Não, Verifica se o caractere é um abre parênteses
                    else if(Expressao[indice] == ')')
                    {
                        double numero_1;
                        double numero_2;
                        double resultado;


                        contadorPeranteses --;
                        if(contadorPeranteses < 0)
                        {
                            erro = true;
                            erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_PARENTESES_FECHADOS, indice, Expressao);
                            break;
                        }
                        else if(operacoes.isThisCharAOperator(Expressao[indice - 1]))
                        {
                            erro = true;
                            erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_OPERADOR_EM_POSICAO_INVALIDA, indice - 1, Expressao);
                            break;

                        }

                        // Caso haja o fechamento de um parênteses, todas as operacoes que estão no Andar atual devem
                        // ser processadas. Para que assim, se possa fechar este andar.
                        
                        // Verifica se existem Elementos na Pilha. Caso tenha, retira o Elemento do Topo e o processa.
                        if(pilhaOperacoesPendentes.Count > 0)
                        {
                            OperacoesPendentes operacoesPendentes = new OperacoesPendentes();                            

                            while(pilhaOperacoesPendentes.Count > 0)
                            {
                                //Extrai o Elemento do Topo da Pilha
                                operacoesPendentes = (OperacoesPendentes)pilhaOperacoesPendentes.Pop();

                                // Após retirar o Elemento do Topo da Pilha, realiza a Operação Pendente
                                numero_1 = operacoesPendentes.getNumero;
                                Operacao = operacoesPendentes.getOperacao;
                                numero_2 = numero.getNumero;

                                // Tenta Realizar o cálculo entre os dois números.
                                (codigoErro, resultado) = operacoes.calculaOperacao(numero_1, Operacao, numero_2);                       
                                if(codigoErro != ConstantesDeErros.SEM_ERRO)
                                {
                                    erro = true;
                                    erroExpressao.exibirMensagemDeErro(codigoErro, indice, Expressao);
                                    break;
                                }                                

                                // Coloca o Resultado da Operação Pendente na variável Numero para que o cálculo da Expressão possa continuar
                                numero.setNumero(resultado);

                                // Limpa a Variável Operação, pois a Operação já foi realizada
                                Operacao = ' ';
                            }                        
                        }

                        // Verifica se existem mais Andares abaixo na Pilha.
                        if(pilhaOperacoesPendentesAndar.Count > 0)
                        {
                            OperacoesPendentes operacoesPendentes = new OperacoesPendentes();

                            // Existindo, carrega o Andar imediatamente Inferior.
                            pilhaOperacoesPendentes = (Stack)pilhaOperacoesPendentesAndar.Pop();

                            // Verifica se a Última Operação Pendente Armazenada neste Andar Inferior é de Alta Prioridade.
                            operacoesPendentes = (OperacoesPendentes)pilhaOperacoesPendentes.Peek();

                            if(operacoes.isThisAPriorityOperator(operacoesPendentes.getOperacao))
                            {
                                // Caso seja, Realiza o Cálculo.
                                operacoesPendentes = (OperacoesPendentes)pilhaOperacoesPendentes.Pop();
                                // Apesar de operacoesPendentes já conter as informações corretas devido ao "Peek",
                                // é necessário se realizar o "Pop" para se retirar este elemento da pilha.

                                // Após retirar o Elemento do Topo da Pilha, realiza a Operação Pendente.
                                numero_1 = operacoesPendentes.getNumero;
                                Operacao = operacoesPendentes.getOperacao;
                                numero_2 = numero.getNumero;
                                

                                // Tenta Realizar o cálculo entre os dois números.
                                // ATENÇÃO !!! A ordem dos números é importante !!!
                                (codigoErro, resultado) = operacoes.calculaOperacao(numero_1, Operacao, numero_2);
                                if(codigoErro != ConstantesDeErros.SEM_ERRO)
                                {
                                    erro = true;
                                    erroExpressao.exibirMensagemDeErro(codigoErro, indice, Expressao);
                                    break;
                                }

                                // Armazena o resultado do Cálculo.
                                numero.setNumero(resultado);

                                // Limpa a Variável Operação, pois a Operação já foi realizada ou armazenada na pilha.
                                Operacao = ' ';

                            }
                        }
                    }
                    else
                    {
                        erro = true;
                        erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_CARACTERE_INVALIDO, indice, Expressao);
                        break;
                    }
                }

                // Este contador só é diferente de Zero quando existem números diferentes de Abertura de Parênteses e Fechamento de Parênteses
                if(contadorPeranteses != 0 && !erro)
                {
                    erro = true;

                    Expressao += " ";
                    erroExpressao.exibirMensagemDeErro(ConstantesDeErros.ERRO_PARENTESES_ABERTOS, Expressao.Length - 1, Expressao);
                }

                // Verificase se a pilha deste Andar está vazia, caso não esteja, esvazia a mesma, realizando todo o cálculo pendente.
                if(pilhaOperacoesPendentes.Count > 0)
                {
                    double resultado;

                    (codigoErro, resultado) = operacoes.calcularTodaAPilha(pilhaOperacoesPendentes, numero.getNumero);

                    if(codigoErro != ConstantesDeErros.SEM_ERRO)
                    {
                        erro = true;
                        erroExpressao.exibirMensagemDeErro(codigoErro, indice, Expressao);
                        break;
                    }

                    // Armazena o resultado do Cálculo.
                    numero.setNumero(resultado);
                }


                // Verifica se existem mais Andares abaixo na Pilha.
                if(pilhaOperacoesPendentesAndar.Count > 0)
                {
                    double resultado = 0;

                    while(pilhaOperacoesPendentesAndar.Count > 0)
                    { 
                        pilhaOperacoesPendentes = (Stack)pilhaOperacoesPendentesAndar.Pop();

                        (codigoErro, resultado) = operacoes.calcularTodaAPilha(pilhaOperacoesPendentes, numero.getNumero);

                        if(codigoErro != ConstantesDeErros.SEM_ERRO)
                        {
                            erro = true;
                            erroExpressao.exibirMensagemDeErro(codigoErro, indice, Expressao);
                            break;
                        }
                    }

                    // Armazena o resultado do Cálculo.
                    numero.setNumero(resultado);
                }

                // Caso nenhum erro seja detectado, exibe o resulta do cálculo.
                if(!erro)
                {
                    // Exibe a Resposta da Expressão na Tela
                    Console.WriteLine("\n\n Resultado : {0}", numero.getNumero);
                }


                //Verifica se o Usuário deseja entrar com outra Expressão
                Console.Write("\n\n\n Você deseja digitar outra Expressão ? (s/n) ");
                DesejaContinuar = Console.ReadLine().ToLower();

                while(DesejaContinuar != "s" && DesejaContinuar != "n")
                {
                    Console.Clear();
                    Console.WriteLine("\n Sua ANTA !\n Responda APENAS com \"s\" ou \"n\" !!! \n Eu perguntarei novamente e desta vez dê uma resposta válida !");
                    Console.Write("\n\n\n Sua BESTA, você deseja digitar outra Expressão ? (s/n) ");
                    DesejaContinuar = Console.ReadLine().ToLower();
                }
                
                // Limpa a Tela
                Console.Clear();

                // Reinicializa todas as Variáveis
                numero.resetarNumero();
                contadorPeranteses  = 0;
                erro                = false;
                codigoErro          = ConstantesDeErros.SEM_ERRO;
                Operacao            = ' ';
                Expressao           = "";

            }
        }
    }
}
