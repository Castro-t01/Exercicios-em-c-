// mini rpg feito em c#
//Nomeado de PHOENIX ARENA!
using System;

class Program
{
 static void Main()
 {
 
Personagem jogador = new();
 jogador.vida = 100;
Inimigo inimigo = new();
 inimigo.vida = 75;

Terminal.BemVindo(jogador, inimigo);
Terminal.Regras(jogador, inimigo);
Utils.EscreverColorido("!INFO! Você deseja saber como funciona as ações do personagem? S/N ", ConsoleColor.DarkMagenta);
string info = Console.ReadLine().Trim().ToUpper();
if(info == "S")
        {
            Terminal.Info();
        }

int contador = 0;
int escolha;

//------------------------ RODADAS ------------------------
while(jogador.vida > 0 && inimigo.vida > 0)
        {
            contador++;
Utils.EscreverColorido($"\nEstamos na rodada {contador} e a vez é do jogador: ", ConsoleColor.Yellow);
// TURNO DO JOGADOR
Console.WriteLine($"Jogador ({jogador.nome}), você deseja:");
Console.WriteLine("1.Atacar");
Console.WriteLine("2.Habilidade: SIXSEVEN");
Console.WriteLine("3.Curar");
Console.WriteLine("4.Fugir");
if (!int.TryParse(Console.ReadLine(), out escolha))
{
    Console.WriteLine("Número inválido!");
    contador--;
    continue;
}
Utils.EscreverColorido("-----------------------", ConsoleColor.DarkMagenta);
            switch (escolha)
            {
            case 1:
            jogador.Atacar(inimigo);
            break; 
            case 2:
            jogador.Stun(inimigo);
            break;
            case 3:
            jogador.Curar();
            break;
            case 4:
            Console.WriteLine("Você fugiu!"); 
            Environment.Exit(0);
            break;
            default:
            Console.WriteLine("Escolha nao reconhecida!");
            continue;
                
            }
// CHECAR VIDA INIMIGO
if(inimigo.vida <= 0)
            {
                Console.WriteLine($"Você ganhou!!!! Matou o seu {inimigo.nome}");
                Environment.Exit(0);
            }
// VEZ DO INIMIGO
Utils.EscreverColorido("-----------------------", ConsoleColor.DarkMagenta);
    if (inimigo.stunRoud <= 0)
{
    Utils.EscreverColorido($"Vez do {inimigo.nome}",ConsoleColor.Yellow);
    inimigo.Atacar(jogador);
}
//CALC DE STUN
else
{
    inimigo.stunRoud--;

    Console.WriteLine("O Inimigo está stunado e não irá jogar esse round!");

    if (inimigo.stunRoud > 0)
    {
        Console.WriteLine($"Rounds de stun: {inimigo.stunRoud}");
    }
    else
    {
        Console.WriteLine("O inimigo não está mais sentindo vergonha e voltará ao normal na proxima rodada!");
    }
}

// CHEGAR VIDA DO PERSONAGEM
if(jogador.vida <= 0)
            {

                Console.WriteLine($"Você perdeuu!!!! Morreu para o seu {inimigo.nome}");
                Environment.Exit(0);
            }
//STATUS DO GAME
                Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine("STATUS:");
                Console.ResetColor();
            Console.WriteLine($"Vida do Jogador: {Terminal.BarraDeVida(jogador.vida, 100) } ");
            Console.WriteLine($"Vida do Inimigo: {Terminal.BarraDeVida(inimigo.vida, 75) } ");
                Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                Console.ResetColor();
 }   
}
}
class Personagem
{
    public int vida;
    public string nome;
    public int curaQtd = 5;

