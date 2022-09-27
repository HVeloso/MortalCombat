using System.Globalization;
using System.Text;

namespace MortalCombat
{
    internal class Personagem
    {
        // Cores
        ConsoleColor corPadrao = ConsoleColor.White; // Volta a cor padão das letras;
        ConsoleColor corVerde = ConsoleColor.Green; // Destaca aumento de status;
        ConsoleColor corVermelho = ConsoleColor.Red; // Destaca subtração de status;
        ConsoleColor corAmarelo = ConsoleColor.Yellow; // Destaca um aviso;

        // Atributos do personagem
        public string Nome { get; private set; }
        public float Vida { get; private set; }
        public float Dano { get; private set; }
        public int ChanceCritico { get; private set; }
        public float DanoCritico { get; private set; }
        public float Cura { get; private set; }
        public int Pocoes { get; private set; }
        public int Mana { get; private set; }
        public Tipos[] TipoPersonagem = new Tipos[2];


        // Construtor
        public Personagem(string nome, float vida, float dano, int chanceCritico,
            float danoCritico, float cura, int pocoes, int mana, Tipos tipo1, Tipos tipo2)
        {
            Nome = nome;
            Vida = vida;
            Dano = dano;
            ChanceCritico = chanceCritico;
            DanoCritico = danoCritico;
            Cura = cura;
            Pocoes = pocoes;
            Mana = mana;
            TipoPersonagem[0] = tipo1;
            TipoPersonagem[1] = tipo2;
        }

        // Funções que modificam os atributos do personagem
        public void UsarPocao()
        {
            if (Pocoes > 0)
            {
                Pocoes--;
                Console.WriteLine("1 poção gasta.");
                AdicionarVida(Cura);
            }
            else
            {
                PrintColor(corAmarelo, "Você não tem poções.");
            }
        }

        public void SubtrairVida(float valor)
        {
            Vida = (Vida - valor < 0) ? 0 : Vida - valor;
            Console.Write("Vida ");
            PrintColor(corVermelho, $"-{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void AdicionarVida(float valor)
        {
            Vida += valor;
            Console.Write("Vida ");
            PrintColor(corVermelho, $"+{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void SubtrairDano(float valor)
        {
            Dano = (Dano - valor < 0) ? 0 : Dano - valor;
            Console.Write("Dano ");
            PrintColor(corVermelho, $"-{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void AdicionarDano(float valor)
        {
            Dano += valor;
            Console.Write("Dano ");
            PrintColor(corVerde, $"+{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void SubtrairChanceCritico(int valor)
        {
            ChanceCritico = (ChanceCritico - valor < 0) ? 0 : ChanceCritico - valor;
            Console.Write("Chance de crítico ");
            PrintColor(corVermelho, $"-{valor.ToString(CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void AdicionarChanceCritico(int valor)
        {
            ChanceCritico = (ChanceCritico + valor > 100) ? 100 : ChanceCritico + valor;
            Console.Write("Chance de crítico ");
            PrintColor(corVerde, $"+{valor.ToString(CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void SubtrairDanoCritico(float valor)
        {
            DanoCritico = (DanoCritico - valor < 1) ? 1 : DanoCritico - valor;
            Console.Write("Dano crítico ");
            PrintColor(corVermelho, $"-{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void AdicionarDanoCritico(float valor)
        {
            DanoCritico = (DanoCritico + valor > 3) ? 3 : DanoCritico + valor;
            Console.Write("Dano crítico ");
            PrintColor(corVerde, $"+{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void SubtrairCura(float valor)
        {
            Cura = (Cura - valor < 0) ? 0 : Cura - valor;
            Console.Write("Cura ");
            PrintColor(corVermelho, $"-{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void AdicionarCura(float valor)
        {
            Cura += valor;
            Console.Write("Cura ");
            PrintColor(corVerde, $"+{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        // Funções miscelânea
        private void PrintColor(ConsoleColor novaCor, params string[] texto1)
        {
            Console.ForegroundColor = novaCor;
            for(int idx = 0; idx < texto1.Length; idx++)
            {
                Console.Write(texto1[idx]);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Log()
        {

            Console.WriteLine("\to--------------------------------o");
            PrintColor(corAmarelo ,$"\t  {Nome}\n");

            PrintColor(corVerde, "\t  Vida: ");
            Console.WriteLine($"{Vida.ToString("F2", CultureInfo.InvariantCulture)}");
            
            PrintColor(corVermelho, "\t  Dano: ");
            Console.WriteLine($"{Dano.ToString("F2", CultureInfo.InvariantCulture)}");
            
            PrintColor(corAmarelo, "\t  Chance de crítico: ");
            Console.WriteLine($"{ChanceCritico.ToString()}", "%");
            
            PrintColor(corVermelho, "\t  Dano crítico: ");
            Console.WriteLine($"{(DanoCritico * 100).ToString("F2", CultureInfo.InvariantCulture)}", "%");
            
            PrintColor(corVerde, "\t  Cura: ");
            Console.WriteLine($"{Cura.ToString("F2", CultureInfo.InvariantCulture)}");
            
            Console.WriteLine($"\t  Poções: {Pocoes.ToString()}");
            Console.WriteLine("\to--------------------------------o");
        }
    }
}
