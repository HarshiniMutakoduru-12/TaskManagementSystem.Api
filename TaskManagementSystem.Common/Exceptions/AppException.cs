using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Common.Exceptions
{
    public class AppException : ApplicationException
    {
        public AppException()
        {

        }
        public AppException(string messageCode) : base(messageCode)
        {
        }

        public AppException(string messageCode, Exception innerException) : base(messageCode, innerException)
        {
        }
    }
}
