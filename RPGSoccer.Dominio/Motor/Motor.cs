using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGSoccer.Dominio.Dominio;
using RPGSoccer.Dominio.Exceptions;
using RPGSoccer.Dominio.Motor.Resultados;
using RPGSoccer.Dominio.Utils;

namespace RPGSoccer.Dominio.Motor
{
    public class Motor
    {
        public ICoeficienteSorte Sorte { get; private set; }

        public Motor(ICoeficienteSorte sorte)
        {
            Sorte = sorte;
        }

        public ResultadoPasse CalculaPasse(Jogador origem, Jogador destino, int distancia, AlturaPasse alturaPasse, IEnumerable<Jogador> adversariosNoCaminho)
        {
            if(origem.Equipe != destino.Equipe)
                throw new PasseInvalidoException();

            int coeficienteDificuldade = CalculaCoeficienteDificuldadePasse(distancia, alturaPasse, adversariosNoCaminho);
            var sorte = Sorte.RodaODado();

            if(sorte + origem.AtributosJogador.Passe > coeficienteDificuldade)
                return new ResultadoPasse() {DetentorDaPelota = destino, Sucesso = true};

            return new ResultadoPasse() {Sucesso = false};

        }

        private int CalculaCoeficienteDificuldadePasse(int distancia, AlturaPasse alturaPasse, IEnumerable<Jogador> adversariosNoCaminho)
        {
            var coeficiente = 0;

            coeficiente += distancia; 
            return coeficiente;
        }
    }
}
