using MortalCombat.Enums;
using MortalCombat.Movimentos;
using MortalCombat.Movimentos.AtaqueAgua;
using MortalCombat.Movimentos.AtaqueGrama;
using MortalCombat.Movimentos.AtaqueTerra;
using System.Globalization;

namespace MortalCombat.Entities
{
    internal class SetAtributos
    {
        public string nome { get; private set; }
        public float vidaBase { get; private set; }
        public float curaBase { get; private set; }
        public int chanceCriticoBase { get; private set; }
        public float danoCriticoBase { get; private set; }

        public float incrementoVida { get; private set; }
        public float incrementoCura { get; private set; }
        public float incrementoDano { get; private set; }
        public int incrementoChanceCritico { get; private set; }
        public float incrementoDanoCritico { get; private set; }

        private float vidaAdicional;
        private float curaAdicional;
        private int chanceCriticoAdicional;
        private float danoCriticoAdicional;

        public int pontos { get; private set; }
        private int maxUpgrade;
        public int[] upgrades { get; private set; } = new int[4];

        private Tipos[] prsTipo = new Tipos[2];

        // Ataques
        private Ataque[,] ataques = new Ataque[2, 2];

        List<Ataque> ataquesAgua = new List<Ataque>();
        private void SetAtqAgua()
        {
            ataquesAgua.Add(new JatoDagua());
            ataquesAgua.Add(new PulsoHidrico());
            ataquesAgua.Add(new TrombaDagua());
        }
        List<Ataque> ataquesEletrico = new List<Ataque>();
        List<Ataque> ataquesFogo = new List<Ataque>();
        List<Ataque> ataquesGelo = new List<Ataque>();
        List<Ataque> ataquesGrama = new List<Ataque>();
        private void SetAtqGrama()
        {
            ataquesGrama.Add(new FolhaNavalha());
            ataquesGrama.Add(new FrenesiDaPlanta());
            ataquesGrama.Add(new MarteloDeMadeira());
        }
        List<Ataque> ataquesTerra = new List<Ataque>();
        private void SetAtqTerra()
        {
            ataquesTerra.Add(new PunhoDePedra());
        }

