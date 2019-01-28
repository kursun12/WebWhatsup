using System.Web.Mvc;
using System.Web.Security;
using WebWhatsUp.Models;
using WebWhatsUp.Repositories;

namespace WebWhatsUp.Controllers
{
    public class AccountController : Controller
    {
       private DbAccountRepository repo = new DbAccountRepository();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // get account with given credentials
                Accounts Account = repo.GetLoggedInAccount(model.Email, model.Password);
                if (Account != null)
                {
                    FormsAuthentication.SetAuthCookie(Account.Email, false);

                    // remember complete account
                    Session["LoggedInAccounts"] = Account;

                    //redirect to default entry of Contact controller
                    return RedirectToAction("Index", "Chat");
                }
                else
                {
                    ModelState.AddModelError("login-error", "the username or password provided is incorrect");
                }
            }
            //there was an error, go back to Login page
            return View(model);
        }

        public ActionResult LogOut()
        {           
            if (Session["LoggedInAccounts"] != null)    //check if there are logged in accounts if so forward to log-out page
            {
                return View("LogOut");
            }
            else   //if not logged in yet, forward to log in page
            {
                return View("Login");
            }
        }

        [HttpPost] 
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();    // clear current logged in sessions
            Session["LoggedInAccounts"] = null;

            return RedirectToAction("Login", "Account");    // forward to log in page
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Register(RegisterModel user)
        {
            Accounts account = repo.GetAccount(user.Email);
            // check if given e-mailaddress already exist in the database, if so return errormesagge with explanation
            if (account != null)
            {
                ModelState.AddModelError("Register-error", "Email-Adress already exist");
                return View(user);
            }
            // check if Modelstate is valid (if all required fields are filled in), if valid forward to chat page 
            else if (ModelState.IsValid)
            {
                repo.RegisterAccount(user);
                return RedirectToAction("Index", "Chat");
            }          
            else
            {
                ModelState.AddModelError("Register-error", "Please fill in the missing fields");
                return View(user);
            }
        }
    }
}