using MortalCombat.Entities;
using MortalCombat.Enums;

namespace MortalCombat.Movimentos
{
    internal abstract record Ataque
    {
        public abstract string NomeAtq { get;  set; }
        public abstract Tipos TipoAtq { get; set; }

        public Ataque() { }
        public abstract void Atacar(Personagem jogador);
        public abstract void Descricao();
    }
}
