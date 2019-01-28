using System.Collections.Generic;
using System.Linq;
using WebWhatsUp.Models;

namespace WebWhatsUp.Repositories
{
    public class DbChatMessageRepository
    {
        private DatabaseConnection databasecon = new DatabaseConnection();

        public void AddChatMessages(ChatMessages message)
        {
            databasecon.ChatMessages.Add(message);
            databasecon.SaveChanges();
        }

        public IEnumerable<ChatMessages> GetLastChatMessages(string email) 
        {
            IEnumerable<ChatMessages> chats = databasecon.ChatMessages.Where(i => i.ReceiverEmail == email || i.SenderEmail == email).ToList();
            return chats;
        }

        public IEnumerable<Groups> GetGroups(Accounts user) 
        {
            //.... To DO
            return null;
        }

    }
}