using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.Entity;

namespace WebWhatsUp.Models
{
    public class DatabaseConnection : DbContext
    {
        public DatabaseConnection()
            : base("WebWhatsUpConnection")
        {
        }

        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<ChatMessages> ChatMessages { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<GroupUsers> GroupUsers { get; set; }
    }
}