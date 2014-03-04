using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RPGSoccer.Dominio;
using RPGSoccer.Dominio.Dominio;
using RPGSoccer.Dominio.Exceptions;
using RPGSoccer.Dominio.Motor;
using RPGSoccer.Dominio.Motor.Resultados;
using RPGSoccer.Dominio.Utils;

namespace JogoTest
{
    [TestClass]
    public class PasseTest
    {
        [TestMethod]
        [ExpectedException(typeof(PasseInvalidoException),AllowDerivedTypes = false)]
        public void NaoConsigoPassarABolaPraUmJogadorDoOutroTime()
        {
            var player1 = new Jogador(Equipe.EquipeA,null);
            var player2 = new Jogador(Equipe.EquipeB, null);

            var motor = new Motor(null);
            motor.CalculaPasse(player1, player2, 2, AlturaPasse.Rasteiro, null);
        }


        [TestMethod]
        public void UmJogadorComOtimoPasseComSorteMaximaConseguePassarABolaPraOutroJogadorA1MetroPeloChaoSemAdversarios()
        {
            var atributosJogador = new AtributosJogador {Passe = 100};
            var player1 = new Jogador(Equipe.EquipeA,atributosJogador);
            var player2 = new Jogador(Equipe.EquipeA,atributosJogador);

            var sorte = new Mock<ICoeficienteSorte>();
            sorte.Setup(c => c.RodaODado()).Returns(100);
            var motor = new Motor(sorte.Object);
            IEnumerable<Jogador> adversariosNoCaminho = new List<Jogador>();

            ResultadoPasse resultadoPasse = motor.CalculaPasse(player1, player2, 1, AlturaPasse.Rasteiro, adversariosNoCaminho);

            Assert.IsTrue(resultadoPasse.Sucesso = true);
            Assert.IsTrue(resultadoPasse.DetentorDaPelota == player2);
        }

        [TestMethod]
        public void UmJogadorComOtimoPasseComSorteMaximaConseguePassarABolaPraOutroJogadorA100MetroPeloChaoSemAdversarios()
        {
            var atributosJogador = new AtributosJogador { Passe = 100 };
            var player1 = new Jogador(Equipe.EquipeA, atributosJogador);
            var player2 = new Jogador(Equipe.EquipeA, atributosJogador);

            var sorte = new Mock<ICoeficienteSorte>();
            sorte.Setup(c => c.RodaODado()).Returns(100);
            var motor = new Motor(sorte.Object);
            IEnumerable<Jogador> adversariosNoCaminho = new List<Jogador>();

            ResultadoPasse resultadoPasse = motor.CalculaPasse(player1, player2, 1, AlturaPasse.Rasteiro, adversariosNoCaminho);

            Assert.IsTrue(resultadoPasse.Sucesso = true);
            Assert.IsTrue(resultadoPasse.DetentorDaPelota == player2);
        }

        [TestMethod]
        public void UmJogadorComOtimoPasseComSorteMinimaConseguePassarABolaPraOutroJogadorA100MetroPeloChaoSemAdversarios()
        {
            var atributosJogador = new AtributosJogador { Passe = 100 };
            var player1 = new Jogador(Equipe.EquipeA, atributosJogador);
            var player2 = new Jogador(Equipe.EquipeA, atributosJogador);

            var sorte = new Mock<ICoeficienteSorte>();
            sorte.Setup(c => c.RodaODado()).Returns(1);
            var motor = new Motor(sorte.Object);
            IEnumerable<Jogador> adversariosNoCaminho = new List<Jogador>();

            ResultadoPasse resultadoPasse = motor.CalculaPasse(player1, player2, 1, AlturaPasse.Rasteiro, adversariosNoCaminho);

            Assert.IsTrue(resultadoPasse.Sucesso = true);
            Assert.IsTrue(resultadoPasse.DetentorDaPelota == player2);
        }


        [TestMethod]
        public void UmJogadorComPessimoPasseComSorteMaximaConseguePassarABolaPraOutroJogadorA100MetroPeloChaoSemAdversarios()
        {
            var atributosJogador = new AtributosJogador { Passe = 1 };
            var player1 = new Jogador(Equipe.EquipeA, atributosJogador);
            var player2 = new Jogador(Equipe.EquipeA, atributosJogador);

            var sorte = new Mock<ICoeficienteSorte>();
            sorte.Setup(c => c.RodaODado()).Returns(100);
            var motor = new Motor(sorte.Object);
            IEnumerable<Jogador> adversariosNoCaminho = new List<Jogador>();

            ResultadoPasse resultadoPasse = motor.CalculaPasse(player1, player2, 100, AlturaPasse.Rasteiro, adversariosNoCaminho);

            Assert.IsTrue(resultadoPasse.Sucesso = true);
            Assert.IsTrue(resultadoPasse.DetentorDaPelota == player2);
        }

        [TestMethod]
        public void UmJogadorComPessimoPasseComSorteQuaseMaximaNaoConseguePassarABolaPraOutroJogadorA100MetroPeloChaoSemAdversarios()
        {
            var atributosJogador = new AtributosJogador { Passe = 1 };
            var player1 = new Jogador(Equipe.EquipeA, atributosJogador);
            var player2 = new Jogador(Equipe.EquipeA, atributosJogador);

            var sorte = new Mock<ICoeficienteSorte>();
            sorte.Setup(c => c.RodaODado()).Returns(99);
            var motor = new Motor(sorte.Object);
            IEnumerable<Jogador> adversariosNoCaminho = new List<Jogador>();

            ResultadoPasse resultadoPasse = motor.CalculaPasse(player1, player2, 100, AlturaPasse.Rasteiro, adversariosNoCaminho);

            Assert.IsFalse(resultadoPasse.Sucesso);
        }



    }
}
