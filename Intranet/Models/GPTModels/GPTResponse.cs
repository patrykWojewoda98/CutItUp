namespace Intranet.Models.GPTModels
{
    public class GPTResponse
    {
        public string id { get; set; }
        public string gptobject { get; set; }
        public long created { get; set; }
        public string model { get; set; }
        public Choices[] choices { get; set; }
        public Usage usage { get; set; }
        public CompletionTokensDetails completion_tokens_details { get; set; }
        public string ServiceTier { get; set; }
        public string SystemFirgerprint { get; set; }
    }
}
