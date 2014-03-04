using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RPGSoccer.Dominio.Dominio
{
    public class EspaçoNoCampo
    {
        public bool BolaEstaAqui { get; set; }
        public OcupaEspaço Conteudo { get; set; }

        public TipoConteudo TipoConteudo
        {
            get
            {
                if (Conteudo == null)
                {
                    Conteudo = new EspacoVazio();
                    return TipoConteudo.EspacoVazio;
                }

                if (Conteudo is Jogador)
                    return TipoConteudo.Jogador;
                

                return TipoConteudo.EspacoVazio;
            }
        }

        public EspaçoNoCampo()
        {
            Conteudo = new EspacoVazio();
            BolaEstaAqui = false;

        }

    }

    public enum TipoConteudo
    {
        Jogador,
        EspacoVazio
    }
}
