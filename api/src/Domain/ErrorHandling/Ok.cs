using System.Net;

namespace SuggestionApi.Domain.ErrorHandling
{
    public class Ok: ApiError
    {
        public Ok()
            : base(200, HttpStatusCode.OK.ToString())
        {
        }

        public Ok(string message)
            : base(200, HttpStatusCode.OK.ToString(), message)
        {
        }
    }
}