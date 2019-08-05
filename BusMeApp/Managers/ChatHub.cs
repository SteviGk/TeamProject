using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusMeApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace BusMeApp.Managers
{
    public class ChatHub : Hub
    {
        private DbManager db = new DbManager();
        public void SendToUser(string from, string to, string message)
        {
           
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ApplicationUser userFrom = context.Users.First(x => x.UserName == from);
                ApplicationUser userTo = context.Users.First(x => x.UserName == to);
                Post post = new Post()
                {
                    FromUserId = userFrom.Id,
                    ToUserId = userTo.Id,
                    Text = message,
                    DateSent = DateTime.Now
                };
                db.AddPost(post);
            }         
            Clients.User(to).gotMessage(Context.User.Identity.Name, message);
            
        }
    }
}
