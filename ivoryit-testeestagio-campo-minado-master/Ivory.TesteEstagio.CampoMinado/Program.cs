using System;
using System.Threading;

namespace Ivory.TesteEstagio.CampoMinado
{
    class Program
    {
        static void Main(string[] args)
        {
            var campoMinado = new CampoMinado();
            Console.WriteLine("Início do jogo\n=========");
            Console.WriteLine(campoMinado.Tabuleiro);

            // Realize sua codificação a partir deste ponto, boa sorte!

            int statusJogo;

            Jogadas jogada = new Jogadas();




            do
            {
                Console.WriteLine("\nAgurdando a proxima jogada");

                // gerar uma pausa entra cada cada jogada
                Thread.Sleep(1000);

                statusJogo = jogada.Jogada(campoMinado);

            } while (statusJogo == 0);

        }
    }
}
