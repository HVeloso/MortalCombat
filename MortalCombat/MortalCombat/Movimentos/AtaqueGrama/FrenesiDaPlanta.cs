using System.Globalization;
using MortalCombat.Entities;
using MortalCombat.Enums;

namespace MortalCombat.Movimentos.AtaqueGrama
{
    internal record class FrenesiDaPlanta : Ataque
    {
        public override string NomeAtq { get; set; }
        public override Tipos TipoAtq { get; set; }
        public float DanoAtq { get; private set; }
        public float Porcentagem { get; private set; }

        public FrenesiDaPlanta() : base ()
        {
            NomeAtq = "Frenesi da planta";
            DanoAtq = 10f;
            TipoAtq = Tipos.Grama;
            Porcentagem = 0.5f;
        }

        public override void Atacar(Personagem jogador)
        {
            float danoCausado = (jogador.Vida > jogador.Inimigo.Vida)
                ? DanoAtq + ((jogador.Vida - jogador.Inimigo.Vida) * Porcentagem) 
                : DanoAtq;
            
            danoCausado = (Miscelanea.TesteCritico(jogador.ChanceCritico))
                ? (danoCausado * jogador.Dano * jogador.DanoCritico)
                : (danoCausado * jogador.Dano);

            float danoPosBlock = danoCausado - (danoCausado * jogador.Inimigo.Escudo);

            Miscelanea.PrintDano(jogador, danoPosBlock);
            jogador.Inimigo.SubtrairVida(danoCausado, true);
        }

        public override void Descricao()
        {
            Miscelanea.PrintColor("verde", NomeAtq, ": ");
            Console.Write("Causa ");
            Miscelanea.PrintColor("vermelho", DanoAtq.ToString("F1", CultureInfo.InvariantCulture));
            Console.Write(" mais ");
            Miscelanea.PrintColor("verde", $"{(Porcentagem * 100).ToString("F1", CultureInfo.InvariantCulture)}%");
            Console.Write(" da diferença de vida entre você e o alvo como dano.\n");
        }
    }
}
