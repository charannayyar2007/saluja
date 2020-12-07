using School.Api.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace School.Api.Controllers.SignalR
{
    public class NotificationController : ApiController
    {
        private static ConcurrentBag<UserConnection> clients;
        static NotificationController()
        {
            clients = new ConcurrentBag<UserConnection>();
        }

        public async Task PostAsync(ChatMessage m)
        {
            m.dt = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            await ChatCallbackMsg(m);
        }
        private async Task ChatCallbackMsg(ChatMessage m)
        {
            foreach (var client in clients)
            {
                try
                {
                    var data = string.Format("data:{0}|{1}|{2}\n\n", m.username, m.text, m.dt);
                    await client.Message.WriteAsync(data);
                    await client.Message.FlushAsync();
                    client.Message.Dispose();
                }
                catch (Exception)
                {
                    UserConnection ignore;
                    clients.TryTake(out ignore);
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage Subscribe(HttpRequestMessage request)
        {
            var response = request.CreateResponse();
            response.Content = new PushStreamContent((a, b, c) =>
            { OnStreamAvailable(a, b, c); }, "text/event-stream");
            return response;
        }

        private void OnStreamAvailable(Stream stream, HttpContent content,
            TransportContext context)
        {
           /// var strem=
            var client = new StreamWriter(stream);
            UserConnection user = new UserConnection() { Message = client, };
            clients.Add(user);
        }
    }
}
