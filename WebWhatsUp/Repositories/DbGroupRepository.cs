using WebWhatsUp.Models;

namespace WebWhatsUp.Repositories
{
    public class DbGroupRepository
    {
        private DatabaseConnection databasecon = new DatabaseConnection();

        public void AddGroup(Groups group, Accounts account)
        {
            //create and add the group to database
            databasecon.Groups.Add(group);

            //register the user, to the group he just made
            GroupUsers user = new GroupUsers();
            user.AccountId = account.id;
            user.FromGroup = group.GroupKey;
            databasecon.GroupUsers.Add(user);

            //save database changes
            databasecon.SaveChanges();
        }

    }
}