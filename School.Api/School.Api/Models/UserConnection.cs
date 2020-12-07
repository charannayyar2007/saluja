
using System.IO;

namespace School.Api.Models
{
    public class UserConnection
    {
        public string UserName { set; get; }
        public System.Collections.Generic.HashSet<string> ConnectionID { set; get; }
        public StreamWriter Message { get; set; }
    }
}