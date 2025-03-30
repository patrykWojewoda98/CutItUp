namespace Intranet.Models.GPTModels
{
    public class Choices
    {
        public int index { get; set; }
        public Message message { get; set; }
        public string logprobs { get; set; }
        public string finish_reason { get; set; }
    }
}
