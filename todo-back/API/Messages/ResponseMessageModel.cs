using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Messages
{
    public class ResponseMessageModel<T>
    {
        public ResponseMessageModel(string message, T data)
        {
            Message = message;
            Data = data;
        }
        
        public string Message { get; set; }
        public T Data { get; set; }
    }
}