    public void Atacar(Inimigo inimigo)
    {
        bool acertou;
        if(inimigo.stunRoud > 0)
        {
            acertou = true;
        }
        else
        {
            acertou = Utils.random.Next(100) < 50;
        }
        if(acertou)
        {
            Utils.EscreverColorido("Você acertou o inimigo!", ConsoleColor.Green);
            int dano = Utils.random.Next(5,26);
            Console.Write("O dano no inimigo foi de ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"{dano}\n");
            Console.ResetColor();
            inimigo.vida -= dano;
            
        }
        else
        {
            Utils.EscreverColorido("O inimigo desviou do golpe!", ConsoleColor.DarkCyan);
        }
    }
    public void Stun(Inimigo inimigo)
    {
        bool acerto = Utils.random.Next(100) < 30;
            if (acerto)
                {
                    Utils.EscreverColorido("Você acertou a sua habilidade", ConsoleColor.Green);
                    Console.WriteLine("SIX SEVENNNN");
                    Utils.EscreverColorido("O inimigo sentiu tanta vergonha de você, que ele irá ficar stunado por 2 rounds, sem poder atacar!", ConsoleColor.Green);
                    inimigo.stunRoud = 3;

                }
            else
                {
                    Utils.EscreverColorido("Você errou a habilidade!", ConsoleColor.DarkCyan);
                }
    }
    public void Curar()
    {
        int cura = 10;

        if(curaQtd <= 0)
        {
            Utils.EscreverColorido("Você não tem mais curas restantes!", ConsoleColor.Gray);
        }
        else{
            vida += cura;
        if (vida > 100)
        {
            vida = 100;
        }
        curaQtd--;
        Utils.EscreverColorido($"Você curou {cura} de vida!", ConsoleColor.DarkGreen);
        Console.Write("Você tem ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write($"{curaQtd} ");
        Console.ResetColor();
        Console.WriteLine("poções de vida restantes!");
        }
    }
}
class Inimigo
{
    public int vida;
    public string nome;
    public int stunRoud;

    public void Atacar(Personagem jogador)
    {
    
        bool acertou = Utils.random.Next(100) < 50;

    if(acertou)
        {
            Utils.EscreverColorido("O inimigo te acertou!", ConsoleColor.Red);
            int dano = Utils.random.Next(1,27);
            bool critico = Utils.random.Next(100) < 15;
            if (critico)
            {
                dano *= 2;
                Utils.EscreverColorido("BOLADAAAAAAAAAAAAAAAA! -- disse seu inimigo", ConsoleColor.DarkRed);
                Utils.EscreverColorido("CRÍTICOOOOOOO!", ConsoleColor.DarkMagenta);
            }
            else
            {
                Utils.EscreverColorido("PAULADAAAAAAAAAAAAAAAA! -- disse seu inimigo", ConsoleColor.Red);
            }
            Console.Write("O dano do inimigo foi de ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"{dano}\n");
            Console.ResetColor();
            jogador.vida -= dano;
            
        }
        else
        {
            Utils.EscreverColorido("O inimigo errou o ataque!", ConsoleColor.Cyan);
        }
    }

}
static class Terminal
{
    

    public static void BemVindo(Personagem jogador, Inimigo inimigo)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Bem vindo ao MINI RPG em C#, o *PHOENIX ARENA*!");
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        Console.Write("Para começar, digite o nome do seu personagem: ");
        Console.ResetColor();
        jogador.nome = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(jogador.nome) == true)
        {
            Console.WriteLine("Erro! Nome não reconhecido.");
            Environment.Exit(0);
        }
        Console.WriteLine($"Bem vindo {jogador.nome}, você deve escolher seu inimigo: ");

        Console.WriteLine("Inimigo 1: Goblin");
        Console.WriteLine("Inimigo 2: Esqueleto");
        Console.WriteLine("Inimigo 3: Anão");
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        int escolha = Convert.ToInt32(Console.ReadLine());
        switch (escolha)
        {
            case 1: inimigo.nome = "Goblin";
            break;
            case 2: inimigo.nome = "Esqueleto";
            break;
            case 3: inimigo.nome = "Anão";
            break;
            default: Console.WriteLine("Inimigo não conhecido!");
            break;
        }
    }
    public static void Regras(Personagem jogador, Inimigo inimigo)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n");
        Console.WriteLine("-=-=-=-=-= Regras -=-=-=-=-=-=");
        Console.WriteLine($"1. O Jogador({jogador.nome}) é o primeiro turno, e irá seguindo uma ordem de alternação.");
        Console.WriteLine($"2. O jogo só acaba quando o jogador({jogador.nome}) ou o inimigo({inimigo.nome}) morrer, sendo a vitória, de quem está vivo!");
        Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
        Console.ResetColor();
    }
    public static void Info()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("-=-=-=-=-= Informações -=-=-=-=-=-=");
        Console.WriteLine("O dano do personagem funciona sendo um sorteio entre 5-25, e tem a chance de 50% de acertar o ataque.");
        Console.WriteLine("O Stun(SIX SEVEN) quando é acertado(30%) causa stun por 2 rounds a seguinte do que foi usado, sendo garantido 100% de acerto de ataque e impedindo o inimigo de atacar.");
        Console.WriteLine("Você possui 5 unidades de cura, e elas curam 10 de vida, apos acabar, não da mais para usar-las!");
        Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
        Console.ResetColor();
    }
    public static string BarraDeVida(int vidaAtual, int vidaMaxima)
{
    int tamanhoBarra = 10; // quantidade de bloquinhos
    int vidaCalculada = vidaAtual;

    if (vidaCalculada < 0)
        vidaCalculada = 0;

    int preenchido = (vidaCalculada * tamanhoBarra) / vidaMaxima;
    int vazio = tamanhoBarra - preenchido;

    string barra = new string('█', preenchido) + new string('░', vazio);

    return $"[{barra}] {vidaCalculada}/{vidaMaxima}";
}
}
static class Utils
{
     public static Random random = new();
    public static void EscreverColorido(string texto, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine(texto);
        Console.ResetColor();
    }
}
