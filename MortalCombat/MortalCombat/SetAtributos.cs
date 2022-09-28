using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortalCombat
{
    internal class SetAtributos
    {
        public float vidaBase { get; private set; }
        public float curaBase { get; private set; }
        public float danoBase { get; private set; }
        public int chanceCriticoBase { get; private set; }
        public float danoCriticoBase { get; private set; }

        public float incrementoVida { get; private set; }
        public float incrementoCura { get; private set; }
        public float incrementoDano { get; private set; }
        public int incrementoChanceCritico { get; private set; }
        public float incrementoDanoCritico { get; private set; }

        private float vidaAdicional;
        private float curaAdicional;
        private float danoAdicional;
        private int chanceCriticoAdicional;
        private float danoCriticoAdicional;

        public int pontos { get; private set; }
        private int[] upgrades = new int[5];

        public SetAtributos()
        {
            Resetar();
            incrementoVida = 220f;
            incrementoCura = 50f;
            incrementoDano = 0.05f;
            incrementoChanceCritico = 11;
            incrementoDanoCritico = 0.2f;
        }

        public void Resetar()
        {
            vidaBase = 1000f;
            curaBase = 72.5f;
            danoBase = 1f;
            chanceCriticoBase = 20;
            danoCriticoBase = 1.75f;

            vidaAdicional = 0f;
            curaAdicional = 0f;
            danoAdicional = 0f;
            chanceCriticoAdicional = 0;
            danoCriticoAdicional = 0f;

            pontos = 12;
            for(int idx = 0; idx < upgrades.Length; idx++)
            {
                upgrades[idx] = 0;
            }
        }

        private void Atualizar()
        {
            vidaAdicional = incrementoVida * upgrades[0];
            curaAdicional = incrementoCura * upgrades[1];
            danoAdicional = incrementoDano * upgrades[2];
            chanceCriticoAdicional = incrementoChanceCritico * upgrades[3];
            danoCriticoAdicional = incrementoDanoCritico * upgrades[4];
        }

        private void Upar(byte indice)
        {
            do
            {
                Console.Clear();
                Miscelanea.PrintColor("amarelo", $"\tPontos: {pontos}\n");
                Console.Write("\tAtributo: ");
                switch (indice)
                {
                    case 0:
                        Miscelanea.PrintColor("verde", "vida: ");
                        Console.Write($"{(vidaBase + vidaAdicional).ToString("F2", CultureInfo.InvariantCulture)}");
                        break;
                    case 1:
                        Miscelanea.PrintColor("verde", "cura: ");
                        Console.Write($"{(curaBase + curaAdicional).ToString("F2", CultureInfo.InvariantCulture)}");
                        break;
                    case 2:
                        Miscelanea.PrintColor("vermelho", "dano: ");
                        Console.Write($"{(danoBase + danoAdicional).ToString("F2", CultureInfo.InvariantCulture)}");
                        break;
                    case 3:
                        Miscelanea.PrintColor("amarelo", "chance de crítico: ");
                        Console.Write($"{(chanceCriticoBase + chanceCriticoAdicional).ToString()} %");
                        break;
                    case 4:
                        Miscelanea.PrintColor("vermelho", "dano crítico: ");
                        Console.Write($"{((danoCriticoBase + danoCriticoAdicional) * 100).ToString("F2", CultureInfo.InvariantCulture)} %");
                        break;
                }
                Miscelanea.PrintColor("amarelo", (upgrades[indice] == 5) ? " (Max.)\n" : "\n" );

                Console.WriteLine("\t[0] - Para voltar");
                
                Console.Write("\t[1] - ");
                Miscelanea.PrintColor("vermelho", "Diminuir Status <");
                Console.Write("|");
                Miscelanea.PrintColor("verde", "> Aumentar Status");
                Console.WriteLine(" - [2]");
                
                byte op;
                do
                {
                    Console.Write(" -> ");
                    op = (byte)Console.ReadLine().Trim()[0];

                    if (op == 48 || op == 49 || op == 50)
                    {
                        op -= 48;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(" - Valor inválido!");
                    }
                } while (true);

                if(op == 0)
                {
                    break;
                }
                else if(op == 1 && upgrades[indice] >= 1)
                {
                    pontos++;
                    upgrades[indice]--;
                }
                else if(op == 2 && upgrades[indice] <= 4 && pontos > 0)
                {
                    pontos--;
                    upgrades[indice]++;
                }
                Atualizar();
            } while (true);
        }

        public void MenuUpgrade(string nomeJogador)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("\to-----------------------------o");
                Miscelanea.PrintColor("ciano", $"\t{nomeJogador}, escolha um atributo para upar:\n");
                Console.WriteLine($"\t  Pontos: {pontos}");

                Console.Write("{0}", (upgrades[0] > 0) ? "\t+ " : "\t");
                Miscelanea.PrintColor("verde", "[1] - Vida: ");
                Console.WriteLine($"{(vidaBase + vidaAdicional).ToString("F2", CultureInfo.InvariantCulture)}");

                Console.Write("{0}", (upgrades[1] > 0) ? "\t+ " : "\t");
                Miscelanea.PrintColor("verde", "[2] - Cura: ");
                Console.WriteLine($"{(curaBase + curaAdicional).ToString("F2", CultureInfo.InvariantCulture)}");

                Console.Write("{0}", (upgrades[2] > 0) ? "\t+ " : "\t");
                Miscelanea.PrintColor("vermelho", "[3] - Dano: ");
                Console.WriteLine($"{(danoBase + danoAdicional).ToString("F2", CultureInfo.InvariantCulture)}");

                Console.Write("{0}", (upgrades[3] > 0) ? "\t+ " : "\t");
                Miscelanea.PrintColor("amarelo", "[4] - Chance de crítico: ");
                Console.WriteLine($"{(chanceCriticoBase + chanceCriticoAdicional).ToString()} %");

                Console.Write("{0}", (upgrades[4] > 0) ? "\t+ " : "\t");
                Miscelanea.PrintColor("vermelho", "[5] - Dano crítico: ");
                Console.WriteLine($"{((danoCriticoBase + danoCriticoAdicional) * 100).ToString("F2", CultureInfo.InvariantCulture)} %");
                
                Console.WriteLine("\to-----------------------------o");
                Console.WriteLine("\t[0] - Para confirmar os upgrades.\n");

                byte op;
                do
                {
                    Console.Write(" -> ");
                    op = (byte)Console.ReadLine()[0];

                    if (op >= 48 && op <= 53)
                    {
                        op -= 48;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(" - Valor inválido!");
                    }
                } while (true);

                if(op == 0)
                {
                    break;
                }

                Upar((byte)(op - 1));
            } while (true);

            vidaBase += vidaAdicional;
            curaBase += curaAdicional;
            danoBase += danoAdicional;
            chanceCriticoBase += chanceCriticoAdicional;
            danoCriticoBase += danoCriticoAdicional;
        }
    }
}
