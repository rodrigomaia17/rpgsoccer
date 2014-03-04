using System;

namespace RPGSoccer.Dominio.Exceptions
{
    public class PasseInvalidoException :Exception 
    {
    }

    public class TimeInvalidoException : Exception
    {
        public TimeInvalidoException(string numeraçãoIncorreta) : base(numeraçãoIncorreta)
        {
            
        }
    }
}
