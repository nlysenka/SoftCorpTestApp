using System.Net;

namespace SoftCorpTestApp.Core.Exceptions
{
    public class SoftCorpException : Exception
    {
        public HttpStatusCode Status { get; set; }

        public SoftCorpException(HttpStatusCode code, string msg) : base(msg)
        {
            Status = code;
        }
    }
}
