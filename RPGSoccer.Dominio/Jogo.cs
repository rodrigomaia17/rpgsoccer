using System.Collections.Generic;
using System.Linq;
using RPGSoccer.Dominio.Dominio;
using RPGSoccer.Dominio.Exceptions;
using RPGSoccer.Dominio.Motor.Resultados;
using RPGSoccer.Dominio.Parametros;
using RPGSoccer.Dominio.Utils;

namespace RPGSoccer.Dominio
{
    public class Jogo
    {
        public Bola Bola { get; set; }
        private readonly EsquemaTatico _esquemaA;
        private readonly EsquemaTatico _esquemaB;
        public IList<Jogador> JogadoresA { get; private set; }
        public IList<Jogador> JogadoresB { get; private set; }

        private readonly Motor.Motor _motor;
        private readonly Equipe _posseDeBola;
        private Equipe _equipeDaVez;
        private ICoeficienteSorte _sorte;

        //public IList<IList<EspaçoNoCampo>> Campo { get; set; }  


        public Jogo(IList<Jogador> jogadoresA, EsquemaTatico esquemaA, IList<Jogador> jogadoresB, EsquemaTatico esquemaB,
            Equipe posseDeBolaInicial)
        {
            ConfereSeTemosJogadoresDe1A11(jogadoresA);
            ConfereSeTemosJogadoresDe1A11(jogadoresB);

            JogadoresA = jogadoresA;
            JogadoresB = jogadoresB;

            _esquemaA = esquemaA;
            _esquemaB = esquemaB;

            _posseDeBola = posseDeBolaInicial;
            _equipeDaVez = posseDeBolaInicial;

            Bola = new Bola();

            //Campo = GeraCampo();
            PosicionaJogadores();

            _sorte = new CoeficienteSorteAleatorio();
            _motor = new Motor.Motor(_sorte);
        }

        private void PosicionaJogadores()
        {
            PosicionaEquipeA();
            PosicionaEquipeB();
        }

        private void PosicionaEquipeB()
        {
            if (_esquemaB == EsquemaTatico.QuatroQuatroDois)
            {
                JogadoresB.First(c => c.Numero == 1).Localizacao = new Localizacao(30, 100);

                JogadoresB.First(c => c.Numero == 2).Localizacao = new Localizacao(57, 90);
                JogadoresB.First(c => c.Numero == 3).Localizacao = new Localizacao(37, 90);
                JogadoresB.First(c => c.Numero == 4).Localizacao = new Localizacao(23, 90);
                JogadoresB.First(c => c.Numero == 6).Localizacao = new Localizacao(3, 90);
                JogadoresB.First(c => c.Numero == 7).Localizacao = new Localizacao(57, 75);
                JogadoresB.First(c => c.Numero == 8).Localizacao = new Localizacao(37, 75);
                JogadoresB.First(c => c.Numero == 5).Localizacao = new Localizacao(23, 75);
                JogadoresB.First(c => c.Numero == 10).Localizacao = new Localizacao(3, 75);
                if (_posseDeBola == Equipe.EquipeB)
                {
                    JogadoresB.First(c => c.Numero == 9).Localizacao = new Localizacao(28, 50);
                    Bola.Localizacao = new Localizacao(28, 50);
                    JogadoresB.First(c => c.Numero == 11).Localizacao = new Localizacao(32, 50);
                }
                else
                {
                    JogadoresB.First(c => c.Numero == 9).Localizacao = new Localizacao(28, 65);
                    JogadoresB.First(c => c.Numero == 11).Localizacao = new Localizacao(32, 65);
                }
            }
        }

