using System;
using System.Collections.Generic;
using System.Text;

namespace Ivory.TesteEstagio.CampoMinado
{
    class Jogadas
    {
        // variavel para armazenar o tabuleiro em forma de matriz depois de tratado 
        private string[,] tabuleiro = new string[9, 9];

        // metodo para tratar o tabuleiro que vem em formato de string e transformar em matriz
        public void SepararTabuleiro(CampoMinado entrada)
        {
            string[] linhas = entrada.Tabuleiro.Split("\n");

            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    tabuleiro[x, y] = linhas[x].Substring(y, 1);
                }
            }
        }

        public int Jogada(CampoMinado entrada)
        {
            SepararTabuleiro(entrada);


            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    if (tabuleiro[x, y].Equals("-"))
                    {
                        if (JogadaEncimaDosZeros(x, y))
                        {
                            //como os indices da matriz não batem com a jogada real que se deseja fazer é preciso acresentar.
                            entrada.Abrir(x + 1, y + 1);
                        }
                    }
                }
            }


            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    if (tabuleiro[x, y].Equals("-"))
                    {
                        if (SuporPossivelBomba(x, y))
                        {
                            // apos feita a verificação com as informações presentes no tabueiro é marcado
                            // onde possivelmente tem uma bomba, sem abrir a casa em que questão.
                            tabuleiro[x, y] = "*";
                        }
                    }
                }
            }

            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    if ((tabuleiro[x, y].Equals("-")))
                    {
                        if (VerificarAberturaDeCasa(x, y))
                        {
                            entrada.Abrir(x + 1, y + 1);
                            tabuleiro[x, y] = "A";
                        }
                    }
                }
            }

            Console.WriteLine("==============\n\n\n\n");
            VerificarStatus(entrada.JogoStatus);
            Console.WriteLine(entrada.Tabuleiro);

            return entrada.JogoStatus;
        }

        public void VerificarStatus(int valor)
        {
            if (valor == 0) Console.WriteLine("Jogo em aberto, continue procurando as casas que não possuem bombas.");
            else if (valor == 1) Console.WriteLine("Vitoria, você encontrou todas as casas que não possui bomba.");
            else Console.WriteLine("GameOver, você encontrou uma bomba.");
            Console.WriteLine("==========");

        }


        public bool JogadaEncimaDosZeros(int x, int y)
        {
            // casa ao redor da que se deseja abrir
            bool acimaEsquerda = false, acima = false, acimaDireita = false;
            bool esquerda = false, direita = false;
            bool abaixoEsquerda = false, abaixo = false, abaixoDireita = false;

            bool resposta = false;

            // acima na esquerda
            if (x - 1 > -1 && y - 1 > -1)
            {
                if (tabuleiro[x - 1, y - 1].Equals("0"))
                {
                    acimaEsquerda = true;
                }
            }
            else acimaEsquerda = true;


            // acima
            if (x - 1 > -1)
            {
                if (tabuleiro[x - 1, y].Equals("0"))
                {
                    acima = true;
                }
            }
            else acima = true;


            // acima na direita
            if (x - 1 > -1 && y + 1 < 9)
            {
                if (tabuleiro[x - 1, y + 1].Equals("0"))
                {
                    acimaDireita = true;
                }

            }
            else acimaDireita = true;


            // esquerda
            if (y - 1 > -1)
            {
                if (tabuleiro[x, y - 1].Equals("0"))
                {
                    esquerda = true;
                }
            }
            else esquerda = true;


            // direita
            if (y + 1 < 9)
            {
                if (tabuleiro[x, y + 1].Equals("0"))
                {
                    direita = true;
                }
            }
            else direita = true;


            // abaixo na esquerda
            if (x + 1 < 9 && y - 1 > -1)
            {
                if (tabuleiro[x + 1, y - 1].Equals("0"))
                {
                    abaixoEsquerda = true;
                }
            }
            else abaixoEsquerda = true;


            // abaixo
            if (x + 1 < 9)
            {
                if (tabuleiro[x + 1, y].Equals("0"))
                {
                    abaixo = true;
                }
            }
            else abaixo = true;


            // abaixo na direita
            if (x + 1 < 9 && y + 1 < 9)
            {
                if (tabuleiro[x + 1, y + 1].Equals("0"))
                {
                    abaixoDireita = true;
                }
            }
            else abaixoDireita = true;


            if (acimaEsquerda && acima && acimaDireita && esquerda && direita && abaixoEsquerda && abaixo && abaixoDireita)
            {
                resposta = true;
            }
            return resposta;
        }


        // esse metodo vai verificar se alguma das casa ao redor confirmam
        // que a casa em questão é uma possivel bomba e então gravar uma marcação
        // ele pega a cada uma das casa ao redor e 
        public bool SuporPossivelBomba(int x, int y)
        {
            // casa ao redor da que se deseja olhar
            bool acimaEsquerda = false, acima = false, acimaDireita = false;
            bool esquerda = false, direita = false;
            bool abaixoEsquerda = false, abaixo = false, abaixoDireita = false;

            bool resposta = false;

            // acima na esquerda
            if (((x - 1) > -1) && ((y - 1) > -1))
            {
                if (ConfirmarBomba((x - 1), (y - 1)))
                {
                    //tabuleiro[x, y] = "*";
                    acimaEsquerda = true;
                }
            }
            else acimaEsquerda = false;

            // acima
            if (x - 1 > -1)
            {
                if (ConfirmarBomba((x - 1), y))
                {
                    //tabuleiro[x, y] = "*";
                    acima = true;
                }
            }
            else acima = false;


            // acima na direita
            if (x - 1 > -1 && y + 1 < 9)
            {
                if (ConfirmarBomba(x - 1, y + 1))
                {
                    //tabuleiro[x, y] = "*";
                    abaixoDireita = true;
                }
            }
            else acimaDireita = false;

            // esquerda
            if (y - 1 > -1)
            {
                if (ConfirmarBomba(x, y - 1))
                {
                    //tabuleiro[x, y] = "*";
                    esquerda = true;
                }
            }
            else esquerda = false;

            // direita
            if (y + 1 < 9)
            {
                if (ConfirmarBomba(x, y + 1))
                {
                    //tabuleiro[x, y] = "*";
                    direita = true;
                }
            }
            else direita = false;

            // abaixo na esquerda
            if (x + 1 < 9 && y - 1 > -1)
            {
                if (ConfirmarBomba(x + 1, y - 1))
                {
                    tabuleiro[x, y] = "*";
                    abaixoEsquerda = true;
                }
            }
            else abaixoEsquerda = false;

            // abaixo
            if (x + 1 < 9)
            {
                if (ConfirmarBomba(x + 1, y))
                {
                    //tabuleiro[x, y] = "*";
                    abaixo = true;
                }
            }
            else abaixo = false;

            // abaixo na direita
            if (x + 1 < 9 && y + 1 < 9)
            {
                if (ConfirmarBomba(x + 1, y + 1))
                {
                    //tabuleiro[x, y] = "*";
                    abaixoDireita = true;
                }
            }
            else abaixoDireita = false;

            if (acimaEsquerda || acima || acimaDireita || esquerda || direita || abaixoEsquerda || abaixo || abaixoDireita)
            {
                resposta = true;
            }

            return resposta;
        }

        public bool ConfirmarBomba(int x, int y)
        {
            bool resposta = false;

            int contador = 0;
            int qtdBombas = -1;


            if ((!tabuleiro[x, y].Equals("-")) && (!tabuleiro[x, y].Equals("*")) && (!tabuleiro[x, y].Equals("A")))
            {
                qtdBombas = int.Parse(tabuleiro[x, y]);
            }


            // acima na esquerda
            if (((x - 1) > -1) && ((y - 1) > -1))
            {
                if ((tabuleiro[(x - 1), (y - 1)].Equals("-")) || (tabuleiro[(x - 1), (y - 1)].Equals("*")))
                {
                    contador++;
                }
            }


            // acima
            if ((x - 1) > -1)
            {
                if ((tabuleiro[(x - 1), y].Equals("-")) || (tabuleiro[(x - 1), y].Equals("*")))
                {
                    contador++;
                }
            }


            // acima na direita
            if (((x - 1) > -1) && ((y + 1) < 9))
            {
                if ((tabuleiro[(x - 1), (y + 1)].Equals("-")) || (tabuleiro[(x - 1), (y + 1)].Equals("*")))
                {
                    contador++;
                }
            }

            // esquerda
            if ((y - 1) > -1)
            {
                if ((tabuleiro[x, (y - 1)].Equals("-")) || (tabuleiro[x, (y - 1)].Equals("*")))
                {
                    contador++;
                }
            }


            // direita
            if ((y + 1) < 9)
            {
                if ((tabuleiro[x, (y + 1)].Equals("-")) || (tabuleiro[x, (y + 1)].Equals("*")))
                {
                    contador++;
                }
            }

            // abaixo na esquerda
            if (((x + 1) < 9) && ((y - 1) > -1))
            {
                if ((tabuleiro[(x + 1), (y - 1)].Equals("-")) || (tabuleiro[(x + 1), (y - 1)].Equals("*")))
                {
                    contador++;
                }
            }

            // abaixo
            if ((x + 1) < 9)
            {
                if ((tabuleiro[(x + 1), y].Equals("-")) || (tabuleiro[(x + 1), y].Equals("*")))
                {
                    contador++;
                }
            }

            // abaixo na direita
            if (((x + 1) < 9) && ((y + 1) < 9))
            {
                if ((tabuleiro[(x + 1), (y + 1)].Equals("-")) || (tabuleiro[(x + 1), (y + 1)].Equals("*")))
                {
                    contador++;
                }
            }

            if (contador == qtdBombas)
            {
                resposta = true;
            }

            return resposta;
        }

        public bool VerificarAberturaDeCasa(int x, int y)
        {
            // casa ao redor da que se deseja verificar antes de abrir
            bool acimaEsquerda = false, acima = false, acimaDireita = false;
            bool esquerda = false, direita = false;
            bool abaixoEsquerda = false, abaixo = false, abaixoDireita = false;

            bool resposta = false;

            // acima na esquerda
            if (((x - 1) > -1) && ((y - 1) > -1))
            {
                if (ConfirmarAberturaDeCasa((x - 1), (y - 1)))
                {
                    acimaEsquerda = true;
                }
            }


            // acima
            if ((x - 1) > -1)
            {
                if (ConfirmarAberturaDeCasa((x - 1), y))
                {
                    acima = true;
                }
            }


            // acima na direita
            if (((x - 1) > -1) && ((y + 1) < 9))
            {
                if (ConfirmarAberturaDeCasa((x - 1), (y + 1)))
                {
                    acimaDireita = true;
                }
            }


            // esquerda
            if ((y - 1) > -1)
            {
                if (ConfirmarAberturaDeCasa(x, (y - 1)))
                {
                    esquerda = true;
                }
            }


            // direita
            if ((y + 1) < 9)
            {
                if (ConfirmarAberturaDeCasa(x, (y + 1)))
                {
                    direita = true;
                }
            }


            // abaixo na esquerda
            if (((x + 1) < 9) && ((y - 1) > -1))
            {
                if (ConfirmarAberturaDeCasa((x + 1), (y - 1)))
                {
                    abaixoEsquerda = true;
                }
            }


            // abaixo
            if ((x + 1) < 9)
            {
                if (ConfirmarAberturaDeCasa((x + 1), y))
                {
                    abaixo = true;
                }
            }


            //abaixo na direita
            if (((x + 1) < 9) && ((y + 1) < 9))
            {
                if (ConfirmarAberturaDeCasa((x + 1), (y + 1)))
                {
                    abaixoDireita = true;
                }
            }

            if (acimaEsquerda || acima || acimaDireita || esquerda || direita || abaixoEsquerda || abaixo || abaixoDireita)
            {
                resposta = true;
            }

            return resposta;
        }

        public bool ConfirmarAberturaDeCasa(int x, int y)
        {
            bool resposta = false;

            int contador = 0;
            int qtdBombas = -1;


            if ((!tabuleiro[x, y].Equals("-")) && (!tabuleiro[x, y].Equals("*")) && (!tabuleiro[x, y].Equals("A")))
            {
                qtdBombas = int.Parse(tabuleiro[x, y]);
            }


            // acima na esquerda
            if (((x - 1) > -1) && ((y - 1) > -1))
            {
                if (tabuleiro[(x - 1), (y - 1)].Equals("*"))
                {
                    contador++;
                }
            }


            // acima
            if ((x - 1) > -1)
            {
                if (tabuleiro[(x - 1), y].Equals("*"))
                {
                    contador++;
                }
            }


            // acima na direita
            if (((x - 1) > -1) && ((y + 1) < 9))
            {
                if (tabuleiro[(x - 1), (y + 1)].Equals("*"))
                {
                    contador++;
                }
            }


            // esquerda
            if ((y - 1) > -1)
            {
                if (tabuleiro[x, (y - 1)].Equals("*"))
                {
                    contador++;
                }
            }


            // direita
            if ((y + 1) < 9)
            {
                if (tabuleiro[x, (y + 1)].Equals("*"))
                {
                    contador++;
                }
            }


            // abaixo na esquerda
            if (((x + 1) < 9) && ((y - 1) > -1))
            {
                if (tabuleiro[(x + 1), (y - 1)].Equals("*"))
                {
                    contador++;
                }
            }


            // abaixo
            if ((x + 1) < 9)
            {
                if (tabuleiro[(x + 1), y].Equals("*"))
                {
                    contador++;
                }
            }


            // abaixo na direita
            if (((x + 1) < 9) && ((y + 1) < 9))
            {
                if (tabuleiro[(x + 1), (y + 1)].Equals("*"))
                {
                    contador++;
                }
            }


            if (contador == qtdBombas)
            {
                resposta = true;
            }


            return resposta;
        }
    }
}
