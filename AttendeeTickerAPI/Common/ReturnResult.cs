using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendeeTickerAPI.Common
{
    public class ReturnResult<T> where T: new ()
    {
        public List<T> itemList = new List<T>();
        public T item = new T();
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
