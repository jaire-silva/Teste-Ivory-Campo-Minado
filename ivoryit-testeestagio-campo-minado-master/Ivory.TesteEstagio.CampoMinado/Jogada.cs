using System;
using System.Collections.Generic;
using System.Text;

namespace Ivory.TesteEstagio.CampoMinado
{
    public class Jogada
    {
        // Variavel para armazenar o tabuleiro em forma de matriz depois de tratado.
        private string[,] tabuleiro = new string[9, 9];


        // Metodo para tratar o tabuleiro que vem em formato de string e transformar em matriz.
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

        public int Decisao(CampoMinado entrada)
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
                            // Como os indices da matriz não batem com a jogada real que se deseja fazer é preciso acresentar o valor 1.
                            entrada.Abrir((x + 1), (y + 1));
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
                        if (PossivelBomba(x, y))
                        {
                            // Apos feita a verificação com as informações presentes no tabueiro é marcado
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

        // Este metodo verifica se todas as casas ao redor da que se deseja abrir são zeros, esta é a jogada obvia.
        public bool JogadaEncimaDosZeros(int x, int y)
        {
            // Casas ao redor da que se deseja abrir.
            var acimaEsquerda = false;
            var acima = false;
            var acimaDireita = false;
            var esquerda = false;
            var direita = false;
            var abaixoEsquerda = false;
            var abaixo = false;
            var abaixoDireita = false;
            var resposta = false;


            // Acima na esquerda.
            if  (((x - 1) > -1) && ((y - 1) > -1))
            {
                if (tabuleiro[(x - 1), (y - 1)].Equals("0"))
                {
                    acimaEsquerda = true;
                }
            }
            else acimaEsquerda = true;


            // Acima.
            if ((x - 1) > -1)
            {
                if (tabuleiro[(x - 1), y].Equals("0"))
                {
                    acima = true;
                }
            }
            else acima = true;


            // Acima na direita.
            if (((x - 1) > -1) && ((y + 1) < 9))
            {
                if (tabuleiro[(x - 1), (y + 1)].Equals("0"))
                {
                    acimaDireita = true;
                }

            }
            else acimaDireita = true;


            // Esquerda.
            if ((y - 1) > -1)
            {
                if (tabuleiro[x, (y - 1)].Equals("0"))
                {
                    esquerda = true;
                }
            }
            else esquerda = true;


            // Direita.
            if ((y + 1) < 9)
            {
                if (tabuleiro[x, (y + 1)].Equals("0"))
                {
                    direita = true;
                }
            }
            else direita = true;


            // Abaixo na esquerda.
            if (((x + 1) < 9) && ((y - 1) > -1))
            {
                if (tabuleiro[(x + 1), (y - 1)].Equals("0"))
                {
                    abaixoEsquerda = true;
                }
            }
            else abaixoEsquerda = true;


            // Abaixo.
            if ((x + 1) < 9)
            {
                if (tabuleiro[(x + 1), y].Equals("0"))
                {
                    abaixo = true;
                }
            }
            else abaixo = true;


            // Abaixo na direita.
            if (((x + 1) < 9) && ((y + 1) < 9))
            {
                if (tabuleiro[(x + 1), (y + 1)].Equals("0"))
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


        // Este metodo verifica se alguma das casas ao redor da selecionada possuem informações de que ela é uma bomba.
        public bool PossivelBomba(int x, int y)
        {
            // Casas ao redor da que se desconfia ser uma bomba.
            var acimaEsquerda = false;
            var acima = false;
            var acimaDireita = false;
            var esquerda = false;
            var direita = false;
            var abaixoEsquerda = false;
            var abaixo = false;
            var abaixoDireita = false;
            var resposta = false;


            // Acima na esquerda.
            if (((x - 1) > -1) && ((y - 1) > -1))
            {
                if (ContaBomba((x - 1), (y - 1)))
                {
                    acimaEsquerda = true;
                }
            }
            else acimaEsquerda = false;

            // Acima.
            if ((x - 1) > -1)
            {
                if (ContaBomba((x - 1), y))
                {
                    acima = true;
                }
            }
            else acima = false;


            // Acima na direita.
            if (((x - 1) > -1) && ((y + 1) < 9))
            {
                if (ContaBomba((x - 1), (y + 1)))
                {
                    abaixoDireita = true;
                }
            }
            else acimaDireita = false;

            // Esquerda.
            if ((y - 1) > -1)
            {
                if (ContaBomba(x, (y - 1)))
                {
                    esquerda = true;
                }
            }
            else esquerda = false;

            // Direita.
            if ((y + 1) < 9)
            {
                if (ContaBomba(x, y + 1))
                {
                    direita = true;
                }
            }
            else direita = false;

            // Abaixo na esquerda.
            if (((x + 1) < 9) && ((y - 1) > -1))
            {
                if (ContaBomba((x + 1), (y - 1)))
                {
                    abaixoEsquerda = true;
                }
            }
            else abaixoEsquerda = false;

            // Abaixo.
            if ((x + 1) < 9)
            {
                if (ContaBomba((x + 1), y))
                {
                    abaixo = true;
                }
            }
            else abaixo = false;

            // Abaixo na direita.
            if (x + 1 < 9 && y + 1 < 9)
            {
                if (ContaBomba((x + 1), (y + 1)))
                {
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

        // Este metodo recebe cada uma das casas que estão ao redor da que se desconfia ser uma bomba, verifica se ja foi aberta,
        // e caso sim verifica se as bombas que ela diz existir ao seu redor ja foram encontradas, se não foram e o numero de casas por abrir
        // for o valor presente nele sinaliza que todas elas são bombas.
        public bool ContaBomba(int x, int y)
        {
            var resposta = false;
            var contador = 0;
            var qtdBombas = -1;

            
            if ((!tabuleiro[x, y].Equals("-")) && (!tabuleiro[x, y].Equals("*")) && (!tabuleiro[x, y].Equals("A")))
            {
                qtdBombas = int.Parse(tabuleiro[x, y]);
            }


            // Acima na esquerda.
            if (((x - 1) > -1) && ((y - 1) > -1))
            {
                if ((tabuleiro[(x - 1), (y - 1)].Equals("-")) || (tabuleiro[(x - 1), (y - 1)].Equals("*")))
                {
                    contador++;
                }
            }


            // Acima.
            if ((x - 1) > -1)
            {
                if ((tabuleiro[(x - 1), y].Equals("-")) || (tabuleiro[(x - 1), y].Equals("*")))
                {
                    contador++;
                }
            }


            // Acima na direita.
            if (((x - 1) > -1) && ((y + 1) < 9))
            {
                if ((tabuleiro[(x - 1), (y + 1)].Equals("-")) || (tabuleiro[(x - 1), (y + 1)].Equals("*")))
                {
                    contador++;
                }
            }


            // Esquerda.
            if ((y - 1) > -1)
            {
                if ((tabuleiro[x, (y - 1)].Equals("-")) || (tabuleiro[x, (y - 1)].Equals("*")))
                {
                    contador++;
                }
            }


            // Direita.
            if ((y + 1) < 9)
            {
                if ((tabuleiro[x, (y + 1)].Equals("-")) || (tabuleiro[x, (y + 1)].Equals("*")))
                {
                    contador++;
                }
            }


            // Abaixo na esquerda.
            if (((x + 1) < 9) && ((y - 1) > -1))
            {
                if ((tabuleiro[(x + 1), (y - 1)].Equals("-")) || (tabuleiro[(x + 1), (y - 1)].Equals("*")))
                {
                    contador++;
                }
            }


            // Abaixo.
            if ((x + 1) < 9)
            {
                if ((tabuleiro[(x + 1), y].Equals("-")) || (tabuleiro[(x + 1), y].Equals("*")))
                {
                    contador++;
                }
            }


            // Abaixo na direita.
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

        // Este metodo verifica se alguma das casas ao redor da selecionada possuem informações de que ela pode ser aberta.
        public bool VerificarAberturaDeCasa(int x, int y)
        {
            // Casas ao redor da que se deseja abrir.
            var acimaEsquerda = false;
            var acima = false;
            var acimaDireita = false;
            var esquerda = false;
            var direita = false;
            var abaixoEsquerda = false;
            var abaixo = false;
            var abaixoDireita = false;
            var resposta = false;


            // Acima na esquerda.
            if (((x - 1) > -1) && ((y - 1) > -1))
            {
                if (ConfirmaAberturaDeCasa((x - 1), (y - 1)))
                {
                    acimaEsquerda = true;
                }
            }


            // Acima.
            if ((x - 1) > -1)
            {
                if (ConfirmaAberturaDeCasa((x - 1), y))
                {
                    acima = true;
                }
            }


            // Acima na direita.
            if (((x - 1) > -1) && ((y + 1) < 9))
            {
                if (ConfirmaAberturaDeCasa((x - 1), (y + 1)))
                {
                    acimaDireita = true;
                }
            }


            // Esquerda.
            if ((y - 1) > -1)
            {
                if (ConfirmaAberturaDeCasa(x, (y - 1)))
                {
                    esquerda = true;
                }
            }


            // Direita.
            if ((y + 1) < 9)
            {
                if (ConfirmaAberturaDeCasa(x, (y + 1)))
                {
                    direita = true;
                }
            }


            // Abaixo na esquerda.
            if (((x + 1) < 9) && ((y - 1) > -1))
            {
                if (ConfirmaAberturaDeCasa((x + 1), (y - 1)))
                {
                    abaixoEsquerda = true;
                }
            }


            // Abaixo.
            if ((x + 1) < 9)
            {
                if (ConfirmaAberturaDeCasa((x + 1), y))
                {
                    abaixo = true;
                }
            }


            // Abaixo na direita.
            if (((x + 1) < 9) && ((y + 1) < 9))
            {
                if (ConfirmaAberturaDeCasa((x + 1), (y + 1)))
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


        // Este metodo recebe cada uma das casas que estão ao redor da que se deseja abrir, verifica se ja foi aberta,
        // e caso sim verifica se as bombas que ela diz existir ao seu redor ja foram encontradas, se sim isso sinaliza
        // que a casa sejada ser aberta não possui bomba.
        public bool ConfirmaAberturaDeCasa(int x, int y)
        {
            var resposta = false;
            var contador = 0;
            var qtdBombas = -1;


            if ((!tabuleiro[x, y].Equals("-")) && (!tabuleiro[x, y].Equals("*")) && (!tabuleiro[x, y].Equals("A")))
            {
                qtdBombas = int.Parse(tabuleiro[x, y]);
            }


            // Acima na esquerda.
            if (((x - 1) > -1) && ((y - 1) > -1))
            {
                if (tabuleiro[(x - 1), (y - 1)].Equals("*"))
                {
                    contador++;
                }
            }


            // Acima.
            if ((x - 1) > -1)
            {
                if (tabuleiro[(x - 1), y].Equals("*"))
                {
                    contador++;
                }
            }


            // Acima na direita.
            if (((x - 1) > -1) && ((y + 1) < 9))
            {
                if (tabuleiro[(x - 1), (y + 1)].Equals("*"))
                {
                    contador++;
                }
            }


            // Esquerda.
            if ((y - 1) > -1)
            {
                if (tabuleiro[x, (y - 1)].Equals("*"))
                {
                    contador++;
                }
            }


            // Direita.
            if ((y + 1) < 9)
            {
                if (tabuleiro[x, (y + 1)].Equals("*"))
                {
                    contador++;
                }
            }


            // Abaixo na esquerda.
            if (((x + 1) < 9) && ((y - 1) > -1))
            {
                if (tabuleiro[(x + 1), (y - 1)].Equals("*"))
                {
                    contador++;
                }
            }


            // Abaixo.
            if ((x + 1) < 9)
            {
                if (tabuleiro[(x + 1), y].Equals("*"))
                {
                    contador++;
                }
            }


            // Abaixo na direita.
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
