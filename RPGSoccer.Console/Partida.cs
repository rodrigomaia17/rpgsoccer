using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using RPGSoccer.Dominio;
using RPGSoccer.Dominio.Dominio;
using RPGSoccer.Dominio.Parametros;
using RPGSoccer.Dominio.Utils;

namespace RPGSoccer.Console
{
    public class Partida
    {
        private readonly Jogo _jogo;

        public Partida()
        {
            var jogA = Gera11Jogadores(Equipe.EquipeA).ToList();
            var jogB = Gera11Jogadores(Equipe.EquipeB).ToList();

            _jogo = new Jogo(jogA, EsquemaTatico.QuatroQuatroDois, jogB, EsquemaTatico.QuatroQuatroDois,
                Equipe.EquipeA);

          
        }

        private void NovaRodada(Equipe vez)
        {
            var continuar = true;

            while (continuar)
            {
                _jogo.DesenhaCampo();
                var opcoes = GeraOpcoesMenu(_jogo.CalculaOpcoesDisponiveis()).ToList();
                ImprimeMenu();
                ImprimeOpcoes(opcoes);
                var opcaoEscolhida = EscolheOpcao(opcoes);
                RealizaAcao(opcaoEscolhida);
                if (opcaoEscolhida == OpcaoDeJogo.Sair)
                {
                    continuar = false;
                }
            }
            
        }

        private void RealizaAcao(OpcaoDeJogo opcaoEscolhida)
        {
            switch (opcaoEscolhida)
            {
                case OpcaoDeJogo.Passe:
                    AcaoPasse();
                    break;
                case OpcaoDeJogo.Sair:
                    _jogo.AcaoSair();
                    break;
                case OpcaoDeJogo.Nada:
                    _jogo.AcaoNada();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("opcaoEscolhida");
            }
        }

        private void AcaoPasse()
        {
            ParametrosPasse parametrosPasse = EscolheOpcaoPasse();
            _jogo.AcaoPasse(parametrosPasse);

        }

        private ParametrosPasse EscolheOpcaoPasse()
        {
            System.Console.WriteLine("Para quem você deseja Passar a bola? (digite o numero do jogador)\n");
            var retorno = new ParametrosPasse();
            int entrada;
            do
            {
                entrada = System.Console.Read();
                if (entrada > 0 && entrada < 12 )
                {
                    retorno.JogadorDestino = entrada;
                }
                else
                {
                    System.Console.WriteLine("Opcao Invalida");
                }

            } while (entrada < 1 || entrada > 11);

            System.Console.WriteLine("\nComo Você deseja que seja o passe?");
            System.Console.WriteLine("1 - Rasteiro, 2 - Meia Altura, 3 - Pelo Alto");
            do
            {
                entrada = System.Console.Read();
                if (entrada > 0 && entrada < 4)
                {
                    if(entrada == 1)
                        retorno.Altura = AlturaPasse.Rasteiro;
                    else if ( entrada == 2)
                        retorno.Altura = AlturaPasse.MeiaAltura;
                    else if ( entrada == 3)
                        retorno.Altura = AlturaPasse.Alto;
                }
                else
                {
                    System.Console.WriteLine("Opcao Invalida");
                }

            } while (entrada < 1 || entrada > 3);

            return retorno;
        }

        private OpcaoDeJogo EscolheOpcao(IList<KeyValuePair<int, OpcaoDeJogo>> opcoesDisponiveis)
        {
            var retorno = OpcaoDeJogo.Sair;
            int entrada;
            do
            {
                entrada = System.Console.Read();
                if (opcoesDisponiveis.Any(o => o.Key == entrada))
                {
                    retorno = opcoesDisponiveis.First(o => o.Key == entrada).Value;
                }
                else
                {
                    System.Console.WriteLine("Opcao Invalida");
                }
                
            } while (opcoesDisponiveis.All(o => o.Key != entrada));

            return retorno;
        }

        private void ImprimeMenu()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("O Que você Deseja Fazer? ");

        }

        private void ImprimeOpcoes(IEnumerable<KeyValuePair<int, OpcaoDeJogo>> opcoesDeJogo)
        {
            foreach (var opcao in opcoesDeJogo)
            {
                var texto = opcao.Key + " - ";
                if (opcao.Value == OpcaoDeJogo.Passe)
                    texto += "Passe";
                else if (opcao.Value == OpcaoDeJogo.Nada)
                    texto += "Nada";
                else if (opcao.Value == OpcaoDeJogo.Sair)
                    texto += "Sair";

                System.Console.WriteLine(texto+"\n");
            }
        }

        private static IEnumerable<KeyValuePair<int, OpcaoDeJogo>> GeraOpcoesMenu(IEnumerable<OpcaoDeJogo> opcoesDisponiveis)
        {
            var retorno = opcoesDisponiveis.Select((o, i) => new {Opcao = o, Index = i+1}).ToDictionary(opcao => opcao.Index, opcao => opcao.Opcao);
            return retorno;
        }

        private static IEnumerable<Jogador> Gera11Jogadores(Equipe equipe)
        {
            var retorno = new List<Jogador>();
            for (var i = 1; i < 12; i++)
            {
                retorno.Add(new Jogador(equipe, new AtributosJogador()) { Numero = i });
            }

            return retorno;
        }
    }
}