        // Funções
        public Tipos[] SetTipos()
        {
            return prsTipo;
        }
        public Ataque[,] SetAtaques()
        {
            return ataques;
        }
        public SetAtributos()
        {
            Resetar();
            incrementoVida = 275f;
            incrementoCura = 67f;
            incrementoChanceCritico = 11;
            incrementoDanoCritico = 0.2f;
        }
        public void Resetar()
        {
            nome = "";
            vidaBase = 1500f;
            curaBase = 72.5f;
            chanceCriticoBase = 10;
            danoCriticoBase = 1.75f;
            vidaAdicional = 0f;
            curaAdicional = 0f;
            chanceCriticoAdicional = 0;
            danoCriticoAdicional = 0f;

            maxUpgrade = 4;
            pontos = 0;
            for (int idx = 0; idx < upgrades.Length; idx++)
            {
                upgrades[idx] = 0;
            }

            for (int idx = 0; idx < prsTipo.Length; idx++)
            {
                prsTipo[idx] = Tipos.Nulo;
            }

            for (int idx1 = 0; idx1 < ataques.GetLength(0); idx1++)
            {
                for (int idx2 = 0; idx2 < ataques.GetLength(1); idx2++)
                {
                    ataques[idx1, idx2] = new AtaqueNulo();
                }
            }
        }
        private void Atualizar()
        {
            vidaAdicional = (incrementoVida + upgrades[0] * 10) * upgrades[0];
            curaAdicional = (incrementoCura + upgrades[1] * 10) * upgrades[1];
            chanceCriticoAdicional = (incrementoChanceCritico + upgrades[2]) * upgrades[2];
            danoCriticoAdicional = (incrementoDanoCritico + upgrades[3] / 100) * upgrades[3];
        }
        private void Upar(byte indice)
        {
            bool invalido = false;
            do
            {
                Console.Clear();
                Miscelanea.PrintColor("amarelo", $"\tPontos: {pontos}\n");
                Console.Write("\tAtributo: ");
                switch (indice)
                {
                    case 0:
                        Miscelanea.PrintColor("verde", "vida: ");
                        Console.Write($"{(vidaBase + vidaAdicional).ToString("F2", CultureInfo.InvariantCulture)}");
                        break;
                    case 1:
                        Miscelanea.PrintColor("verde", "cura: ");
                        Console.Write($"{(curaBase + curaAdicional).ToString("F2", CultureInfo.InvariantCulture)}");
                        break;
                    case 2:
                        Miscelanea.PrintColor("amarelo", "chance de crítico: ");
                        Console.Write($"{(chanceCriticoBase + chanceCriticoAdicional).ToString()} %");
                        break;
                    case 3:
                        Miscelanea.PrintColor("vermelho", "dano crítico: ");
                        Console.Write($"{((danoCriticoBase + danoCriticoAdicional) * 100).ToString("F2", CultureInfo.InvariantCulture)} %");
                        break;
                }
                Miscelanea.PrintColor("amarelo", upgrades[indice] == maxUpgrade ? " (Max.)\n" : "\n");

                Console.WriteLine("\t[0] - Para voltar");

                Console.Write("\t[1] - ");
                Miscelanea.PrintColor("vermelho", "Diminuir Status <");
                Console.Write("|");
                Miscelanea.PrintColor("verde", "> Aumentar Status");
                Console.WriteLine(" - [2]");

                if (invalido)
                {
                    Miscelanea.PrintColor("amarelo", "\n - Valor inválido!\n");
                    invalido = false;
                }

                char op;
                op = Console.ReadKey().KeyChar;
                List<char> valoresValidos = new List<char> { '0', '1', '2' };

                if (valoresValidos.Contains(op))
                {
                    op -= (char)48;
                }
                else
                {
                    invalido = true;
                }

                if (op == 0)
                {
                    break;
                }
                else if (op == 1 && upgrades[indice] >= 1)
                {
                    pontos++;
                    upgrades[indice]--;
                }
                else if (op == 2 && upgrades[indice] < maxUpgrade && pontos > 0)
                {
                    pontos--;
                    upgrades[indice]++;
                }
                Atualizar();
            } while (true);
        }
        private void AddTipos(Tipos tipo)
        {
            if (prsTipo[0] == Tipos.Nulo)
            {
                prsTipo[0] = tipo;
            }
            else if (prsTipo[1] == Tipos.Nulo)
            {
                prsTipo[1] = tipo;
            }
        }
        private void RemoveTipos(Tipos tipo)
        {
            if (prsTipo[0] == tipo)
            {
                prsTipo[0] = Tipos.Nulo;
            }
            else if (prsTipo[1] == tipo)
            {
                prsTipo[1] = Tipos.Nulo;
            }
        }
        private void GetTipos()
        {
            bool invalido = false;
            bool invalido2 = false;

            do
            {
                Console.Clear();
                Console.WriteLine("\to-----------------------------o");
                Console.WriteLine("\tEscolha dois tipos: \n");

                Miscelanea.PrintColor("ciano", prsTipo.Contains(Tipos.Água) ? "\t + [1] - Retirar tipo água.\n" : "\t[1] - Escolher tipo água.\n");
                Miscelanea.PrintColor("amarelo", prsTipo.Contains(Tipos.Elétrico) ? "\t + [2] - Retirar tipo elétrico.\n" : "\t[2] - Escolher tipo elétrico.\n");
                Miscelanea.PrintColor("vermelho", prsTipo.Contains(Tipos.Fogo) ? "\t + [3] - Retirar tipo fogo.\n" : "\t[3] - Escolher tipo fogo.\n");
                Miscelanea.PrintColor("azul", prsTipo.Contains(Tipos.Gelo) ? "\t + [4] - Retirar tipo gelo.\n" : "\t[4] - Escolher tipo gelo.\n");
                Miscelanea.PrintColor("verde", prsTipo.Contains(Tipos.Grama) ? "\t + [5] - Retirar tipo grama.\n" : "\t[5] - Escolher tipo grama.\n");
                Miscelanea.PrintColor("roxo", prsTipo.Contains(Tipos.Terra) ? "\t + [6] - Retirar tipo terra.\n" : "\t[6] - Escolher tipo terra.\n");

                Console.WriteLine("\n\t[0] - Para confirmar os tipos.");
                Console.WriteLine("\n\to-----------------------------o\n");

                if (invalido)
                {
                    Miscelanea.PrintColor("amarelo", "\n - Valor inválido!\n");
                    invalido = false;
                }
                else if (invalido2)
                {
                    Miscelanea.PrintColor("amarelo", "\tEscolha DOIS tipos.\n");
                    invalido2 = false;
                }

                char op = Console.ReadKey().KeyChar;
                List<char> valoresValidos = new List<char> { '0', '1', '2', '3', '4', '5', '6' };

                if (valoresValidos.Contains(op))
                {
                    op -= (char)48;
                }
                else
                {
                    invalido = true;
                }

                if (op == 0)
                {
                    if (prsTipo.Contains(Tipos.Nulo))
                    {
                        invalido2 = true;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (op == 1)
                {
                    if (prsTipo.Contains(Tipos.Água))
                    {
                        RemoveTipos(Tipos.Água);
                    }
                    else
                    {
                        AddTipos(Tipos.Água);
                    }
                }
                else if (op == 2)
                {
                    if (prsTipo.Contains(Tipos.Elétrico))
                    {
                        RemoveTipos(Tipos.Elétrico);
                    }
                    else
                    {
                        AddTipos(Tipos.Elétrico);
                    }
                }
                else if (op == 3)
                {
                    if (prsTipo.Contains(Tipos.Fogo))
                    {
                        RemoveTipos(Tipos.Fogo);
                    }
                    else
                    {
                        AddTipos(Tipos.Fogo);
                    }
                }
                else if (op == 4)
                {
                    if (prsTipo.Contains(Tipos.Gelo))
                    {
                        RemoveTipos(Tipos.Gelo);
                    }
                    else
                    {
                        AddTipos(Tipos.Gelo);
                    }
                }
                else if (op == 5)
                {
                    if (prsTipo.Contains(Tipos.Grama))
                    {
                        RemoveTipos(Tipos.Grama);
                    }
                    else
                    {
                        AddTipos(Tipos.Grama);
                    }
                }
                else if (op == 6)
                {
                    if (prsTipo.Contains(Tipos.Terra))
                    {
                        RemoveTipos(Tipos.Terra);
                    }
                    else
                    {
                        AddTipos(Tipos.Terra);
                    }
                }
            } while (true);
        }
        public void MenuUpgrade()
        {
            Miscelanea.PrintColor("ciano", "Digite seu nome: ");
            nome = Console.ReadLine();

            bool invalido = false;
            bool invalido2 = false;
            do
            {
                Console.Clear();
                Console.WriteLine("\to-----------------------------o");
                Miscelanea.PrintColor("ciano", $"\t{nome}, escolha um atributo para upar:\n");
                Console.WriteLine($"\t  Pontos: {pontos}");

                Console.Write("{0}", upgrades[0] > 0 ? "\t+ " : "\t");
                Miscelanea.PrintColor("verde", "[1] - Vida: ");
                Console.WriteLine($"{(vidaBase + vidaAdicional).ToString("F2", CultureInfo.InvariantCulture)}");

                Console.Write("{0}", upgrades[1] > 0 ? "\t+ " : "\t");
                Miscelanea.PrintColor("verde", "[2] - Cura: ");
                Console.WriteLine($"{(curaBase + curaAdicional).ToString("F2", CultureInfo.InvariantCulture)}");

                Console.Write("{0}", upgrades[2] > 0 ? "\t+ " : "\t");
                Miscelanea.PrintColor("amarelo", "[3] - Chance de crítico: ");
                Console.WriteLine($"{(chanceCriticoBase + chanceCriticoAdicional).ToString()} %");

                Console.Write("{0}", upgrades[3] > 0 ? "\t+ " : "\t");
                Miscelanea.PrintColor("vermelho", "[4] - Dano crítico: ");
                Console.WriteLine($"{((danoCriticoBase + danoCriticoAdicional) * 100).ToString("F2", CultureInfo.InvariantCulture)} %");

                Console.WriteLine("\to-----------------------------o");
                Console.WriteLine("\t[0] - Para confirmar os upgrades.\n");

                if (invalido)
                {
                    Miscelanea.PrintColor("amarelo", " - Valor inválido!\n");
                    invalido = false;
                }
                else if (invalido2)
                {
                    Miscelanea.PrintColor("amarelo", "\tVocê ainda tem pontos não gastos.\n");
                    invalido2 = false;
                }

                char op;
                op = Console.ReadKey().KeyChar;

                List<char> valoresValidos = new List<char> { '0', '1', '2', '3', '4' };
                if (valoresValidos.Contains(op))
                {
                    op -= (char)48;
                }
                else
                {
                    invalido = true;
                }

                if (op == 0)
                {
                    if (pontos == 0)
                    {
                        break;
                    }
                    else
                    {
                        invalido2 = true;
                    }
                }
                else if (valoresValidos.Contains((char)(op + 48)))
                {
                    Upar((byte)(op - 1));
                }
            } while (true);

            vidaBase += vidaAdicional;
            curaBase += curaAdicional;
            chanceCriticoBase += chanceCriticoAdicional;
            danoCriticoBase += danoCriticoAdicional;

            GetTipos();
        }
        private byte QntAtaquesNulo()
        {
            byte qntAtqs = (byte)ataques.Length;
            AtaqueNulo atqNulo = new AtaqueNulo();

            for (int idx = 0; idx < ataques.GetLength(0); idx++)
            {
                for (int idx2 = 0; idx2 < ataques.GetLength(1); idx2++)
                {
                    qntAtqs -= ataques[idx, idx2].NomeAtq != atqNulo.NomeAtq ? (byte)1 : (byte)0;
                }
            }

            return qntAtqs;
        }

        private bool MatrizContem(Ataque[,] mat, Ataque atq)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j] == atq)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void MenuAtaques()
        {
            for (int idx = 0; idx < prsTipo.Length; idx++)
            {
                Tipos tipo = prsTipo[idx];
                List<Ataque> ataquesAux = new List<Ataque>();

                switch (tipo)
                {
                    case Tipos.Água:
                        SetAtqAgua();
                        foreach (Ataque item in ataquesAgua)
                        {
                            ataquesAux.Add(item);
                        }
                        break;

                    case Tipos.Elétrico:
                        foreach (Ataque item in ataquesEletrico)
                        {
                            ataquesAux.Add(item);
                        }
                        break;

                    case Tipos.Fogo:
                        foreach (Ataque item in ataquesFogo)
                        {
                            ataquesAux.Add(item);
                        }
                        break;

                    case Tipos.Gelo:
                        foreach (Ataque item in ataquesGelo)
                        {
                            ataquesAux.Add(item);
                        }
                        break;

                    case Tipos.Grama:
                        SetAtqGrama();
                        foreach (Ataque item in ataquesGrama)
                        {
                            ataquesAux.Add(item);
                        }
                        break;

                    case Tipos.Terra:
                        SetAtqTerra();
                        foreach (Ataque item in ataquesTerra)
                        {
                            ataquesAux.Add(item);
                        }
                        break;
                }

                byte pagina = 1;
                byte selecao = 1;
                byte qntItens = 4;
                byte qntItensUP = ataquesAux.Count() % 4 == 0 ? (byte)4 : (byte)(ataquesAux.Count() % 4);
                byte paginaMax = (byte)Math.Ceiling(ataquesAux.Count() / (double)4);

                do
                {
                    Console.Clear();
                    byte quantRealItens = pagina == paginaMax ? qntItensUP : qntItens;
                    Ataque[] atqEscolhido = new Ataque[qntItens];
                    for (int i = 0; i < quantRealItens; i++)
                    {
                        atqEscolhido[i] = ataquesAux[qntItens * pagina - qntItens + i];
                    }

                    Console.WriteLine($"\tUse W e S para escolher DOIS ataques do tipo {prsTipo[idx]}:");
                    Miscelanea.PrintColor("amarelo", "\tUma vez escolhido, não há como voltar, tome cuidado!\n");
                    Console.WriteLine($"\tPágina {pagina}.\n");

                    for (int i = 0; i < quantRealItens; i++)
                    {
                        byte index = (byte)(qntItens * pagina - qntItens + i);

                        Miscelanea.PrintColor(selecao == i + 1 ? "azul" : ""
                            , MatrizContem(ataques, ataquesAux[index]) ? $"\t + [{index + 1}] - " : $"\t [{index + 1}] - ");

                        Console.WriteLine($"{ataquesAux[index].NomeAtq}.");
                    }

                    Console.Write("\n\t");
                    if (pagina > 1)
                    {
                        Console.Write("(1) - Página anterior <");
                    }

                    if (pagina != paginaMax)
                    {
                        Console.Write("|");
                        Console.WriteLine("Próxima página - (2)");
                    }

                    Console.Write("\nDescrição: ");
                    atqEscolhido[selecao - 1].Descricao();
                    Console.WriteLine();

                    char key = Console.ReadKey().KeyChar;

                    if ((key == 'W' || key == 'w') && selecao > 1)
                    {
                        selecao--;
                    }
                    else if ((key == 'S' || key == 's') && selecao < quantRealItens)
                    {
                        selecao++;
                    }
                    else if (pagina > 1 && key == '1')
                    {
                        pagina--;
                    }
                    else if (pagina != paginaMax && key == '2')
                    {
                        pagina++;
                    }
                    else if (key == '\r')
                    {
                        if (QntAtaquesNulo() == 4)
                        {
                            if (!MatrizContem(ataques, atqEscolhido[selecao - 1]))
                            {
                                ataques[0, 0] = atqEscolhido[selecao - 1];
                            }
                        }
                        else if (QntAtaquesNulo() == 3)
                        {
                            if (!MatrizContem(ataques, atqEscolhido[selecao - 1]))
                            {
                                ataques[1, 0] = atqEscolhido[selecao - 1];
                                break;
                            }
                        }
                        else if (QntAtaquesNulo() == 2)
                        {
                            if (!MatrizContem(ataques, atqEscolhido[selecao - 1]))
                            {
                                ataques[0, 1] = atqEscolhido[selecao - 1];
                            }
                        }
                        else
                        {
                            if (!MatrizContem(ataques, atqEscolhido[selecao - 1]))
                            {
                                ataques[1, 1] = atqEscolhido[selecao - 1];
                                break;
                            }
                        }
                    }
                } while (true);
            }
        }
    }
}
