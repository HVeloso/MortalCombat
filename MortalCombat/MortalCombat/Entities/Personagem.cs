using MortalCombat.Enums;
using MortalCombat.Movimentos;
using System.Globalization;

namespace MortalCombat.Entities
{
    internal class Personagem
    {
        // Atributos do personagem
        public string Nome { get; private set; }
        public float Vida { get; private set; }
        public float VidaMaxima { get; private set; }
        public float Cura { get; private set; }
        public int ChanceCritico { get; private set; }
        public float DanoCritico { get; private set; }
        public float Dano { get; private set; }
        public float Escudo { get; private set; }

        public int Pocoes { get; private set; }
        public Tipos[] TipoPersonagem = new Tipos[2];

        public Personagem Inimigo { get; private set; }
        public Ataque[,] Ataques = new Ataque[2, 2];

        // Construtor
        public Personagem(string nome, float vida, int chanceCritico
            , float danoCritico, float cura, float escudo, int pocoes
            , Tipos[] tipos, Ataque[,] ataques)
        {
            Nome = nome;
            Cura = cura;
            Vida = vida;
            VidaMaxima = Vida;
            ChanceCritico = chanceCritico;
            DanoCritico = danoCritico;
            Dano = 1.0f;
            Escudo = escudo;
            Pocoes = pocoes;
            TipoPersonagem = tipos;
            Ataques = ataques;
        }

        public void SetarInimigo(Personagem personagemInimigo)
        {
            Inimigo = personagemInimigo;
        }

        // Funções que modificam os atributos do personagem
        public void UsarPocao()
        {
            if (Pocoes > 0)
            {
                if (Vida != VidaMaxima)
                {
                    Pocoes--;
                    Console.WriteLine("1 poção gasta.");
                    AdicionarVida(Cura);
                }
                else
                {
                    Miscelanea.PrintColor("amarelo", "Vida já est");
                }
            }
            else if (Pocoes <= 0)
            {
                Miscelanea.PrintColor("amarelo", "Você não tem poções.");
            }
        }

        public void SubtrairVida(float valor, bool diminuirEscudo)
        {
            if (diminuirEscudo && Escudo > 0 && valor > 0)
            {
                valor -= valor * Escudo;
                Vida = Vida - valor < 0 ? 0 : Vida - valor;
                Miscelanea.PrintColor("ciano", "\t", (Escudo * 100).ToString("F1", CultureInfo.InvariantCulture) + "%");
                Console.Write(" do dano foi bloqueado.\n");
                ZerarEscudo();
            }
            else
            {
                Vida = Vida - valor < 0 ? 0 : Vida - valor;
            }
        }

        public void AdicionarVida(float valor)
        {
            Vida = Vida + valor > VidaMaxima ? VidaMaxima : Vida + valor;
        }

        public void SubtrairDano(float valor)
        {
            Dano = Dano - valor < 0 ? 0 : Dano - valor;
        }

        public void AdicionarDano(float valor)
        {
            Dano += valor;
        }

        public void SubtrairChanceCritico(int valor)
        {
            ChanceCritico = ChanceCritico - valor < 0 ? 0 : ChanceCritico - valor;
        }

        public void AdicionarChanceCritico(int valor)
        {
            ChanceCritico = ChanceCritico + valor > 100 ? 100 : ChanceCritico + valor;
        }

        public void SubtrairDanoCritico(float valor)
        {
            DanoCritico = DanoCritico - valor < 1 ? 1 : DanoCritico - valor;
        }

        public void AdicionarDanoCritico(float valor)
        {
            DanoCritico += valor;
        }

        public void SubtrairCura(float valor)
        {
            Cura = Cura - valor < 0 ? 0 : Cura - valor;
        }

        public void AdicionarCura(float valor)
        {
            Cura += valor;
        }

        public void ZerarEscudo()
        {
            Escudo = 0f;
        }

        public void SubtrairEscudo(float valor)
        {
            Escudo = Cura - valor < 0f ? 0f : Cura - valor;
        }

        public void AdicionarEscudo(float valor)
        {
            Escudo = Escudo + valor > 1f ? 1f : Escudo + valor;
        }

        public void Log()
        {

            Miscelanea.PrintColor("ciano", $"\t  - {Nome}:\n\n");

            Miscelanea.PrintColor("verde", "\t  Vida: ");
            Console.WriteLine($"{Vida.ToString("F2", CultureInfo.InvariantCulture)}");

            if (Escudo != 0)
            {
                Miscelanea.PrintColor("ciano", "\t  Escudo: ");
                Console.WriteLine($"{(Escudo * 100).ToString("F1")}%");
            }

            Console.WriteLine($"\t  Poções: {Pocoes.ToString()}");

            Miscelanea.PrintColor("verde", "\t  Cura: ");
            Console.WriteLine($"{Cura.ToString("F2", CultureInfo.InvariantCulture)}");

            Miscelanea.PrintColor("vermelho", "\t  Dano: ");
            Console.WriteLine($"{Dano.ToString("F2", CultureInfo.InvariantCulture)}");

            Miscelanea.PrintColor("amarelo", "\t  Chance de crítico: ");
            Console.WriteLine($"{ChanceCritico.ToString()} %");

            Miscelanea.PrintColor("vermelho", "\t  Dano crítico: ");
            Console.WriteLine($"{(DanoCritico * 100).ToString("F2", CultureInfo.InvariantCulture)} %");

            Console.Write("\t  Tipos: ");

            for (int idx = 0; idx < TipoPersonagem.Length; idx++)
            {
                if (TipoPersonagem[idx] == Tipos.Água)
                {
                    Miscelanea.PrintColor("ciano", "água");
                }
                else if (TipoPersonagem[idx] == Tipos.Elétrico)
                {
                    Miscelanea.PrintColor("amarelo", "elétrico");
                }
                else if (TipoPersonagem[idx] == Tipos.Fogo)
                {
                    Miscelanea.PrintColor("vermelho", "fogo");
                }
                else if (TipoPersonagem[idx] == Tipos.Gelo)
                {
                    Miscelanea.PrintColor("azul", "gelo");
                }
                else if (TipoPersonagem[idx] == Tipos.Grama)
                {
                    Miscelanea.PrintColor("verde", "grama");
                }
                else if (TipoPersonagem[idx] == Tipos.Terra)
                {
                    Miscelanea.PrintColor("roxo", "terra");
                }

                Console.Write(idx == 0 ? " e " : "");
            }
            Console.Write("\n");
        }
    }
}
