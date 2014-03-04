using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGSoccer.Dominio.Dominio;
using RPGSoccer.Dominio.Exceptions;
using RPGSoccer.Dominio.Parametros;
using RPGSoccer.Dominio.Utils;

namespace RPGSoccer.Dominio
{
    public class Jogo
    {
        private readonly IList<Jogador> _jogadoresA;
        private readonly EsquemaTatico _esquemaA;
        private readonly IList<Jogador> _jogadoresB;
        private readonly EsquemaTatico _esquemaB;
        private readonly Equipe _posseDeBola;
        private Equipe _equipeDaVez;
        private ICoeficienteSorte _sorte;
        private Motor.Motor _motor;

        //public IList<IList<EspaçoNoCampo>> Campo { get; set; }  


        public Jogo(IList<Jogador> jogadoresA,EsquemaTatico esquemaA,IList<Jogador> jogadoresB,EsquemaTatico esquemaB,Equipe posseDeBolaInicial  )
        {
            ConfereSeTemosJogadoresDe1A11(jogadoresA);
            ConfereSeTemosJogadoresDe1A11(jogadoresB);

            _jogadoresA = jogadoresA;
            _jogadoresB = jogadoresB;

            _esquemaA = esquemaA;
            _esquemaB = esquemaB;

            _posseDeBola = posseDeBolaInicial;
            _equipeDaVez = posseDeBolaInicial;

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
            _jogadoresB.First(c => c.Numero == 1).Localizacao = new Localizacao(30,99);

            Campo.ElementAt(57).ElementAt(90).Conteudo = _jogadoresB.First(c => c.Numero == 2).Localizacao = new Localizacao(30, 99);
            Campo.ElementAt(37).ElementAt(90).Conteudo = _jogadoresB.First(c => c.Numero == 3).Localizacao = new Localizacao(30, 99);
            Campo.ElementAt(23).ElementAt(90).Conteudo = _jogadoresB.First(c => c.Numero == 4).Localizacao = new Localizacao(30, 99);
            Campo.ElementAt(3).ElementAt(90).Conteudo = _jogadoresB.First(c => c.Numero == 6).Localizacao = new Localizacao(30, 99);
            Campo.ElementAt(57).ElementAt(75).Conteudo = _jogadoresB.First(c => c.Numero == 7).Localizacao = new Localizacao(30, 99);
            Campo.ElementAt(37).ElementAt(75).Conteudo = _jogadoresB.First(c => c.Numero == 8).Localizacao = new Localizacao(30, 99);
            Campo.ElementAt(23).ElementAt(75).Conteudo = _jogadoresB.First(c => c.Numero == 5).Localizacao = new Localizacao(30, 99);
            Campo.ElementAt(3).ElementAt(75).Conteudo = _jogadoresB.First(c => c.Numero == 10).Localizacao = new Localizacao(30, 99);
            if (_posseDeBola == Equipe.EquipeB)
            {
                Campo.ElementAt(28).ElementAt(50).Conteudo = _jogadoresB.First(c => c.Numero == 9).Localizacao = new Localizacao(30, 99);
                Campo.ElementAt(28).ElementAt(50).BolaEstaAqui = true;
                Campo.ElementAt(32).ElementAt(50).Conteudo = _jogadoresB.First(c => c.Numero == 11.Localizacao = new Localizacao(30, 99));
            }
            else
            {
                Campo.ElementAt(28).ElementAt(65).Conteudo = _jogadoresB.First(c => c.Numero == 9).Localizacao = new Localizacao(30, 99);
                Campo.ElementAt(32).ElementAt(65).Conteudo = _jogadoresB.First(c => c.Numero == 11).Localizacao = new Localizacao(30, 99);
            }
        }

