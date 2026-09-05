using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Filters
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        private readonly string _reasonPhrase;
        private readonly HttpRequestMessage _request;

        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            _reasonPhrase = reasonPhrase;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(HttpStatusCode.Unauthorized, new { message = _reasonPhrase });
            return Task.FromResult(response);
        }

    }
}