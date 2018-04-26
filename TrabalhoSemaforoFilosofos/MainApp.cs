using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TrabalhoSemaforoFilosofos
{
    class Application
    {

        static int qtd_filosofos = 50;
        static int tempoExecucao = 180000; //3 minutos
        static string[] objs;
        //static Semaphore sema = new Semaphore(0,1);
        static Semaphore[] hashis = new Semaphore[qtd_filosofos];
        static Filosofo[] filosofos = new Filosofo[qtd_filosofos];


        public static void Main(string[] args)
        {
            Console.WriteLine("===== QUANTIDADE FILÓSOFOS: " + qtd_filosofos);
            Console.WriteLine("===== TEMPO DE EXECUÇÃO: 3 minutos");
            for (int i = 0; i < qtd_filosofos; i++)
            {
                hashis[i] = new Semaphore(1, 1);
                filosofos[i] = new Filosofo("Galileu " + i, i);
                Thread t = new Thread(new ParameterizedThreadStart(filosofoTask));
                t.Start(i);

                new Timer((e) => //CORRIGIR TIMER
                {
                    Console.WriteLine("Filosófo " + filosofos[i - 1].nome + " saiu da janta"); //TODO: VERIFICAR PQ ESTÁ PEGANDO SEMPRE O ÚLTIMO INDICE DO LOOP
                    t.Abort();                     
                }, null, tempoExecucao, 0);

            }

            Console.ReadLine();
        }

        public static void filosofoTask(object posicao)
        {
            int pos = (int)posicao;
            int posicaoHashiDireita = (pos + qtd_filosofos - 1) % qtd_filosofos;
            int posicaoHashiEsquerda = (pos + 1) % qtd_filosofos;
            
            do
            {
                Console.WriteLine("Filósofo " + filosofos[pos].nome + " está pensando...");
                Thread.Sleep(5000);
                Console.WriteLine("Filósofo " + filosofos[pos].nome + " irá tentar comer...");
                filosofos[pos].pensou();
                try
                {
                    
                    if(hashis[posicaoHashiDireita].WaitOne() &&
                       hashis[posicaoHashiEsquerda].WaitOne())
                    {
                        Console.WriteLine("Filósofo " + filosofos[pos].nome + " está comendo...");
                    } else
                    {
                        filosofos[pos].tentouComer();
                        Console.WriteLine("Filósofo " + filosofos[pos].nome + " mas não conseguiu pegar os dois hashis");
                        Thread.Sleep(new Random().Next(3000));
                    }
                    
                    
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    filosofos[pos].comeu();
                    hashis[posicaoHashiDireita].Release();
                    hashis[posicaoHashiEsquerda].Release();
                }

            } while (true);
            
        }

    }
}
