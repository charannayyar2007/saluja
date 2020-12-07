//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using Microsoft.AspNet.SignalR;

//namespace School.Api.Models
//{
//    public class NotificationHub : Hub
//    {
//        private static readonly ConcurrentDictionary<string, UserConnection> Users =
//         new ConcurrentDictionary<string, UserConnection>(StringComparer.InvariantCultureIgnoreCase);


//        public override Task OnConnected()
//        {
//            string userName = Context.User.Identity.Name;
//            string connectionId = Context.ConnectionId;

//            var user = Users.GetOrAdd(userName, _ => new UserConnection
//            {
//                UserName = userName,
//                ConnectionID = new HashSet<string>()
//            });

//            lock (user.ConnectionID)
//            {
//                user.ConnectionID.Add(connectionId);
//                if (user.ConnectionID.Count == 1)
//                {
//                    Clients.Others.userConnected(userName);
//                }
//            }

//            return base.OnConnected();
//        }

//        public override Task OnDisconnected(bool stopCalled)
//        {
//            string userName = Context.User.Identity.Name;
//            string connectionId = Context.ConnectionId;

//            UserConnection user;
//            Users.TryGetValue(userName, out user);

//            if (user != null)
//            {
//                lock (user.ConnectionID)
//                {
//                    user.ConnectionID.RemoveWhere(cid => cid.Equals(connectionId));
//                    if (!user.ConnectionID.Any())
//                    {
//                        UserConnection removedUser;
//                        Users.TryRemove(userName, out removedUser);
//                        Clients.Others.userDisconnected(userName);
//                    }
//                }
//            }

//            return base.OnDisconnected(stopCalled);
//        }
//    }
//}