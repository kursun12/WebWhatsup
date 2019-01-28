using System.Collections.Generic;
using System.Linq;
using WebWhatsUp.Models;

namespace WebWhatsUp.Repositories
{

    public class DbContactRepository
    {
        private DatabaseConnection databasecon = new DatabaseConnection();
        
        public IEnumerable<Contacts> GetAllContacts(string accountsEmail) 
        {
            return databasecon.Contacts.Where(i => (i.OwnerEmail == accountsEmail)).ToList(); // get all contacts that belong to the logged in user
        }

        public void AddContacts(Contacts contacts)
        {
            databasecon.Contacts.Add(contacts);
            databasecon.SaveChanges();
        }

        public bool EditContact(Contacts Contact)
        {
            Contacts GetContact = databasecon.Contacts.SingleOrDefault(c => c.Id == Contact.Id); // get the contact, if for some reason the contact could not be found return "false"
            if (GetContact != null)
            {
                GetContact.Name = Contact.Name;
                databasecon.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteContacts(Contacts Contact)
        {
            Contacts GetContact = GetContacts(Contact.Id);
            if (GetContact != null)
            {
                databasecon.Contacts.Remove(GetContact);
                databasecon.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Contacts GetContacts(int id)
        {
            return databasecon.Contacts.SingleOrDefault(i => i.Id == id); // get a specefic user from the database by the given "id"
        }

    }
}