using System.Globalization;
using MortalCombat.Entities;
using MortalCombat.Enums;

namespace MortalCombat.Movimentos.AtaqueAgua
{
    internal record class PulsoHidrico : Ataque
    {
        public override string NomeAtq { get; set; }
        public override Tipos TipoAtq { get; set; }
        public float PorcentagemAtq { get; private set; }

        public PulsoHidrico() : base()
        {
            NomeAtq = "Pulso hídrico";
            PorcentagemAtq = 0.4f;
            TipoAtq = Tipos.Água;
        }

        public override void Atacar(Personagem jogador)
        {
            float danoCausado = (jogador.Inimigo.VidaMaxima - jogador.Inimigo.Vida) 
                * PorcentagemAtq * jogador.Dano;

            danoCausado *= (Miscelanea.TesteCritico(jogador.ChanceCritico)) 
                ? jogador.DanoCritico : 1;
            
            float danoPosBlock = danoCausado - (danoCausado * jogador.Inimigo.Escudo);

            Miscelanea.PrintDano(jogador, danoPosBlock);
            jogador.Inimigo.SubtrairVida(danoCausado, true);
        }
        
        public override void Descricao()
        {
            Miscelanea.PrintColor("ciano", NomeAtq + ": ");
            Console.Write("Causa ");
            Miscelanea.PrintColor("verde", (PorcentagemAtq * 100).ToString("F1", CultureInfo.InvariantCulture) + "%");
            Console.Write(" da vida perdida do inimigo como dano.");
        }
    }
}
