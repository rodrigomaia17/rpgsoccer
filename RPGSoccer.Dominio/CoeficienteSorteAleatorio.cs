using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGSoccer.Dominio.Utils;

namespace RPGSoccer.Dominio
{
    public class CoeficienteSorteAleatorio : ICoeficienteSorte
    {
        private Random _d100;


        public CoeficienteSorteAleatorio()
        {
            _d100 = new Random();

        }

        public int RodaODado()
        {
            return _d100.Next(1, 100);
        }
    }
}
