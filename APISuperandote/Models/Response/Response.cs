namespace APISuperandote.Models.Response
{
    public class Response
    {
        public int success { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }
        public Response()
        {
            this.success = 0;
        }
    }
}
