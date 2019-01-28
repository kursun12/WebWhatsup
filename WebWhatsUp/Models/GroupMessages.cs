using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWhatsUp.Models
{
    public partial class GroupMessages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public System.DateTime Time { get; set; }
        public string SenderEmail { get; set; }
    }
}