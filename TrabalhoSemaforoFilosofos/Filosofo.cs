using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrabalhoSemaforoFilosofos
{
    class Filosofo
    {
        public string nome { get; }
        public int posicao { get; }
        private int vezes_comeu;
        private int vezes_pensou;
        private int tentativas_comer;

        public Filosofo(string nome, int posicao)
        {
            this.nome = nome;
            this.posicao = posicao;
            this.vezes_comeu = 0;
            this.vezes_pensou = 0;
            this.tentativas_comer = 0;
        }

        public void comeu()
        {
            this.vezes_comeu++;
        }

        public void tentouComer()
        {
            this.tentativas_comer++;
        }

        public void pensou()
        {
            this.vezes_pensou++;
        }

        public int getVezesComeu()
        {
            return this.vezes_comeu;
        }

        public int getTentativasComer()
        {
            return this.tentativas_comer;
        }
    }
}
