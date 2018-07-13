using System;
using System.Collections.Generic;
using System.Text;

namespace FreeFleet.Model.Ogame
{
    public class ServerAccount
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Server Server { get; set; }
        public bool Blocked { get; set; }
    }

    public class Server
    {
        public string Language { get; set; }
        public int Number { get; set; }
    }
}
