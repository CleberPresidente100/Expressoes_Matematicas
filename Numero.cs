

namespace AutomatoExpressoesMatematicas
{
    class Numero
    {
        private bool vazio = true;
        private double valor = 0;

        public bool estaVazio
        {
            get{return vazio;}
        }

        public void setNumero(double numero)
        {
            this.vazio = false;
            this.valor = numero;
        }

        public double getNumero
        {
            get{return this.valor;}
        }

        public void resetarNumero()
        {
            this.vazio = true;
            this.valor = 0;
        }


    }
}
