﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGSoccer.Dominio.Dominio
{
    public class Localizacao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Localizacao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }
    }
}
