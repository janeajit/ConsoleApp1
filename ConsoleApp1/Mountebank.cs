using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class MountebankClass
    {



        public class Rootobject
        {
            public string requestFrom { get; set; }
            public string method { get; set; }
            public string path { get; set; }
            public Query query { get; set; }
            public Headers headers { get; set; }
            public string body { get; set; }
            public string ip { get; set; }
            public DateTime timestamp { get; set; }
        }

        public class Query
        {
        }

        public class Headers
        {
            public string Host { get; set; }
            public string XForwardedFor { get; set; }
            public string ContentType { get; set; }
            public string ContentLength { get; set; }
        }

    }
}
