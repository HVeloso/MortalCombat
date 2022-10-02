using System.Globalization;
using MortalCombat.Entities;
using MortalCombat.Enums;

namespace MortalCombat.Movimentos.AtaqueGrama
{
    internal record class MarteloDeMadeira : Ataque
    {
        public override string NomeAtq { get; set; }
        public override Tipos TipoAtq { get; set; }
        public float DanoAtq { get; private set; }

        public MarteloDeMadeira() : base ()
        {
            NomeAtq = "Martelo de madeira";
            DanoAtq = 150f;
            TipoAtq = Tipos.Grama;
        }

        public override void Atacar(Personagem jogador)
        {
            float danoCausado = (Miscelanea.TesteCritico(jogador.ChanceCritico)) 
                ? (DanoAtq * jogador.Dano * jogador.DanoCritico)
                : (DanoAtq * jogador.Dano);

            float danoPosBlock = danoCausado - (danoCausado * jogador.Inimigo.Escudo);

            Miscelanea.PrintDano(jogador, danoPosBlock);
            jogador.Inimigo.SubtrairVida(danoCausado, true);

            Miscelanea.PrintColor("ciano", "\t", jogador.Nome);
            Console.Write(" causou ");
            Miscelanea.PrintColor("vermelho", (danoCausado / 3).ToString("F1", CultureInfo.InvariantCulture));
            Console.Write(" de dano em ");
            Miscelanea.PrintColor("ciano", "si mesmo.\n");
            jogador.SubtrairVida(danoCausado / 3, false);
        }

        public override void Descricao()
        {
            Miscelanea.PrintColor("verde", NomeAtq + ": ");
            Console.Write("Causa ");
            Miscelanea.PrintColor("vermelho", DanoAtq.ToString("F1", CultureInfo.InvariantCulture));
            Console.Write(" de dano no inimigo e ");
            Miscelanea.PrintColor("vermelho", "1/3");
            Console.Write(" desse dano em você.\n");
        }
    }
}
