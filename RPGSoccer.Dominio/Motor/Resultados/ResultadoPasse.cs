using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGSoccer.Dominio.Dominio;

namespace RPGSoccer.Dominio.Motor.Resultados
{
    public class ResultadoPasse
    {
        public bool Sucesso { get; set; }
        public Jogador DetentorDaPelota { get; set; }
    }
}
