using System.Globalization;

namespace MortalCombat
{
    internal class Personagem
    {   
        // Atributos do personagem
        public string Nome { get; private set; }
        public float Vida { get; private set; }
        public float Cura { get; private set; }
        public float Dano { get; private set; }
        public int ChanceCritico { get; private set; }
        public float DanoCritico { get; private set; }
        public int Pocoes { get; private set; }
        //public Tipos[] TipoPersonagem = new Tipos[2];
        // vetor de ataques

        // Construtor
        public Personagem(string nome, float vida, float dano, int chanceCritico,
            float danoCritico, float cura, int pocoes/*, Tipos tipo1, Tipos tipo2*/)
        {
            Nome = nome;
            Cura = cura;
            Vida = vida;
            Dano = dano;
            ChanceCritico = chanceCritico;
            DanoCritico = danoCritico;
            Pocoes = pocoes;
            //TipoPersonagem[0] = tipo1;
            //TipoPersonagem[1] = tipo2;
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
                Miscelanea.PrintColor("amarelo", "Você não tem poções.");
            }
        }

        public void SubtrairVida(float valor)
        {
            valor = (float)valor;
            Vida = (Vida - valor < 0) ? 0 : Vida - valor;
            Console.Write("Vida ");
            Miscelanea.PrintColor("vermelho", $"-{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void AdicionarVida(float valor)
        {
            valor = (float)valor;
            Vida += valor;
            Console.Write("Vida ");
            Miscelanea.PrintColor("verde", $"+{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void SubtrairDano(float valor)
        {
            valor = (float)valor;
            Dano = (Dano - valor < 0) ? 0 : Dano - valor;
            Console.Write("Dano ");
            Miscelanea.PrintColor("vermelho", $"-{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void AdicionarDano(float valor)
        {
            valor = (float)valor;
            Dano += valor;
            Console.Write("Dano ");
            Miscelanea.PrintColor("verde", $"+{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void SubtrairChanceCritico(int valor)
        {
            valor = (int)valor;
            ChanceCritico = (ChanceCritico - valor < 0) ? 0 : ChanceCritico - valor;
            Console.Write("Chance de crítico ");
            Miscelanea.PrintColor("vermelho", $"-{valor.ToString(CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void AdicionarChanceCritico(int valor)
        {
            valor = (int)valor;
            ChanceCritico = (ChanceCritico + valor > 100) ? 100 : ChanceCritico + valor;
            Console.Write("Chance de crítico ");
            Miscelanea.PrintColor("verde", $"+{valor.ToString(CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void SubtrairDanoCritico(float valor)
        {
            valor = (float)valor;
            DanoCritico = (DanoCritico - valor < 1) ? 1 : DanoCritico - valor;
            Console.Write("Dano crítico ");
            Miscelanea.PrintColor("vermelho", $"-{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void AdicionarDanoCritico(float valor)
        {
            valor = (float)valor;
            DanoCritico = (DanoCritico + valor > 3) ? 3 : DanoCritico + valor;
            Console.Write("Dano crítico ");
            Miscelanea.PrintColor("verde", $"+{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void SubtrairCura(float valor)
        {
            valor = (float)valor;
            Cura = (Cura - valor < 0) ? 0 : Cura - valor;
            Console.Write("Cura ");
            Miscelanea.PrintColor("vermelho", $"-{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void AdicionarCura(float valor)
        {
            valor = (float)valor;
            Cura += valor;
            Console.Write("Cura ");
            Miscelanea.PrintColor("verde", $"+{valor.ToString("2F", CultureInfo.InvariantCulture)}");
            Console.Write(".");
        }

        public void Log()
        {

            Console.WriteLine("\to-----------------------------o");
            Miscelanea.PrintColor("ciano", $"\t  - {Nome}:\n\n");

            Miscelanea.PrintColor("verde", "\t  Vida: ");
            Console.WriteLine($"{Vida.ToString("F2", CultureInfo.InvariantCulture)}");

            Console.WriteLine($"\t  Poções: {Pocoes.ToString()}");

            Miscelanea.PrintColor("verde", "\t  Cura: ");
            Console.WriteLine($"{Cura.ToString("F2", CultureInfo.InvariantCulture)}");

            Miscelanea.PrintColor("vermelho", "\t  Dano: ");
            Console.WriteLine($"{Dano.ToString("F2", CultureInfo.InvariantCulture)}");

            Miscelanea.PrintColor("amarelo", "\t  Chance de crítico: ");
            Console.WriteLine($"{ChanceCritico.ToString()} %");

            Miscelanea.PrintColor("vermelho", "\t  Dano crítico: ");
            Console.WriteLine($"{(DanoCritico * 100).ToString("F2", CultureInfo.InvariantCulture)} %");

            Console.WriteLine("\to-----------------------------o");
        }
    }
}
