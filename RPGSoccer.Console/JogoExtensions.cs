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
            var jogadores = jogo.JogadoresA.ToList();
            jogadores.AddRange(jogo.JogadoresB);

            var arr = new List<string>(60);
            for(var i = 1 ; i < 101; i++)
                System.Console.Write("_");
            for (int i = 1; i < 61; i++)
            {
                System.Console.Write("\n|");
                for (int j = 1; j < 101; j++)
                {
                    var jogadorAqui = jogadores.FirstOrDefault(c => c.Localizacao.Equals(new Localizacao(i, j)));
                    var bolaAqui = jogo.Bola.Localizacao.Equals(new Localizacao(i, j));
                    if(jogadorAqui != null)
                        if(bolaAqui)
                            System.Console.Write((jogadorAqui).Numero + ".");
                        else
                            System.Console.Write((jogadorAqui).Numero);
                    else if(bolaAqui)
                        System.Console.WriteLine(".");
                    else 
                        System.Console.Write(" ");
                 
                }
                System.Console.Write("|");
                if(jogadores.All(c => c.Localizacao.Linha != i) && jogadores.All(c => c.Localizacao.Linha != i+1) )  //Se essa linha foi toda vazia, e a proxima também é, pula a proxima (economizar altura)
                        i++;
            }
        
            System.Console.WriteLine();
            for (var i = 0; i < 100; i++)
                System.Console.Write("_");
            System.Console.WriteLine();

        }


   
    }
}
