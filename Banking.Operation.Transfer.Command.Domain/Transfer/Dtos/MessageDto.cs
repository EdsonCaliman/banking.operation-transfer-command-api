namespace Banking.Operation.Transfer.Command.Domain.Transfer.Dtos
{
    public class MessageDto
    {
        public MessageDto(string name, string email, string subject, string body)
        {
            Name = name;
            Email = email;
            Subject = subject;
            Body = body;
        }

        public string Body { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
    }
}