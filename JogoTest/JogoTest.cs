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

            var jogadoresEquipeACount = 0;
            var jogadoresEquipeBCount = 0;
            var bolas = 0;

            foreach (var espaçoNoCampo in jogo.Campo.SelectMany(linha => linha))
            {
                if (espaçoNoCampo.BolaEstaAqui)
                    bolas++;

                if(espaçoNoCampo.TipoConteudo == TipoConteudo.Jogador )
                    if (((Jogador) espaçoNoCampo.Conteudo).Equipe == Equipe.EquipeA)
                        jogadoresEquipeACount++;
                    else if( ((Jogador)espaçoNoCampo.Conteudo).Equipe == Equipe.EquipeB)
                        jogadoresEquipeBCount++;
            }

            Assert.AreEqual(6000,jogo.Campo.SelectMany(linha => linha).Count());
            Assert.AreEqual(11,jogadoresEquipeACount);
            Assert.AreEqual(11,jogadoresEquipeBCount);
            Assert.AreEqual(1,bolas);
        }
    }
}
