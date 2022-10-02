using System.Globalization;
using MortalCombat.Entities;
using MortalCombat.Enums;

namespace MortalCombat.Movimentos.AtaqueAgua
{
    internal record class TrombaDagua : Ataque
    {
        public override string NomeAtq { get; set; }
        public override Tipos TipoAtq { get; set; }
        public float DanoAtqVMaior { get; private set; }
        public float DanoAtqVIgual { get; private set; }
        public float DanoAtqVMenor { get; private set; }

        public TrombaDagua() : base()
        {
            NomeAtq = "Tromba d'água";
            TipoAtq = Tipos.Água;
            DanoAtqVMaior = 200f;
            DanoAtqVIgual = 100f;
            DanoAtqVMenor = 50f;
        }

        public override void Atacar(Personagem jogador)
        {
            float danoCausado = jogador.Dano;
            if(jogador.Vida > jogador.Inimigo.Vida)
            {
                danoCausado *= DanoAtqVMaior;
            }
            else if(jogador.Vida < jogador.Inimigo.Vida)
            {
                danoCausado *= DanoAtqVMenor;
            }
            else
            {
                danoCausado *= DanoAtqVIgual;
            }

            danoCausado *= (Miscelanea.TesteCritico(jogador.ChanceCritico)) 
                ? jogador.DanoCritico : 1;
            
            float danoPosBlock = danoCausado - (danoCausado * jogador.Inimigo.Escudo);

            Miscelanea.PrintDano(jogador, danoPosBlock);
            jogador.Inimigo.SubtrairVida(danoCausado, true);
        }

        public override void Descricao()
        {
            Miscelanea.PrintColor("ciano", NomeAtq, ": ");
            Console.Write("Causa ");
            Miscelanea.PrintColor("vermelho"
                , DanoAtqVMaior.ToString("F1", CultureInfo.InvariantCulture), ", "
                , DanoAtqVIgual.ToString("F1", CultureInfo.InvariantCulture), ", "
                , DanoAtqVMenor.ToString("F1", CultureInfo.InvariantCulture));
            Console.Write(" de dano no inimigo se sua vida for, respectivamente," +
                " maior, igual ou menor que a dele.\n");
        }
    }
}
