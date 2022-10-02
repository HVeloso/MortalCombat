using MortalCombat.Entities;
using MortalCombat.Enums;
using MortalCombat.Movimentos;
using MortalCombat.Movimentos.AtaqueAgua;
using MortalCombat.Movimentos.AtaqueGrama;
using MortalCombat.Movimentos.AtaqueTerra;
using System.Globalization;

namespace MortalCombat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Pendente:
             *  - Substituir das os printcolor onde n é preciso de duas linhas por uma linha só;
             */
            
            Personagem[] jogadores = new Personagem[2];

            SetAtributos setAtr = new SetAtributos();
            setAtr.MenuUpgrade();
            setAtr.MenuAtaques();

            Tipos[] a = { Tipos.Grama, Tipos.Terra };
            jogadores[0] = new Personagem("higaro1", 1500f, 20, 1.75f, 60, 0f, 4, a, setAtr.SetAtaques());
            
            Console.Clear();

            jogadores[1] = new Personagem("higaro2", 1500f, 20, 1.75f, 60, 0.5f, 4, a, setAtr.SetAtaques());

            jogadores[0].SetarInimigo(jogadores[1]);
            jogadores[1].SetarInimigo(jogadores[0]);

            Console.Clear();
            Console.WriteLine("\to-----------------------------------o");
            jogadores[0].Log();
            Console.WriteLine("\to-----------------------------------o\n");
            foreach (Ataque item in jogadores[0].Ataques)
            {
                Console.WriteLine(item.NomeAtq);
            }
            
            Console.WriteLine("\n\n\n");
            // */
        }
    }
}
