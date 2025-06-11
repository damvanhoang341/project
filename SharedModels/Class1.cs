using System;
using System.Collections.Generic;

namespace SharedModels
{
    [Serializable]
    public class RequestObject
    {
        public int Type { get; set; }
        public object? Data { get; set; }
    }

    [Serializable]
    public class ResponseObject
    {
        public int Type { get; set; }
        public object Data { get; set; }
        public string StatusCode { get; set; }
    }
}
