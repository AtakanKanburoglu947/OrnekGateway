using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class Response
    {
        public string? Message { get; private set; }
        public bool IsSuccessful { get; private set; }
        public Response(string message, bool isSuccessful)
        {
            Message = message;
            IsSuccessful = isSuccessful;
        }

        public static Response Success(string message) => new(message, true);
        public static Response Fail(string message) => new(message, false);

    }
    public class Response<T> : Response
    {
        public T? Data { get; set; }
        public Response(string message, bool isSuccessful, T? data) : base(message, isSuccessful)
        {
            Data = data;

        }

        public static Response<T> Success(string message, T data) => new(message, true, data);
    }
}
