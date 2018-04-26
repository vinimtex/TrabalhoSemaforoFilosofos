using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TrabalhoSemaforoFilosofos
{
    public class MainApp
    {
        static int QTD_FILOSOFOS = 5;
        delegate int LEFT(int posicaoAtual);
        static LEFT left = verificarLeft;
        delegate int RIGHT(int posicaoAtual);
        static LEFT right = verificarRight;
        static bool THINKING = false;
        static int LIMITE_FILOSOFOS_COMENDO = 2;
        static Semaphore semaforo = new Semaphore(0, 1);
        static string[] ESTADO = new string[QTD_FILOSOFOS];
        static Semaphore SEMAFORO_RC = new Semaphore(0, 1);
        static Semaphore[] SEMAFORO_FILOSOFO = new Semaphore[5];


       /*public static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("PENSANDO");


            }

        }*/

        public void PegarrGarfos(int posicao)
        {
            SEMAFORO_RC.WaitOne(); // sinaliza aque está entrando na região crítica
            #region Crítica
            ESTADO[posicao] = "FAMINTO"; // SETA QUE O CARA ESTÁ COM FOME
            TentarPegarDoisGarfos(posicao); // TENTA PEGAR DOIS GARFOS
            #endregion
            SEMAFORO_RC.Release(); // DEIXA A REGIÃO CRÍTICA
            SEMAFORO_FILOSOFO[posicao].Release();
        }

        public void TentarPegarDoisGarfos(int posicao)
        {
            if(ESTADO[posicao] == "FAMINTO" && ESTADO[left(posicao)] != "COMENDO" && ESTADO[right(posicao)] != "COMENDO")
            {
                ESTADO[posicao] = "COMENDO";
                SEMAFORO_FILOSOFO[posicao].WaitOne();
            }
        }


        public static int verificarLeft(int posicaoAtual)
        {
            return (posicaoAtual + QTD_FILOSOFOS - 1) % QTD_FILOSOFOS;
        }
        public static int verificarRight(int posicaoAtual)
        {
            return (posicaoAtual + 1) % QTD_FILOSOFOS;
        }
    }
}

