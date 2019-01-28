using System.Web.Mvc;
using WebWhatsUp.Models;
using WebWhatsUp.Repositories;

namespace WebWhatsUp.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            DbChatMessageRepository chatrepo = new DbChatMessageRepository();
            Accounts Account = (Accounts)Session["LoggedInAccounts"]; 

            if (Account == null) // check if a session is active ( if someone is logged in), if not redirect to login.
            {
                return Redirect("~/Account/Login");
            }
            else 
            {
                return View(chatrepo.GetLastChatMessages(Account.Email));
            }
        }

        public ActionResult GroupChat()
        {
            return View();
        }

        public ActionResult CreateGroupChat()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGroupChat(Groups group)
        {
            DbGroupRepository grouprepo = new DbGroupRepository();
            Accounts Account = (Accounts)Session["LoggedInAccounts"];

            if (ModelState.IsValid)
            {
                grouprepo.AddGroup(group, Account);
                return RedirectToAction("GroupChat", "Chat");
            }
            return View(group);
        }
    }
}