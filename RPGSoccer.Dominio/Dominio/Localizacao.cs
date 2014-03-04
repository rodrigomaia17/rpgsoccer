using System;
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

        public int DistanceTo(Localizacao point2)
        {
            var a = (point2.Linha - Linha);
            var b = (point2.Coluna - Coluna);

            return (int) Math.Sqrt(a * a + b * b);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Localizacao p = obj as Localizacao;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Linha == p.Linha) && (Coluna == p.Coluna);
        }

        public bool Equals(Localizacao p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Linha == p.Linha) && (Coluna == p.Coluna);
        }

        public override int GetHashCode()
        {
            return Linha ^ Coluna;
        }
    }
}
