using MortalCombat.Entities;
using MortalCombat.Enums;

namespace MortalCombat.Movimentos
{
    internal record class AtaqueNulo : Ataque
    {
        public override string NomeAtq { get; set; }
        public override Tipos TipoAtq { get; set; }

        public AtaqueNulo()
        {
            NomeAtq = "Nulo";
            TipoAtq = Tipos.Nulo;
        }

        public override void Atacar(Personagem jogador) { }
        public override void Descricao() { Console.WriteLine("NULO"); }
    }
}
