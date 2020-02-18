

using System;

namespace AutomatoExpressoesMatematicas
{
    class NumerosLib
    {
        public (bool, string) lerNumeroDaString(string Expressao, int posicao)
        {
            int contadorPontoDecimal = 0;
            int tamanho = Expressao.Length;
            string numero = "";
            string auxiliar = "";
            
            // Lê os caracateres da Expressão até encontrar um que não seja parte do número que está sendo lido
            while(posicao < tamanho)
            {
                auxiliar = isANumber(Expressao[posicao]);
                if(auxiliar == "")
                {
                   break;
                }

                if(Expressao[posicao] == ',' || Expressao[posicao] == '.')
                {
                    contadorPontoDecimal++;
                    if(contadorPontoDecimal > 1)
                    {
                        return(true, numero);
                    }
                }
                
                posicao++;
                numero += auxiliar;
            }

            return (false, numero);
        }

        private string isANumber(char caractere)
        {            
            switch (caractere)
            {
                case ',':
                    return ".";
                
                case '.':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return caractere.ToString();
            }

            return "";
        }

        public bool isThisCharANumber(char caractere)
        {            
            switch (caractere)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return true;
            }

            return false;
        }


        public (bool, double) converterParaNumero(string stringNumero)
        {
            double numero = 0;

            try
            {
                numero = Convert.ToDouble(stringNumero);
            }
            catch
            {
                return (true, 0);
            }
            
            return (false, numero);
        }


    }

}