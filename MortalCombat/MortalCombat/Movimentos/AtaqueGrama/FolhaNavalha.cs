using System.Globalization;
using MortalCombat.Entities;
using MortalCombat.Enums;

namespace MortalCombat.Movimentos.AtaqueGrama
{
    internal record class FolhaNavalha : Ataque
    {
        public override string NomeAtq { get; set; }
        public override Tipos TipoAtq { get; set; }
        public float DanoAtq { get; private set; }
        public float DanoCriticoAdicional { get; private set; }

        public FolhaNavalha() : base()
        {
            NomeAtq = "Folha navalha";
            DanoAtq = 50f;
            TipoAtq = Tipos.Grama;
            DanoCriticoAdicional = 0.1f;
        }

        public override void Atacar(Personagem jogador)
        {
            float danoCriticoTotal = jogador.DanoCritico + 
                (DanoCriticoAdicional * (100 - (jogador.Vida * 100 / jogador.VidaMaxima)));

            float danoCausado = (Miscelanea.TesteCritico(jogador.ChanceCritico))
                ? (DanoAtq * jogador.Dano * danoCriticoTotal)
                : (DanoAtq * jogador.Dano);

            float danoPosBlock = danoCausado - (danoCausado * jogador.Inimigo.Escudo);

            Miscelanea.PrintDano(jogador, danoPosBlock);
            jogador.Inimigo.SubtrairVida(danoCausado, true);
        }

        public override void Descricao()
        {
            Miscelanea.PrintColor("verde", NomeAtq, ": ");
            Console.Write("Causa ");
            Miscelanea.PrintColor("vermelho", DanoAtq.ToString("F1", CultureInfo.InvariantCulture));
            Console.Write(" de dano e, caso seja crítico, o dano é aumentado em ");
            Miscelanea.PrintColor("vermelho", (DanoCriticoAdicional * 100).ToString("F1", CultureInfo.InvariantCulture), "%");
            Console.Write(" para cada ");
            Miscelanea.PrintColor("verde", "1%");
            Console.Write(" de vida perdida.\n");
        }
    }
}