        private void PosicionaEquipeA()
        {
            if (_esquemaA == EsquemaTatico.QuatroQuatroDois)
            {
                Campo.ElementAt(30).ElementAt(1).Conteudo = _jogadoresA.First(c => c.Numero == 1).Localizacao = new Localizacao(30, 99);
                Campo.ElementAt(57).ElementAt(10).Conteudo = _jogadoresA.First(c => c.Numero == 2).Localizacao = new Localizacao(30, 99);
                Campo.ElementAt(37).ElementAt(10).Conteudo = _jogadoresA.First(c => c.Numero == 3).Localizacao = new Localizacao(30, 99);
                Campo.ElementAt(23).ElementAt(10).Conteudo = _jogadoresA.First(c => c.Numero == 4).Localizacao = new Localizacao(30, 99);
                Campo.ElementAt(3).ElementAt(10).Conteudo = _jogadoresA.First(c => c.Numero == 6).Localizacao = new Localizacao(30, 99);
                Campo.ElementAt(57).ElementAt(25).Conteudo = _jogadoresA.First(c => c.Numero == 7).Localizacao = new Localizacao(30, 99);
                Campo.ElementAt(37).ElementAt(25).Conteudo = _jogadoresA.First(c => c.Numero == 8).Localizacao = new Localizacao(30, 99);
                Campo.ElementAt(23).ElementAt(25).Conteudo = _jogadoresA.First(c => c.Numero == 5).Localizacao = new Localizacao(30, 99);
                Campo.ElementAt(3).ElementAt(25).Conteudo = _jogadoresA.First(c => c.Numero == 10).Localizacao = new Localizacao(30, 99);
                if (_posseDeBola == Equipe.EquipeA)
                {
                    Campo.ElementAt(28).ElementAt(50).Conteudo = _jogadoresA.First(c => c.Numero == 9);
                    Campo.ElementAt(28).ElementAt(50).BolaEstaAqui = true;
                    Campo.ElementAt(32).ElementAt(50).Conteudo = _jogadoresA.First(c => c.Numero == 11);
                }
                else
                {
                    Campo.ElementAt(28).ElementAt(35).Conteudo = _jogadoresA.First(c => c.Numero == 9);
                    Campo.ElementAt(32).ElementAt(35).Conteudo = _jogadoresA.First(c => c.Numero == 11);
                }
            }
        }

        private IList<IList<EspaçoNoCampo>> GeraCampo()
        {
            //Convencionamos que o campo é composto por uma matriz 10 por 6 (como se fosse 100 metros por 60 de largura)
            var retorno = new List<IList<EspaçoNoCampo>>();
            for(var i = 0; i < 60 ; i++)
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
            var equipe = time as IList<Jogador> ?? time.ToList();
            if(equipe.Count() != 11)
                throw new TimeInvalidoException("Numero de Jogadores Incorretos");
            if (equipe.Select((x,i) => new { Value = x, Index = i+1}).Any(it => it.Value.Numero != it.Index))
            {
                throw new TimeInvalidoException("Numeração Incorreta");
            }
        }

        public IEnumerable<OpcaoDeJogo> CalculaOpcoesDisponiveis()
        {
            var retorno = new List<OpcaoDeJogo>();

            if(_equipeDaVez == _posseDeBola)
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

        public void AcaoPasse(ParametrosPasse parametrosPasse)
        {
            Jogador jogadorComABola = BuscaJogadorComABola();
            Jogador jogadorDestino = BuscaJogadorDestinoPasse(parametrosPasse.JogadorDestino);
            return _motor.CalculaPasse(jogadorComABola, jogadorDestino, , parametrosPasse.Altura, null);


        }

        private Jogador BuscaJogadorComABola()
        {
            var timeAProcurar = _equipeDaVez == Equipe.EquipeA ? _jogadoresA : _jogadoresB;
            Jogador retorno = null;
            foreach (var espaco in from linha in Campo from espaco in linha where espaco.BolaEstaAqui && espaco.TipoConteudo == TipoConteudo.Jogador select espaco)
            {
                retorno = (Jogador) espaco.Conteudo;
            }
            return retorno;
        }

        private Jogador BuscaJogadorDestinoPasse(int jogadorDestino)
        {
            var timeAProcurar = _equipeDaVez == Equipe.EquipeA ? _jogadoresA : _jogadoresB;
            return timeAProcurar.First(c => c.Numero == jogadorDestino);
        }
    }
}
