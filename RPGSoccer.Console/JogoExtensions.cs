using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using RPGSoccer.Dominio;
using RPGSoccer.Dominio.Dominio;
using RPGSoccer.Dominio.Motor;

namespace RPGSoccer.Console
{
    public static class JogoExtensions
    {
        public static void DesenhaCampo(this Jogo jogo)
        {
            for(var i = 0 ; i < 100; i++)
                System.Console.Write("_");
            for (int i = 0; i < jogo.Campo.Count; i++)
            {
                var linha = jogo.Campo[i];
               

                System.Console.Write("\n|");
                for (int j = 0; j < linha.Count; j++)
                {
                    var coluna = linha[j];
                    if (coluna.TipoConteudo == TipoConteudo.EspacoVazio)
                        System.Console.Write(" ");
                    else if (coluna.TipoConteudo == TipoConteudo.Jogador && !coluna.BolaEstaAqui)
                        System.Console.Write(((Jogador)coluna.Conteudo).Numero);
                    else if (coluna.TipoConteudo == TipoConteudo.Jogador && coluna.BolaEstaAqui)
                        System.Console.Write(((Jogador)coluna.Conteudo).Numero + ".");
                }
                System.Console.Write("|");
                if(linha.All(c => c.TipoConteudo == TipoConteudo.EspacoVazio) )  //Se essa linha foi toda vazia, e a proxima também é, pula a proxima (economizar altura)
                    if (i < jogo.Campo.Count - 1 &&
                        jogo.Campo[i + 1].All(c => c.TipoConteudo == TipoConteudo.EspacoVazio))
                        i++;
            }
        
            System.Console.WriteLine();
            for (var i = 0; i < 100; i++)
                System.Console.Write("_");
            System.Console.WriteLine();

        }


   
    }
}
