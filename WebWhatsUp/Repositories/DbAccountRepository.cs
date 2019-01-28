using System.Linq;
using WebWhatsUp.Models;

namespace WebWhatsUp.Repositories
{
    public class DbAccountRepository
    {
        private DatabaseConnection databasecon = new DatabaseConnection();
        
        public void RegisterAccount(RegisterModel register)
        {
            Accounts user = new Accounts();

            user.Name = register.Name;
            user.Email = register.Email;
            user.Password = register.Password;

            databasecon.Accounts.Add(user); 
            databasecon.SaveChanges(); // execute operatian in the DB and save changes.
        }

        public Accounts GetLoggedInAccount(string email, string password)
        {
            Accounts user = databasecon.Accounts.SingleOrDefault(i => i.Email == email && i.Password == password); // get the account that the users tries to log in with
            return user;
        }

        public Accounts GetAccount(string email)
        {
            Accounts user = databasecon.Accounts.SingleOrDefault(i => i.Email == email); // get the account from the DB that is desired by the given "email"
            return user;
        }
    }
}