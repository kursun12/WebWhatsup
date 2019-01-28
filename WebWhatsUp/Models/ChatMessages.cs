namespace WebWhatsUp.Models
{
    public partial class ChatMessages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public System.DateTime Time { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
    }
}
