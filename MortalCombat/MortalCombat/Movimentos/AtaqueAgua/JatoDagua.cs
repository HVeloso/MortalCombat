using System.Globalization;
using MortalCombat.Entities;
using MortalCombat.Enums;

namespace MortalCombat.Movimentos.AtaqueAgua
{
    internal record class JatoDagua : Ataque
    {
        public override string NomeAtq { get; set; }
        public float DanoAtq { get; private set; }
        public override Tipos TipoAtq { get; set; }
        public byte QuantAtaques { get; private set; }

        public JatoDagua() : base()
        {
            NomeAtq = "Jato d'agua";
            DanoAtq = 20f;
            TipoAtq = Tipos.Água;
            QuantAtaques = 3;
        }

        public override void Atacar(Personagem jogador)
        {
            Miscelanea.PrintColor("ciano", "\t" + jogador.Nome);
            Console.Write(" causa:\n");

            for (int i = 0; i < QuantAtaques; i++)
            {
                float danoCausado = (Miscelanea.TesteCritico(jogador.ChanceCritico))
                    ? (DanoAtq * jogador.Dano * jogador.DanoCritico)
                    : (DanoAtq * jogador.Dano);

                float danoPosBlock = danoCausado - (danoCausado * jogador.Inimigo.Escudo);
                float escudoInicial = jogador.Inimigo.Escudo;

                Miscelanea.PrintColor("vermelho", "\t" + danoPosBlock.ToString("F2", CultureInfo.InvariantCulture));
                Console.Write(" de dano.");
                jogador.Inimigo.SubtrairVida(danoCausado, true);
                Console.Write((escudoInicial == jogador.Inimigo.Escudo) ? "\n" : "");
            }
            Console.Write("\tem ");
            Miscelanea.PrintColor("ciano", jogador.Inimigo.Nome + "\n");
        }

        public override void Descricao()
        {
            Miscelanea.PrintColor("ciano", NomeAtq + ": ");
            Console.Write("Ataca ");
            Miscelanea.PrintColor("amarelo", QuantAtaques.ToString());
            Console.Write(" vezes causando ");
            Miscelanea.PrintColor("vermelho", DanoAtq.ToString("F1", CultureInfo.InvariantCulture));
            Console.Write(" dano em cada ataque. Pode causar crítico.\n");
        }
    }
}