        private void PosicionaEquipeA()
        {
            if (_esquemaA == EsquemaTatico.QuatroQuatroDois)
            {
                JogadoresA.First(c => c.Numero == 1).Localizacao = new Localizacao(30, 1);
                JogadoresA.First(c => c.Numero == 2).Localizacao = new Localizacao(57, 10);
                JogadoresA.First(c => c.Numero == 3).Localizacao = new Localizacao(37, 10);
                JogadoresA.First(c => c.Numero == 4).Localizacao = new Localizacao(23, 10);
                JogadoresA.First(c => c.Numero == 6).Localizacao = new Localizacao(3, 10);
                JogadoresA.First(c => c.Numero == 7).Localizacao = new Localizacao(57, 25);
                JogadoresA.First(c => c.Numero == 8).Localizacao = new Localizacao(37, 25);
                JogadoresA.First(c => c.Numero == 5).Localizacao = new Localizacao(23, 25);
                JogadoresA.First(c => c.Numero == 10).Localizacao = new Localizacao(3, 25);
                if (_posseDeBola == Equipe.EquipeA)
                {
                    JogadoresA.First(c => c.Numero == 9).Localizacao = new Localizacao(28, 50);
                    Bola.Localizacao = new Localizacao(28, 50);
                    JogadoresA.First(c => c.Numero == 11).Localizacao = new Localizacao(32, 50);
                }
                else
                {
                    JogadoresA.First(c => c.Numero == 9).Localizacao = new Localizacao(28, 35);
                    JogadoresA.First(c => c.Numero == 11).Localizacao = new Localizacao(32, 35);
                }
            }
        }

        private IList<IList<EspaçoNoCampo>> GeraCampo()
        {
            //Convencionamos que o campo é composto por uma matriz 10 por 6 (como se fosse 100 metros por 60 de largura)
            var retorno = new List<IList<EspaçoNoCampo>>();
            for (int i = 0; i < 60; i++)
            {
                var linha = new List<EspaçoNoCampo>();
                for (int j = 0; j < 100; j++)
                {
                    linha.Add(new EspaçoNoCampo());
                }
                retorno.Add(linha);
            }

            return retorno;
        }

        private void ConfereSeTemosJogadoresDe1A11(IEnumerable<Jogador> time)
        {
            IList<Jogador> equipe = time as IList<Jogador> ?? time.ToList();
            if (equipe.Count() != 11)
                throw new TimeInvalidoException("Numero de Jogadores Incorretos");
            if (equipe.Select((x, i) => new {Value = x, Index = i + 1}).Any(it => it.Value.Numero != it.Index))
            {
                throw new TimeInvalidoException("Numeração Incorreta");
            }
        }

        public IEnumerable<OpcaoDeJogo> CalculaOpcoesDisponiveis()
        {
            var retorno = new List<OpcaoDeJogo>();

            if (_equipeDaVez == _posseDeBola)
                retorno.Add(OpcaoDeJogo.Passe);

            retorno.Add(OpcaoDeJogo.Nada);
            retorno.Add(OpcaoDeJogo.Sair);

            return retorno;
        }

        private void PassaAVez()
        {
            _equipeDaVez = _equipeDaVez == Equipe.EquipeA ? Equipe.EquipeB : Equipe.EquipeA;
        }

        public void AcaoNada()
        {
            PassaAVez();
        }

        public void AcaoSair()
        {
        }

        public ResultadoPasse AcaoPasse(ParametrosPasse parametrosPasse)
        {
            Jogador jogadorComABola = BuscaJogadorComABola();
            Jogador jogadorDestino = BuscaJogadorDestinoPasse(parametrosPasse.JogadorDestino);
            var retorno =  _motor.CalculaPasse(jogadorComABola, jogadorDestino, Bola.Localizacao.DistanceTo(jogadorDestino.Localizacao), parametrosPasse.Altura, null);
            if (retorno.Sucesso)
                Bola.Localizacao = retorno.DetentorDaPelota.Localizacao;
            return retorno;
        }

        private Jogador BuscaJogadorComABola()
        {
            IList<Jogador> timeAProcurar = _equipeDaVez == Equipe.EquipeA ? JogadoresA : JogadoresB;
            Jogador retorno = null;

            retorno = timeAProcurar.FirstOrDefault(c => c.Localizacao.Equals(Bola.Localizacao));

            return retorno;
        }

        private Jogador BuscaJogadorDestinoPasse(int jogadorDestino)
        {
            IList<Jogador> timeAProcurar = _equipeDaVez == Equipe.EquipeA ? JogadoresA : JogadoresB;
            return timeAProcurar.First(c => c.Numero == jogadorDestino);
        }
    }
}