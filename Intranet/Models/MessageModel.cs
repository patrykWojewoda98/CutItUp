namespace Intranet.Models
{
    public class MessageModel
    {
        //chwilowa lista. W przyszłości będzie pobierana z bazy danych
        public static List<MessageModel> messages = new List<MessageModel>();
        public string Message { get; set; }
        public bool IsUsersMessage { get; set; }
    }
}
