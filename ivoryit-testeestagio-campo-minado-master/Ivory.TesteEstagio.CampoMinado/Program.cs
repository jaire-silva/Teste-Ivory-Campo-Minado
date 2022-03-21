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
            var escolha = new Jogada();


            do
            {
                Console.WriteLine("\nAgurdando a proxima jogada");
                Thread.Sleep(1000);
                statusJogo = escolha.Decisao(campoMinado);

            } while (statusJogo == 0);
        }
    }
}
