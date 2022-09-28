using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortalCombat
{
    internal class Miscelanea
    {
        ConsoleColor corPadrao = ConsoleColor.White; // Volta a cor padão das letras;
        ConsoleColor corAmarelo = ConsoleColor.Yellow; // Destaca um aviso;
        ConsoleColor corVerde = ConsoleColor.Green; // Destaca aumento de status;
        ConsoleColor corVermelho = ConsoleColor.Red; // Destaca subtração de status;
        ConsoleColor corCiano = ConsoleColor.Cyan;
        public static void PrintColor(string cor, params string[] texto1)
        {
            switch(cor)
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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
            }


            for (int idx = 0; idx < texto1.Length; idx++)
            {
                Console.Write(texto1[idx]);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
