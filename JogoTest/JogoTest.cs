using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGSoccer.Dominio;
using RPGSoccer.Dominio.Dominio;
using RPGSoccer.Dominio.Exceptions;

namespace JogoTest
{
    [TestClass]
    public class JogoTest
    {
        [TestMethod]
        [ExpectedException(typeof(TimeInvalidoException))]
        public void NaoConsigoCriarJogoSeTimeANaoTiverJogadoresDe1A11()
        {
            
            var timeA = Gera11Jogadores(Equipe.EquipeA).ToList();
            var jogadorASeAlterar = new Random().Next(1, 11);
            timeA.ElementAt(jogadorASeAlterar).Numero = 30;  // Altera o numero de algum jogador aleatorio (ele vira o Kleber =D )
            var timeB = Gera11Jogadores(Equipe.EquipeB).ToList();

            new Jogo(timeA, EsquemaTatico.QuatroQuatroDois, timeB, EsquemaTatico.QuatroQuatroDois,
                Equipe.EquipeA);
        }

        private static IEnumerable<Jogador> Gera11Jogadores(Equipe equipe)
        {
            var retorno = new List<Jogador>();
            for (var i = 1; i < 12; i++)
            {
                retorno.Add(new Jogador(equipe,new AtributosJogador()){Numero = i});
            }
            return retorno;
        }

        [TestMethod]
        [ExpectedException(typeof (TimeInvalidoException))]
        public void NaoConsigoCriarJogoSeTimeBNaoTiverJogadoresDe1A11()
        {
            var timeA = Gera11Jogadores(Equipe.EquipeA).ToList();
            var timeB = Gera11Jogadores(Equipe.EquipeB).ToList();
            var jogadorASeAlterar = new Random().Next(1, 11);
            timeB.ElementAt(jogadorASeAlterar).Numero = 30;  // Altera o numero de algum jogador aleatorio (ele vira o Kleber =D )


            new Jogo(timeA, EsquemaTatico.QuatroQuatroDois, timeB, EsquemaTatico.QuatroQuatroDois,
                Equipe.EquipeA);
        }

        [TestMethod]
        [ExpectedException(typeof (TimeInvalidoException))]
        public void NaoConsigoCriarJogoSeTimeANaoTiver11Jogadores()
        {
            var timeA = Gera11Jogadores(Equipe.EquipeA).ToList();
            var timeB = Gera11Jogadores(Equipe.EquipeB).ToList();
            var jogadorASeRemover = new Random().Next(1, 11);
            timeA.RemoveAt(jogadorASeRemover);


            new Jogo(timeA, EsquemaTatico.QuatroQuatroDois, timeB, EsquemaTatico.QuatroQuatroDois,
                Equipe.EquipeA);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeInvalidoException))]
        public void NaoConsigoCriarJogoSeTimeBNaoTiver11Jogadores()
        {
            var timeA = Gera11Jogadores(Equipe.EquipeA).ToList();
            var timeB = Gera11Jogadores(Equipe.EquipeB).ToList();
            var jogadorASeRemover = new Random().Next(1, 11);
            timeB.RemoveAt(jogadorASeRemover);

            new Jogo(timeA, EsquemaTatico.QuatroQuatroDois, timeB, EsquemaTatico.QuatroQuatroDois,
                Equipe.EquipeA);
        }

        [TestMethod]
        public void ConsigoCriarUmJogoPadraoCom22JogadoresNoCampo()
        {
            var timeA = Gera11Jogadores(Equipe.EquipeA).ToList();
            var timeB = Gera11Jogadores(Equipe.EquipeB).ToList();

            var jogo = new Jogo(timeA, EsquemaTatico.QuatroQuatroDois, timeB, EsquemaTatico.QuatroQuatroDois,
                Equipe.EquipeA);


       

            Assert.IsTrue(jogo.JogadoresA.All(j => j.Localizacao != null));
            Assert.IsTrue(jogo.JogadoresB.All(j => j.Localizacao != null));
            Assert.IsTrue(jogo.Bola.Localizacao != null);
        }
    }
}
