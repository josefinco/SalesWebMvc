using System;

namespace SalesWebMvc.Services.Exeptions
{
    public class NotFoundExeption : ApplicationException
    {
        public NotFoundExeption(string message) : base(message)
        {
        }
        
    }
}
