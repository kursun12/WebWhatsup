using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebWhatsUp.Models;
using WebWhatsUp.Repositories;

namespace WebWhatsUp.Controllers
{
    public class ContactsController : Controller
    {
        private DbContactRepository ContactRepo = new DbContactRepository();
        private DbChatMessageRepository ChatRepo = new DbChatMessageRepository();
        private DbAccountRepository repo = new DbAccountRepository();

        public ActionResult Index()
        {
            Accounts Account = (Accounts)Session["LoggedInAccounts"]; 

            if (Account == null) // check if a session is active ( if someone is logged in), if not redirect to login.
            {
                return Redirect("~/Account/Login");
            }
            else
            {
                IEnumerable<Contacts> allContacts = ContactRepo.GetAllContacts(Account.Email); // Haal alle contacten op van de desbetreffende gebruiker
                return View(allContacts);
            }
        }
        public ActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(Contacts Contacts)
        {
            if (ModelState.IsValid) // check if model state is valid(if all required fields are filled).
            {
                Accounts accounts = repo.GetAccount(Contacts.Email); // check if given e-mailaddress exist in the database, if not return errormesagge with explanation
                if (accounts != null)
                {
                    Accounts account = (Accounts)Session["LoggedInAccounts"]; // gather contact details of logged in user.
                    Contacts.OwnerEmail = account.Email;
                    ContactRepo.AddContacts(Contacts); // call the method that executes the operatian in the DB.

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("contact-error", "the provided user does not exist");
                }
            }
            return View(Contacts);
        }

        public ActionResult EditContact(int id)
        {
            Contacts contact = ContactRepo.GetContacts(id); // gather contact details of desired user and return this with the view
            return View(contact);
        }

        [HttpPost]
        public ActionResult EditContact(Contacts contact)
        {
            if (ModelState.IsValid)
            {
                bool EditUser = ContactRepo.EditContact(contact); // call the method that executes the operation in the DB & check if succesfull, if not return error message

                if (EditUser == false)
                {
                    ModelState.AddModelError("Contact-error", "oops, something went wrong could not edit user");
                    return View(contact);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(contact);
        }

        public ActionResult DeleteContact(int id)
        {
            Contacts contact = ContactRepo.GetContacts(id); // gather contact details of desired user and return this with the view
            return View(contact);
        }

        [HttpPost]
        public ActionResult DeleteContact(Contacts contact)
        {
            if (ModelState.IsValid)
            {
                bool DeleteUser = ContactRepo.DeleteContacts(contact); 
                if (DeleteUser == false)
                {
                    ModelState.AddModelError("Contact-error", "oops, something went wrong could not delete user");
                    return View(contact);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(contact);
        }

        public ActionResult SendMessage(int id) 
        {
            ChatMessages message = new ChatMessages();
            Contacts receiverAccounts = ContactRepo.GetContacts(id); // gather contact details of desired user and store this into "receiverAccount"
            Accounts senderAccounts = (Accounts)Session["LoggedInAccounts"]; // gather contact details of logged in user, who is sending the message

            message.SenderEmail = senderAccounts.Email;
            message.ReceiverEmail = receiverAccounts.Email;
            message.Time = DateTime.Now;
            ViewBag.Title = receiverAccounts.Name;

            return View(message);
        }

        [HttpPost]
        public ActionResult SendMessage(ChatMessages bericht)
        {
            if (bericht.Message == null) // check if the message is not empty, if so warn user that he or she forget to write the message
            {
                ModelState.AddModelError("Message-error", "oops, it seems you forget to type your message");
                return View(bericht);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    ChatRepo.AddChatMessages(bericht); // call the method that executes the operatian in the DB.
                }
            }
            return RedirectToAction("Index", "Chat");
        }
    }
}