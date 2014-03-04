using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGSoccer.Dominio.Dominio
{
    public class Jogador : OcupaEspaço
    {
        public Equipe Equipe { get; private set; }
        public AtributosJogador AtributosJogador { get; private set; }
        public int Numero { get; set; }

        public Localizacao Localizacao { get; set; }

        public Jogador(Equipe equipe, AtributosJogador atributosJogador)
        {
            Equipe = equipe;
            AtributosJogador = atributosJogador;
        }

    
    }

}
