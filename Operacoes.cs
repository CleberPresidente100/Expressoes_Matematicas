
using System.Collections;

namespace AutomatoExpressoesMatematicas
{
    class Operacoes
    {
        public bool isThisCharAOperator (char caractere)
        {
            switch (caractere)
            {
                case '+':
                case '-':
                case '*':
                case 'x':
                case '/':
                    return true;

                default:
                    return false;
            }

        }

        public (int, double) calculaOperacao (double numero_1, char operacao, double numero_2)
        {
            double resultado = 0;
            
            int erro = ConstantesDeErros.SEM_ERRO;

            switch(operacao)
            {
                case '+':
                    resultado = numero_1 + numero_2;
                break;
                
                case '-':
                    resultado = numero_1 - numero_2;
                break;
                
                case '*':
                case 'x':
                    resultado = numero_1 * numero_2;
                break;
                
                case '/':
                    if(numero_2 == 0)
                    {
                        erro = ConstantesDeErros.ERRO_DIVISAO_POR_ZERO;
                        break;
                    }
                    resultado = numero_1 / numero_2;

                break;

                default:
                    erro = ConstantesDeErros.ERRO_OPERADOR_INVALIDO;
                break;
            }

            return (erro, resultado);
        }

        public bool isThisAPriorityOperator (char operacao)
        {
            switch (operacao)
            {
                case 'x':
                case '*':
                case '/':
                    return true;
            }

            return false;
        }    
    
        public (int, double) calcularTodaAPilha(Stack pilhaOperacoesPendentes, double numero)
        {
            int codigoErro = ConstantesDeErros.SEM_ERRO;
            char operacao;

            double numero_1;
            double numero_2;
            double resultado = numero;

            OperacoesPendentes operacoesPendentes = new OperacoesPendentes();

            while(pilhaOperacoesPendentes.Count > 0)
            {
                operacoesPendentes = (OperacoesPendentes)pilhaOperacoesPendentes.Pop();

                // Após retirar o Elemento do Topo da Pilha, realiza a Operação Pendente.
                numero_1 = operacoesPendentes.getNumero;
                operacao = operacoesPendentes.getOperacao;
                numero_2 = resultado;

                // Tenta Realizar o cálculo entre os dois números.
                // ATENÇÃO !!! A ordem dos números é importante !!!
                (codigoErro, resultado) = this.calculaOperacao(numero_1, operacao, numero_2);
                if(codigoErro != ConstantesDeErros.SEM_ERRO)
                {
                    return (codigoErro, 0);
                }
            }

            return (codigoErro, resultado);
        }
    }
}