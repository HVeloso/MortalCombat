using System.Globalization;
using MortalCombat.Entities;
using MortalCombat.Enums;

namespace MortalCombat.Movimentos.AtaqueTerra
{
    internal record class PunhoDePedra : Ataque
    {
        public override string NomeAtq { get; set; }
        public override Tipos TipoAtq { get; set; }
        public float DanoAtq { get; private set; }
        public float PorcEscudo { get; private set; }

        public PunhoDePedra() : base()
        {
            NomeAtq = "Punho de pedra";
            DanoAtq = 60f;
            TipoAtq = Tipos.Terra;
            PorcEscudo = 0.5f;
        }

        public override void Atacar(Personagem jogador)
        {
            float danoCausado = DanoAtq * jogador.Dano;

            if(Miscelanea.TesteCritico(jogador.ChanceCritico))
            {
                danoCausado *= jogador.DanoCritico;
                float danoPosBlock = danoCausado - (danoCausado * jogador.Inimigo.Escudo);

                Miscelanea.PrintDano(jogador, danoPosBlock);
                jogador.Inimigo.SubtrairVida(danoCausado, true);

                Miscelanea.PrintColor("ciano", "\t" + jogador.Nome);
                Console.Write(" aumentou o próprio escudo em ");
                Miscelanea.PrintColor("ciano", (PorcEscudo * 100).ToString("F2", CultureInfo.InvariantCulture) + "%\n");
                jogador.AdicionarEscudo(PorcEscudo);
            }
            else
            {
                float danoPosBlock = danoCausado - (danoCausado * jogador.Inimigo.Escudo);

                Miscelanea.PrintDano(jogador, danoPosBlock);
                jogador.Inimigo.SubtrairVida(danoCausado, true);
            }
        }

        public override void Descricao()
        {
            Miscelanea.PrintColor("roxo", NomeAtq + ": ");
            Console.Write("Causa ");
            Miscelanea.PrintColor("vermelho", DanoAtq.ToString("F1", CultureInfo.InvariantCulture));
            Console.Write(" de dano no inimigo e, caso seja um ataque crítico, aumenta seu escudo em ");
            Miscelanea.PrintColor("ciano", (PorcEscudo * 100).ToString("F1", CultureInfo.InvariantCulture) + "%");
        }
    }
}
