using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Common.Constants;

namespace TaskManagementSystem.Common.ResponseModel
{
    public class GenericResponses<T>
    {
        public int StatusCode { get; set; }
        public string StatusMsg { get; set; } = null!;
        public string? ErrorMsg { get; set; }
        public T? Response { get; set; }
        public GenericResponses<T> SuccessResponse(T response)
        {

            return new GenericResponses<T>()
            {
                StatusCode = 200,
                StatusMsg = ApiErrorCodeMessages.SUCCESS,
                Response = response
            };
        }
        public GenericResponses<T> ErrorResponse(string msg)
        {
            return new GenericResponses<T>()
            {
                StatusCode = 400,
                StatusMsg = ApiErrorCodeMessages.FAILURE,
                ErrorMsg = msg

            };
        }
        public GenericResponses<T> InternalErrorResponse(string msg)
        {
            return new GenericResponses<T>()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                StatusMsg = ApiErrorCodeMessages.FAILURE,
                ErrorMsg = msg
            };

        }

        public GenericResponses<T> BuildResponse(int statusCode, string statusMsg, List<string> msg = default, T response = default)
        {
            var errorResponse = new GenericResponses<T>()
            {
                StatusCode = statusCode,
                StatusMsg = statusMsg,
            };
            if (msg?.Count > 0)
                errorResponse.ErrorMsg = string.Join(" | ", msg);
            if (response != null)
                Response = response;
            return errorResponse;
        }
    }
}
