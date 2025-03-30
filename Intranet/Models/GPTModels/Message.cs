namespace Intranet.Models.GPTModels
{
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
        public bool? refusal { get; set; }
        public string[] annotations { get; set; }
    }
}
