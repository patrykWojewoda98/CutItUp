namespace Intranet.Models.GPTModels
{
    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
        public PromptTokensDetails promptDetails { get; set; }
        public CompletionTokensDetails completionTokensDetails { get; set; }
    }
}
