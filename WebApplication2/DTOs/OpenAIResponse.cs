namespace WebApplication2.DTOs
{
    public class OpenAIResponse
    {

        public string Id { get; set; }

        public string Object { get; set; }

        public int Created { get; set; }

        public string Model { get; set; }


        public List<Choices> choices {get;set ;}
    }

    public class Choices
    {
        public String Index { get; set; }
        public Message Message { get; set; }

        public string FinishReason { get; set; }
    }

    public class Message
    {
        public String Role { get; set; }

        public String Content { get; set; }
    }
}
