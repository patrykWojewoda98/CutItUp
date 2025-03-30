namespace Intranet.Models.GPTModels
{
    public class ResponseFromGPTModel
    {
        public string id { get; set; }
        public string @object { get; set; }
        public long created { get; set; }
        public string model { get; set; }
        public List<Choices> choices { get; set; }
        public string finish_reason { get; set; }
        public Usage usage { get; set; }
    }
}
