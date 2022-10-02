using System;
using System.Collections.Generic;
using System.Globalization;
using MortalCombat.Enums;

namespace MortalCombat.Entities
{
    internal class Miscelanea
    {
        public static void PrintColor(string cor, params string[] texto1)
        {
            switch (cor.ToLower())
            {
                case "amarelo":
                    Console.ForegroundColor = ConsoleColor.Yellow; // Destaca um aviso;
                    break;
                case "verde":
                    Console.ForegroundColor = ConsoleColor.Green; // Destaca aumento de status;
                    break;
                case "vermelho":
                    Console.ForegroundColor = ConsoleColor.Red; // Destaca subtração de status;
                    break;
                case "ciano":
                    Console.ForegroundColor = ConsoleColor.Cyan; // Destaca um nome;
                    break;
                case "azul":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "roxo":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            for (int idx = 0; idx < texto1.Length; idx++)
            {
                Console.Write(texto1[idx]);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintDano(Personagem jogador, float dano)
        {
            PrintColor("ciano", "\t" + jogador.Nome + ":");
            Console.Write(" causou ");
            PrintColor("vermelho", dano.ToString("F1", CultureInfo.InvariantCulture));
            Console.Write(" de dano em ");
            PrintColor("ciano", jogador.Inimigo.Nome, ".\n");
        }

        public static bool TesteCritico(int chanceCritico)
        {
            Random rnd = new Random();
            return rnd.Next(1, 101) <= chanceCritico && chanceCritico != 0
                ? true : false;
        }

        public static bool Vantagem(Tipos tipo1, Tipos tipo2) // Jogar em ataques depois
        {
            switch (tipo1)
            {
                case Tipos.Água:
                    if (tipo2 == Tipos.Fogo || tipo2 == Tipos.Terra)
                    {
                        return true;
                    }
                    break;
                case Tipos.Elétrico:
                    if (tipo2 == Tipos.Água)
                    {
                        return true;
                    }
                    break;
                case Tipos.Fogo:
                    if (tipo2 == Tipos.Gelo || tipo2 == Tipos.Grama)
                    {
                        return true;
                    }
                    break;
                case Tipos.Gelo:
                    if (tipo2 == Tipos.Grama || tipo2 == Tipos.Terra)
                    {
                        return true;
                    }
                    break;
                case Tipos.Grama:
                    if (tipo2 == Tipos.Água || tipo2 == Tipos.Terra)
                    {
                        return true;
                    }
                    break;
                case Tipos.Terra:
                    if (tipo2 == Tipos.Elétrico || tipo2 == Tipos.Fogo)
                    {
                        return true;
                    }
                    break;
                default:
                    Console.WriteLine("Error.");
                    break;
            }

            return false;
        }
    }
}
