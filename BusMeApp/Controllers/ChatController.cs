using BusMeApp.Managers;
using BusMeApp.Models;
using BusMeApp.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusMeApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private DbManager db = new DbManager();
        // GET: Chat
        public ActionResult Index()
        {           
            string name = User.Identity.Name;
            var users = db.GetUsers(name);
            ChatViewModel vm = new ChatViewModel()
            {
                Post = new Post(),
                UserName = name
            };
            ViewBag.Users = new SelectList(users, "Username", "UserName"); 
            return View(vm);
        }
    }
}