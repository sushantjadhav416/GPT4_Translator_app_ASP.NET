namespace WebApplication2.DTOs
{
    public class OpenAIResponse
    {

        public string ID { get; set; }

        public string Object { get; set; }

        public int created { get; set; }

        public string model { get; set; }


        public List<Choices> choices {get;set ;}
    }

    public class Choices
    {
        public String Index { get; set; }
        public Message Message { get; set; }

        public string FinishedReason { get; set; }
    }

    public class Message
    {
        public String Role { get; set; }

        public String content { get; set; }
    }
}
