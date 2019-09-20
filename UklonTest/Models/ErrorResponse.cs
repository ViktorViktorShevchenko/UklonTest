namespace UklonTest.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; } = "";
    }
}
