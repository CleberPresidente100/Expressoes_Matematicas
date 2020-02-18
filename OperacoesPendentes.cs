

namespace AutomatoExpressoesMatematicas
{
    class OperacoesPendentes
    {
        private bool vazio;
        private double numero;
        private char operacao;

        public void setNumero(double numero)
        {
            this.vazio = false;
            this.numero = numero;
        }

        public double getNumero
        {
            get{return this.numero;}
        }

        public void setOperacao(char operacao)
        {
            this.vazio = false;
            this.operacao = operacao;
        }

        public char getOperacao
        {
            get{return this.operacao;}
        }

        public void resetarOperacoesPendentes()
        {
            this.vazio = true;
            this.operacao = ' ';
            // A variável this.numero não é alterada pois Zero é um valor válido
            // assim como qualquer outro. Ou seja, não existe uma forma de se
            // nulificar a variável this.numero.
        }

        public void setOperacaoPendente(double numero, char operacao)
        {
            this.vazio = false;
            this.numero = numero;
            this.operacao = operacao;
        }

        
    }
}