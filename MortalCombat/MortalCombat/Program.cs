using System.Globalization;

namespace MortalCombat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //Console.WriteLine("Hello World!");
            SetAtributos setAtb = new SetAtributos();
            setAtb.MenuUpgrade("Higaro");
            Console.WriteLine("\n\n");
            
            Console.Clear();
            Personagem per = new Personagem("Hígaro", setAtb.vidaBase, setAtb.danoBase
                , setAtb.chanceCriticoBase, setAtb.danoCriticoBase, setAtb.curaBase, 6);
            per.Log();
        }
    }
}
