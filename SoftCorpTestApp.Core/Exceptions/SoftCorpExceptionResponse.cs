namespace SoftCorpTestApp.Core.Exceptions
{
    public class SoftCorpExceptionResponse
    {
        public string Message { get; set; }

        public SoftCorpExceptionResponse(Exception ex)
        {
            Message = ex.Message;
        }
    }
}
